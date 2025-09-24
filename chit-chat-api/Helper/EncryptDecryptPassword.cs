using System.Text;

namespace chit_chat_api.Helper
{
    public class EncryptDecryptPassword
    {
        public static string? EncryptPassword(string? password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storepassword=ASCIIEncoding.ASCII.GetBytes(password);
                string storeencryptedpassword=Convert.ToBase64String(storepassword);
                return storeencryptedpassword;
            }
        }
        public static string? DecryptPassword(string? password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedpassword=Convert.FromBase64String(password);
                string decryptedpassword=ASCIIEncoding.ASCII.GetString(encryptedpassword);
                return decryptedpassword;
            }
        }
    }
}
