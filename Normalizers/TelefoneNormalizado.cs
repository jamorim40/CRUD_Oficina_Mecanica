using System.Text.RegularExpressions;

namespace Mecanica.Normalizers
{
    public class TelefoneNormalizado
    {
        public static string Normalizar(string telefone)
        {
            return Regex.Replace(telefone, @"[()\s-]","");
        }
    }
}
