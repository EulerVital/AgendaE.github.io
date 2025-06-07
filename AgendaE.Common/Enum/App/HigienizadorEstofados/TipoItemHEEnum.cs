using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Common.Enum.App.HigienizadorEstofados
{
    public enum TipoItemHEEnum
    {
        [Description("Sofá")]
        Sofa,
        [Description("Colchão")]
        Colchao,
        Poltrona,
        Cadeira,
        Carro,
        Outro
    }
}
