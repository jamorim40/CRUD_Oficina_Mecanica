
using System.Text.RegularExpressions;

namespace Mecanica.Normalizers
{
    public class StatusNormalizado
    {
        public static string Normalizar(string status)
        {
            return Regex.Replace(status.Trim(), @"[\s\-_/]+", " ").ToUpper();
        }

       
    }
}
