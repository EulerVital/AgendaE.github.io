using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class ControleAgendamentoModel
    {
        public Guid Id { get; set; }

        public int CalendarId { get; set; }

        public int Status { get; set; }

        public int? MinutosPraFinalizar { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
