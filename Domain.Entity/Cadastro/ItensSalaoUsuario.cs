using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Cadastro
{
    [Table("ItensSalaoUsuario")]
    public class ItensSalaoUsuario
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("ItensSalaoId")]
        public int ItensSalaoId { get; set; }

        [Column("UsuarioId")]
        public int UsuarioId { get; set; }

        [Column("DataCriacao")]
        public DateTime DataCriacao { get; set; }

        [Column("Inativo")]
        public bool Inativo { get; set; }
    }
}
