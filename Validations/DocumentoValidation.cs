using System.Text.RegularExpressions;
using Mecanica.Normalizers;

namespace Mecanica.Validations
{
    public static class DocumentoValidation
    {
        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            // Remove qualquer caractere que não seja número
            cpf = Regex.Replace(cpf, @"[^\d]", "");

            // O CPF deve ter exatamente 11 dígitos
            if (cpf.Length != 11) return false;

            // Elimina sequências conhecidas e inválidas de números repetidos
            if (new string(cpf[0], 11) == cpf) return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Validação do Primeiro Dígito
            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (cpf[i] - '0') * multiplicador1[i];

            int resto = (soma * 10) % 11;
            int digito1 = resto == 10 ? 0 : resto;

            if (cpf[9] - '0' != digito1) return false;

            // Validação do Segundo Dígito
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (cpf[i] - '0') * multiplicador2[i];

            resto = (soma * 10) % 11;
            int digito2 = resto == 10 ? 0 : resto;

            return cpf[10] - '0' == digito2;
        }

        public static bool ValidarCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj)) return false;

            // Remove qualquer caractere que não seja número
            cnpj = Regex.Replace(cnpj, @"[^\d]", "");

            // O CNPJ deve ter exatamente 14 dígitos
            if (cnpj.Length != 14) return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Validação do Primeiro Dígito
            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += (cnpj[i] - '0') * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            if (cnpj[12] - '0' != digito1) return false;

            // Validação do Segundo Dígito
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += (cnpj[i] - '0') * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cnpj[13] - '0' == digito2;
        }

        public static bool ValidarCpfCnpj(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return false;

            // Normaliza removendo caracteres especiais
            documento = DocumentoNormalized.Normalizar(documento);

            // Considera apenas os dígitos para validação
            var apenasDigitos = Regex.Replace(documento, @"[^\d]", "");

            if (apenasDigitos.Length == 11)
                return ValidarCpf(apenasDigitos);
            if (apenasDigitos.Length == 14)
                return ValidarCnpj(apenasDigitos);

            return false;
        }
        
    }
}
