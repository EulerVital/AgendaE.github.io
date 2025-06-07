using AgendaE.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Cadastro
{
    public class Itens<T> where T : Enum 
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Status")]
        public T Status { get; set; }

        [Column("Nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("Descricao")]
        public string? Descricao { get; set; }

        [Column("Preco")]
        public decimal? Preco { get; set; }
    }
}
