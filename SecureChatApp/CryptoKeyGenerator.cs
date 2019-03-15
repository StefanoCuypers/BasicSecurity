using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Windows;

namespace SecureChatApp
{
    class CryptoKeyGenerator
    {
        private static byte[] Key;
        private static byte[] IV;
        public void generateAESKey()
        {
            AesManaged aesEncryption = new AesManaged();
            aesEncryption.KeySize = 256;


            IV = aesEncryption.IV;
            //  string ivStr = Convert.ToBase64String(aesEncryption.IV);

            //  string keyStr = Convert.ToBase64String(aesEncryption.Key);
            Key = aesEncryption.Key;

        }

        public void generateRSAKey()
        {


            //stream to save the keys
            FileStream fs = null;
            StreamWriter sw = null;

            //create RSA provider
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096);
            RSACryptoServiceProvider rsa1 = new RSACryptoServiceProvider(4096);
            try
            {
                //save private key
                fs = new FileStream(Path.Combine(Environment.CurrentDirectory, "Private_A.xml"), FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa.ToXmlString(true));
                sw.Flush();
                fs = new FileStream(Path.Combine(Environment.CurrentDirectory, "Private_B.xml"), FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa1.ToXmlString(true));
                sw.Flush();
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }

            try
            {
                //save public key
                fs = new FileStream(Path.Combine(Environment.CurrentDirectory, "Public_A.xml"), FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa.ToXmlString(true));
                sw.Flush();
                fs = new FileStream(Path.Combine(Environment.CurrentDirectory, "Public_B.xml"), FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa1.ToXmlString(true));
                sw.Flush();
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
            rsa.Clear();

        }
        //--------------------------------------AES ENCRYPT / DECRYPT-----------------------------------------

        public void Encrypt(string plainText)
        {
            byte[] encrypted;
            FileStream fs = null;

            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();

                        fs = new FileStream(Path.Combine(Environment.CurrentDirectory, "File1.txt"), FileMode.Create, FileAccess.Write);
                        StreamWriter sw1 = new StreamWriter(fs);
                        sw1.Write(Convert.ToBase64String(encrypted));
                        sw1.Flush();
                        

                    }
                }
            }
            // Return encrypted data    

        }
        public string Decrypt(byte[] cipherText)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        //--------------------------------------AES ENCRYPT / DECRYPT-----------------------------------------




    }
}
