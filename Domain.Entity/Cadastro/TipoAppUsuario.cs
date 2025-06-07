using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Cadastro
{
    [Table("TipoAppUsuario")]
    public class TipoAppUsuario
    {

        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("TipoAppId")]
        public int TipoAppId { get; set; }

        [Column("UsuarioId")]
        public int UsuarioId { get; set; }

        [Column("DataCriacao")]
        public DateTime DataCriacao { get; set; }

        [Column("Inativo")]
        public bool Inativo { get; set; }
    }
}
