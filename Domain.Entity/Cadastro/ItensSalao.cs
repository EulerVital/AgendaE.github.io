using AgendaE.Common.Enum;
using AgendaE.Common.Enum.App.Salao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Cadastro
{
    [Table("ItensSalao")]
    public class ItensSalao : Itens<StatusAgendaSalaoEnum>
    {
        [Column("Tipo")]
        public ItensTipoEnum Tipo { get; set; }

        [Column("DuracaoEstimada")]
        public long? DuracaoEstimada { get; set; }

        [NotMapped]
        public TimeSpan? ValidityPeriod
        {
            get { return DuracaoEstimada.HasValue ? TimeSpan.FromTicks(DuracaoEstimada.Value) : null; }
            set { DuracaoEstimada = value.HasValue ? value.Value.Ticks : null; }
        }
    }
}
