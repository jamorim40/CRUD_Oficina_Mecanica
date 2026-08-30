using System.ComponentModel;
using System.Reflection;

namespace Mecanica.Extensions
{
    public static class EnumExtensoes
    {
        public static string ObterDescricao(this Enum valor)
        {
            FieldInfo? campo = valor.GetType().GetField(valor.ToString());

            if (campo == null)
                return valor.ToString();

            DescriptionAttribute? atributo = campo.GetCustomAttribute<DescriptionAttribute>();

            return atributo?.Description ?? valor.ToString();
        }

        public static T ObterEnumPorDescricao<T>(string descricao) where T : Enum
        {
            foreach (var campo in typeof(T).GetFields())
            {
                var atributo = Attribute.GetCustomAttribute(
                    campo,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (atributo != null && atributo.Description == descricao)
                {
                    return (T) campo.GetValue(null)!;
                }

                    
            }
                throw new ArgumentException($"Descrição '{descricao}' não encontrada. ");
        }
    }
}
