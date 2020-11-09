using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace templateProject.Helper
{
    public enum EncryptionMethod
    {
        MD5,
        SHA1
    }

    public class Encryption
    {
        private static Byte[] m_Key = new Byte[24];
        private static Byte[] m_IV = new Byte[8];
        private static bool InitKey(String key)
        {
            try
            {
                // Convert Key to byte array
                byte[] bp = new byte[key.Length];
                ASCIIEncoding aEnc = new ASCIIEncoding();
                aEnc.GetBytes(key, 0, key.Length, bp, 0);


                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                byte[] bpHash = sha.ComputeHash(bp);

                int i;

                for (i = 0; i < 20; i++)
                    m_Key[i] = bpHash[i];

                for (i = 8; i < 16; i++)
                    m_IV[i - 8] = bpHash[i];

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region RegularEncryptDecrypt
        public static string EncryptRegular(String key, String value)
        {
            string strResult;
            if (value.Length > 92160)
            {
                strResult = "Error. Data String too large. Keep within 90Kb.";
                return strResult;
            }

            if (!InitKey(key))
            {
                strResult = "Error. Fail to generate key for encryption";
                return strResult;
            }

            value = String.Format("{0,5:00000}" + value, value.Length);

            byte[] rbData = new byte[value.Length];
            ASCIIEncoding aEnc = new ASCIIEncoding();
            aEnc.GetBytes(value, 0, value.Length, rbData, 0);

            TripleDESCryptoServiceProvider descsp = new TripleDESCryptoServiceProvider();

            ICryptoTransform desEncrypt = descsp.CreateEncryptor(m_Key, m_IV);

            MemoryStream mStream = new MemoryStream(rbData);
            CryptoStream cs = new CryptoStream(mStream, desEncrypt, CryptoStreamMode.Read);
            MemoryStream mOut = new MemoryStream();

            int bytesRead;
            byte[] output = new byte[1024];
            do
            {
                bytesRead = cs.Read(output, 0, 1024);
                if (bytesRead != 0)
                    mOut.Write(output, 0, bytesRead);
            } while (bytesRead > 0);

            if (mOut.Length == 0)
                strResult = "";
            else
                strResult = Convert.ToBase64String(mOut.GetBuffer(), 0, (int)mOut.Length);

            return strResult;
        }

        public static string DecryptRegular(String key, String value)
        {
            string strResult;

            if (!InitKey(key))
            {
                strResult = "Error. Fail to generate key for decryption";
                return strResult;
            }
            int nReturn = 0;
            TripleDESCryptoServiceProvider descsp = new TripleDESCryptoServiceProvider();
            ICryptoTransform desDecrypt = descsp.CreateDecryptor(m_Key, m_IV);

            MemoryStream mOut = new MemoryStream();
            CryptoStream cs = new CryptoStream(mOut, desDecrypt, CryptoStreamMode.Write);

            byte[] bPlain = new byte[value.Length];
            try
            {
                bPlain = Convert.FromBase64CharArray(value.ToCharArray(), 0, value.Length);
            }
            catch (Exception)
            {
                strResult = "Error. Input Data is not base64 encoded.";
                return strResult;
            }

            long lRead = 0;
            long lTotal = value.Length;

            try
            {
                while (lTotal >= lRead)
                {
                    cs.Write(bPlain, 0, (int)bPlain.Length);
                    lRead = mOut.Length + Convert.ToUInt32(((bPlain.Length / descsp.BlockSize) * descsp.BlockSize));
                };

                ASCIIEncoding aEnc = new ASCIIEncoding();
                strResult = aEnc.GetString(mOut.GetBuffer(), 0, (int)mOut.Length);
                String strLen = strResult.Substring(0, 5);
                int nLen = Convert.ToInt32(strLen);
                strResult = strResult.Substring(5, nLen);
                nReturn = (int)mOut.Length;

                return strResult;
            }
            catch (Exception e)
            {
                strResult = e + "Error. Decryption Failed. Possibly due to incorrect Key or corrputed data";
                return strResult;
            }


        }
        #endregion

        #region
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
        #endregion
    }
}