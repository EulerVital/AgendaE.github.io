using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Common.Enum.App.HigienizadorEstofados
{
    public enum TamanhoItemHEEnum
    {
        Pequeno,

        [Description("Médio")]
        Medio,  
        Grande, 
        Casal,
        Queen,
        King,
        Solteiro
    }
}
