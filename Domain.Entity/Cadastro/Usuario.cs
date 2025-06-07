using AgendaE.Common.Enum.Cadastro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Cadastro
{
    [Table("Usuario")]
    public class Usuario
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("UltimoNome")]
        public string UltimoNome { get; set; }

        [Column("Celular")]
        public string Celular { get; set; }

        [Column("Senha")]
        public string Senha { get; set; }

        [Column("Cpf")]
        public string Cpf { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Status")]
        public UsuarioStatusEnum Status { get; set; }
    }
}
