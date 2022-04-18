using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography; 


namespace Sistem_Proje
{
	class aesSifreleVeCoz
	{
		private const string AES_IV = @"!&+QWSDF!123126+";//içeriği ne olacak karakter türü
		private string aesAnahtar = @"QQsaw!257()%%ert";
		AesCryptoServiceProvider aesSaglayici = new AesCryptoServiceProvider();
		public string AESsifrele(string metin)
		{
			
			//kaç bitlik şifreleme yapacağımızı söylüyoruz
			aesSaglayici.BlockSize = 128;
			aesSaglayici.KeySize = 128;

			aesSaglayici.IV = Encoding.UTF8.GetBytes(AES_IV);//şifrelenecek metnin hangi karakterlerde olması
			aesSaglayici.Key = Encoding.UTF8.GetBytes(aesAnahtar);
			aesSaglayici.Mode = CipherMode.CBC;//modunu belirledik
			aesSaglayici.Padding = PaddingMode.PKCS7;

			byte[] kaynak = Encoding.Unicode.GetBytes(metin);//şifreleyecek

			using(ICryptoTransform sifrele = aesSaglayici.CreateEncryptor())
			{
				byte[] hedef = sifrele.TransformFinalBlock(kaynak, 0, kaynak.Length);
				//byte türünde değişken oluşturduk 
				return Convert.ToBase64String(hedef);//64 bitlik değeri dönüştürüp metin değişkenine göndereceğiz
			}
		}
		public String AESsifre_Coz(string sifreliMetin)
		{
			aesSaglayici.BlockSize = 128;
			aesSaglayici.KeySize = 128;

			aesSaglayici.IV = Encoding.UTF8.GetBytes(AES_IV);
			aesSaglayici.Key = Encoding.UTF8.GetBytes(aesAnahtar);
			aesSaglayici.Mode = CipherMode.CBC;
			aesSaglayici.Padding = PaddingMode.PKCS7;

			byte[] kaynak = System.Convert.FromBase64String(sifreliMetin);
			using (ICryptoTransform decrypt = aesSaglayici.CreateDecryptor())
			{
				byte[] hedef = decrypt.TransformFinalBlock(kaynak, 0, kaynak.Length);
				//offseti 0 belirledik ramdeki yeri belirsiz olduğu için 0
				return Encoding
					.Unicode.GetString(hedef);
			}
		}
	}
}
