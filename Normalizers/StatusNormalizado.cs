using Mecanica.Models.Enums;
using System.Text.RegularExpressions;

namespace Mecanica.Normalizers
{
    public class StatusNormalizado
    {
        public static string Normalizar(string status)
        {
            return status.Trim().ToUpper();
        }

       
    }
}
