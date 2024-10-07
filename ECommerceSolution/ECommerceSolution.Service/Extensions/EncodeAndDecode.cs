using System.Security.Cryptography;

namespace ECommerceSolution.Service.Extensions
{
    public class EncodeAndDecode
    {
        public static string GetEncryptedText(string InputString)
        {
            string senc = "";
            try
            {
                string smsg = InputString;

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                System.Text.Encoding utf = new System.Text.UTF8Encoding();
                byte[] key = utf.GetBytes("57836241");
                byte[] iv = { 1, 2, 3, 4, 8, 7, 6, 5 };
                ICryptoTransform encryptor = des.CreateEncryptor(key, iv);

                byte[] bmsg = utf.GetBytes(smsg);
                byte[] benc = encryptor.TransformFinalBlock(bmsg, 0, bmsg.Length);
                senc = System.Convert.ToBase64String(benc);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return senc;
        }
        public static string GetDecryptedText(string EncryptedString)
        {
            string sdec = "";
            try
            {
                string smsg = EncryptedString;
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                System.Text.Encoding utf = new System.Text.UTF8Encoding();

                byte[] key = utf.GetBytes("57836241");
                byte[] iv = { 1, 2, 3, 4, 8, 7, 6, 5 };

                ICryptoTransform decryptor = des.CreateDecryptor(key, iv);

                byte[] bmsg = utf.GetBytes(smsg);

                byte[] benc1 = System.Convert.FromBase64String(EncryptedString);
                byte[] bdec = decryptor.TransformFinalBlock(benc1, 0, benc1.Length);
                sdec = utf.GetString(bdec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return sdec;
        }
    }
}
