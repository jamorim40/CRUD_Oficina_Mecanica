using System.Text.RegularExpressions;

namespace Mecanica.Normalizers
{
    public class PlacaNormalizado
    {
        public static string Normalizar(string placa)
        {
            placa = Regex.Replace(placa, @"[^A-Za-z0-9]","");
            return placa.ToUpper();
        }
    }
}
