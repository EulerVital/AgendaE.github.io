using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Cadastro
{
    [Table("TipoApp")]
    public class TipoApp
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Codigo")]
        public int Codigo { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Descricao")]
        public string? Descricao { get; set; }

        [Column("UrlImagem")]
        public string? UrlImagem { get; set; }

        [Column("Inativo")]
        public bool Inativo { get; set; }
    }
}
