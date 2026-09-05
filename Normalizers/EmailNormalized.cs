namespace Mecanica.Normalizers
{
    public class EmailNormalized
    {
        public static string Normalizar(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
