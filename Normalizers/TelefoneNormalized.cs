using System.Text.RegularExpressions;

namespace Mecanica.Normalizers
{
    public class TelefoneNormalized
    {
        public static string Normalizar(string telefone)
        {
            return Regex.Replace(telefone, @"[()\s-]","");
        }
    }
}
