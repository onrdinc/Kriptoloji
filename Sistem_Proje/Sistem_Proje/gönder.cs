using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;//kütüphaneyi ekledik

namespace Sistem_Proje
{
	public partial class gönder : Form
	{
		public gönder()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MailMessage mesajım = new MailMessage();
			SmtpClient istemci = new SmtpClient();//smtp-simple mail transfer protokol
			istemci.Credentials = new System.Net.NetworkCredential("onurirem.19@hotmail.com", "onur1907");
			istemci.Port = 587;//tr de kullanılan port
			istemci.Host = "smtp.live.com";
			//ssl güvenlik yuva katmanı sunucu ile istemci 
			//arasındaki verileri doğru adrese göderene kadar şifreleme yapar
			istemci.EnableSsl = true;
			mesajım.To.Add(textBox1.Text);
			mesajım.From = new MailAddress("onurirem.19@hotmail.com");
			mesajım.Subject = textBox2.Text;
			mesajım.Body = textBox3.Text;
			istemci.Send(mesajım);
			try
			{
				istemci.Send(mesajım);
				MessageBox.Show("GÖNDERİLDİ", "Bilgilendirme Penceresi",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}catch(Exception)
			{
				MessageBox.Show("MAİL GÖNDERİLİRKEN BİR HATA İLE KARŞILAŞTI", "Bilgilendirme Penceresi",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
