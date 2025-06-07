using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Common.Enum.App.Salao
{
    public enum StatusAgendaSalaoEnum
    {
        [Description("Solicitação de Agendamento")]
        SolicitaAgendamento = 0,

        [Description("Agendamento foi aceito")]
        AgendamentoAceito = 1,

        [Description("Pendência de pagamento parcial (sinal)")]
        PendenciaPagamentoParcial = 2,

        [Description("Pendência de pagamento")]
        PendenciaPagamento = 3,

        [Description("Agendado")]
        Agendado = 4,

        [Description("Solicitação de agendamento negada na data")]
        SolicitacaoAgendamentoNegadaData = 5,

        [Description("Solicitação de agendamento negada")]
        SolicitacaoAgendamentoNegada = 6,
    }
}
