using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;


namespace Sistem_Proje
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		aesSifreleVeCoz AESSifre = new aesSifreleVeCoz();

		
		public static string md5(string metin)
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.UTF8.GetBytes(metin);//metnin boyutuna göre hash hesaplar
			bytes = md5.ComputeHash(bytes);
			StringBuilder sb = new StringBuilder();
			foreach (var item in bytes)
			{
				sb.Append(item.ToString("x2").ToLower());//her 2 baytı hexadecimal hane olarak değiştir
			}
			return sb.ToString();
		}
		public static string sha1(string metin)
		{
			SHA1 sha1sifreleme = new SHA1CryptoServiceProvider();
			byte[] bytes = sha1sifreleme.ComputeHash(Encoding.UTF8.GetBytes(metin));
			StringBuilder sb = new StringBuilder();
			foreach (var item in bytes)
			{
				sb.Append(item.ToString("x2"));
			}
			return sb.ToString();
		}
		
		public static string sha2(string metin)
		{
			SHA256 sha2sifreleme = new SHA256CryptoServiceProvider();
			byte[] bytes = sha2sifreleme.ComputeHash(Encoding.UTF8.GetBytes(metin));
			StringBuilder sb = new StringBuilder();
			foreach (var item in bytes)
			{
				sb.Append(item.ToString("x2"));
			}
			return sb.ToString();
		}
		public static string sha3(string metin)
		{
			SHA384 sha3sifreleme = new SHA384CryptoServiceProvider();
			byte[] bytes = sha3sifreleme.ComputeHash(Encoding.UTF8.GetBytes(metin));
			StringBuilder sb = new StringBuilder();
			foreach (var item in bytes)
			{
				sb.Append(item.ToString("x2"));
			}
			return sb.ToString();
		}
		public static string polybius(string metin)
		{
			int[,] dizi = new int[5, 5];
			string sifreli = "";
			string alfabe = "abcdefgğhıjklmnoprsştuvyz";
			int say = 0;
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					dizi[i, j] = alfabe[say];
					say++;
				}
			}
			for (int i = 0; i < metin.Length; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					for (int k = 0; k < 5; k++)
					{
						if (metin[i] == dizi[j, k])
						{
							sifreli = sifreli + (j + 1) + (k + 1);
						}
					}
				}
			}
			return sifreli;
		}
		
		public static string sezar(string metin,int sayı)
		{
			string sifreli = "";
			char[] karakterler = metin.ToCharArray();//char dizisine ata
			foreach(char eleman in karakterler)
			{
				sifreli += Convert.ToChar(eleman+sayı).ToString();
			}
			return sifreli;
		}
		public static string sezar_coz(string metin,int sayı)
		{
			string sifre_coz = "";
			char[] karakterler2 = metin.ToCharArray();
			foreach(char eleman2 in karakterler2)
			{
				sifre_coz += Convert.ToChar(eleman2 - sayı).ToString();
			}
			return sifre_coz;
		}

		
		public static string affine(string metin, int a, int b)
		//y=ax+b
		{
		
			string sifreli = "";
			for (int i = 0; i < metin.Length; i++)
			{
				int ascii_degeri = metin[i] - 96;//a=97, z=123--a=1,z=26
				int y = a * ascii_degeri + b;
				//değer 26 dan büyükse başa döndürmemiz gerekiyor
				while (y > 26)
				{
					y -= 26;
				}
				char sifrelenmis_harf = (char)(y + 96);//sayısal verinin ascii deki denki
				sifreli += sifrelenmis_harf;
			}
			return sifreli;
		}
		 

		public static string vigenere(string metin, string metin_2)
		{
			char[] kelime;
			string sifreli = "";
			if (metin.Length >= metin_2.Length)
			{
				kelime = new char[metin.Length];
				for (int i = 0; i < metin.Length; i++)
				{
					char cr1 = Convert.ToChar(metin.Substring(i, 1).ToLower());
					char cr2 = Convert.ToChar(metin_2.Substring(((i % metin_2.Length)), 1).ToLower());

					int s1 = Convert.ToInt32(cr1) - 96;
					int s2 = Convert.ToInt32(cr2) - 96;
					int s3 = ((s1 + s2) % 26) + 96;
					kelime[i] = Convert.ToChar(s3);
				}
				for (int i = 0; i < metin.Length; i++)
				{
					sifreli += Convert.ToString(kelime[i]);
				}
			}
			else
			{
				kelime = new char[metin_2.Length];
				for (int i = 0; i < metin_2.Length; i++)
				{
					char cr1 = Convert.ToChar(metin_2.Substring(i, 1).ToLower());
					char cr2 = Convert.ToChar(metin.Substring(((i % metin.Length)), 1).ToLower());

					int s1 = Convert.ToInt32(cr1) - 96;
					int s2 = Convert.ToInt32(cr2) - 96;
					int s3 = ((s1 + s2) % 26) + 96;
					kelime[i] = Convert.ToChar(s3);
				}
				for (int i = 0; i < metin_2.Length; i++)
				{
					sifreli += Convert.ToString(kelime[i]);
				}

			}
			return sifreli;
		}
		public static string ROT13(string metin)
		{
			char[] dizi = metin.ToCharArray();
			for (int i = 0; i < dizi.Length; i++)
			{
				int ilk = (int)dizi[i];

				if (ilk >= 'a' && ilk <= 'z')
				{
					if (ilk > 'm')
					{
						ilk -= 13;
					}
					else
						ilk += 13;
				}

				else if (ilk >= 'A' && ilk <= 'Z')
				{
					if (ilk > 'M')
					{
						ilk -= 13;
					}
					else
						ilk += 13;
				}

				dizi[i] = (char)ilk;
			}

			return new string(dizi);
		}
		public void sifrele(string veri, string anahtar24, string dyolu)
		{
			try
			{
				FileStream fs = new FileStream(dyolu, FileMode.OpenOrCreate, FileAccess.Write);
				//Dosyayı kaydetmek için bir filestream nesnesi oluşturduk.
				byte[] key = Encoding.ASCII.GetBytes(anahtar24);
				//parametreyle gelen anahtarı aldık ve byte dizisine atadık
				byte[] iv = { 04, 21, 70, 30, 32, 04, 21, 70 };
				//Vektörü 8 byte olarak belirledik.
				byte[] d = Encoding.Default.GetBytes(veri);
				//parametreyle gelen string türündeki şifrelenecek metni byte dizisine atadık
				TripleDES servis = new TripleDESCryptoServiceProvider();
				CryptoStream sifreyaz = new CryptoStream(fs, servis.CreateEncryptor(key, iv), CryptoStreamMode.Write);
				//TripleDes kripto servisini çağırdık.Cryptostream 3 parametreyle çalışıyor.Bu kısım can alıcı nokta 
				//iyice inceleyin.Şifrelenecek Filestream,Şifreleme fonksiyonu anahtar ve vektörle çalışıyor.Cryptomuz
				//yazma modunda çalışıyor.
				sifreyaz.Write(d, 0, d.Length);
				//Crypto streami dosyaya yazıyoruz.
				sifreyaz.Close();
				fs.Close();
			}
			catch
			{
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string sMetin = textBox1.Text;
			string aMetin = textBox3.Text; if (aMetin == "") { aMetin = "1"; }
			string a = textBox4.Text;
			string b = textBox5.Text;
			if (comboBox1.Text == "MD5") { textBox2.Text = md5(sMetin); }
			if (comboBox1.Text == "a") {  textBox2.Text = affine(sMetin, Int32.Parse(a), Int32.Parse(b)); }
			if (comboBox1.Text == "SHA-1") { textBox2.Text = sha1(sMetin); }
			if (comboBox1.Text == "SHA-2") { textBox2.Text = sha2(sMetin); }
			if (comboBox1.Text == "SHA-3") { textBox2.Text = sha3(sMetin); }
			if (comboBox1.Text == "POLYBİUS") { textBox2.Text = polybius(sMetin); }
			if (comboBox1.Text == "SEZAR") { textBox2.Text = sezar(sMetin, Int32.Parse(aMetin)); }
			if (comboBox1.Text == "AFFİNE") { textBox2.Text = affine(sMetin, Int32.Parse(a), Int32.Parse(b)); }
			if (comboBox1.Text == "VİGENERE") { textBox2.Text = vigenere(sMetin, aMetin); }
			if (comboBox1.Text == "ROT13") { textBox2.Text = ROT13(sMetin); }
			if (comboBox1.Text == "TRİPLE DES")
			{
				string anahtar = "A96E6945H9658J1246XM43R1";
				//24 byte değerinde şifre oluşturuyoruz.Sadece bu şifreyle veriyi eski haline döndürebiliriz.
				string kayityolu = "C://sifreli.txt";
				//Şifreli metni nereye kaydedeceğimizi belirliyoruz.
				sifrele(textBox1.Text, anahtar, kayityolu);
				//Yazdığımız fonksiyonu çağırdık.Gerisini o helledecek.
				if (textBox1.Text == "") { MessageBox.Show("işlem gerçekleştirilemedi"); }
				else { MessageBox.Show("İşlem Başarılı"); }
			}
			if (comboBox1.Text == "AES") {
				textBox2.Text = AESSifre.AESsifrele(textBox1.Text);
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox1.Text == "SEZAR" || comboBox1.Text == "VİGENERE") { label4.Visible = true; textBox3.Visible = true; }
			else { label4.Visible = false; textBox3.Visible = false; }
			if (comboBox1.Text == "AFFİNE")
			{
				label5.Visible = true;
				label6.Visible = true; textBox4.Visible = true; textBox5.Visible = true;
			}
			else
			{
				label5.Visible = false; label6.Visible = false;
				textBox4.Visible = false; textBox5.Visible = false;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Form gönder = new gönder();
			gönder.Show();
			
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string aMetin = textBox3.Text;
			string sifreli_metin = textBox2.Text;
			string a = textBox4.Text;
			string b = textBox5.Text;
			if (comboBox1.Text == "SEZAR") { textBox6.Text = sezar_coz(sifreli_metin, Int32.Parse(aMetin)); }
			if (comboBox1.Text == "AES") { textBox6.Text = AESSifre.AESsifre_Coz(textBox2.Text); }
			if (comboBox1.Text == "MD5") {
				MessageBox.Show("TEK YÖNLÜ ŞİFRELEME YAPTIĞI İÇİN ESKİ HALİNE DÖNDÜRÜLEMEZ!","Bilgilendirme Penceresi",
					MessageBoxButtons.OK,MessageBoxIcon.Warning);
			}
			
			
		}
	} 
	
}


