using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace HipercorWeb.Models
{

    /*
     * https://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp
     */
    public static class StringCipher
    {
        
        public static string Encrypt(string password, byte[] key)
        {
            //byte[] keyArray = Encoding.UTF8.GetBytes(key); //Exception Specified key is not a valid size for this algorithm.
            byte[] keyArray = key; // debe ser un array con 24 de longitud 
            byte[] passArray = Encoding.UTF8.GetBytes(password);
            try
            {
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = keyArray;
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;

                ICryptoTransform crypto = tripleDES.CreateEncryptor();
                byte[] encryptPass = crypto.TransformFinalBlock(passArray, 0, passArray.Length);
                tripleDES.Clear();

                return Convert.ToBase64String(encryptPass, 0, encryptPass.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
