using System.Text;
using System.Text.RegularExpressions;

namespace Mecanica.Normalizers
{
    public class NomeNormalizado
    {
        public static string NormalizarUsuario(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) return nome.ToLower();

            nome = nome.Normalize(NormalizationForm.FormD);
            nome = Regex.Replace(nome, @"[\p{IsCombiningDiacriticalMarks}]+", "");
            nome = Regex.Replace(nome,@"[^a-zA-Z0-9\s]","" );
            nome = nome.Trim();
            return nome.ToLower();
        }

        public static string NormalizarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) return nome.ToLower();
            nome = string.Join(" ", nome.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            return nome;
        }
    }
}
