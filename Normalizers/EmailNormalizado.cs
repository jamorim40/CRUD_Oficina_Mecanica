namespace Mecanica.Normalizers
{
    public class EmailNormalizado
    {
        public static string Normalizar(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
