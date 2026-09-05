using System.Text.RegularExpressions;

namespace Mecanica.Normalizers
{
    public static class DocumentoNormalized
    {
        public static string Normalizar(string documento)
        {
            return Regex.Replace(documento, @"[^\dA-Za-z]", "");
        }
    }
}
