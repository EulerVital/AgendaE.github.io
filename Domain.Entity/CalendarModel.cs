using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity 
{
    public class CalendarModel
    {
        public Guid Id { get; set; }

        public DateTime Data { get; set; }

        public string? Celular { get; set; }

        public string? Nome { get; set; }

        public DateTime DataCriacao { get; set; }

        public int Status { get; set; }
    }
}
