namespace Mecanica.Normalizers
{
    public class EmailNormalizer
    {
        public static string Normalizar(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
