using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Common.Extension
{
    public static class EnumDescriptonExension
    {
        public static Dictionary<int, string> ToDictionary<TEnum>(bool isSelecione = true) where TEnum : System.Enum
        {
            var type = typeof(TEnum);
            var retorno = System.Enum.GetValues(type)
                .Cast<TEnum>()
                .ToDictionary(
                    e => Convert.ToInt32(e),
                    e => ToDescription(e)
                );

            //if (isSelecione)
            //{
            //    retorno.Append(new KeyValuePair<int, string>(-1, "Selecione"));
            //    retorno = retorno.OrderBy(o => o.Key).ToDictionary();
            //}

            return retorno;
        }

        public static string ToDescription(this System.Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? value.ToString();
        }
    }
}
