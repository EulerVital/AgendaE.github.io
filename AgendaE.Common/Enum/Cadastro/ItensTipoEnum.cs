using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Common.Enum
{
    public enum ItensTipoEnum
    {
        [Description("Salão")]
        Salao = 0,

        [Description("Dread Maker")]
        Dreadmaker = 1,

        [Description("Adega")]
        Adega = 2,

        [Description("Tabacaria")]
        Tabacaria = 3,

        [Description("Lanchonete")]
        Lanchonete = 4,

        [Description("Manutenção PC")]
        ManutencaoPC = 5,

        [Description("Manutenção Maquina de Lavar")]
        ManutencaoMaquinaLavar = 6,

        [Description("Bar")]
        Bar = 7,

        [Description("Agendamento Higienizacao")]
        AgendamentoHigienizacao = 8,

    }
}
