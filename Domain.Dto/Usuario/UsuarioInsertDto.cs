using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Usuario
{
    public class UsuarioInsertDto
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string UltimoNome { get; set; }

        [Required]
        public string Celular { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Email { get; set; }


        public static implicit operator Domain.Entity.Cadastro.Usuario(UsuarioInsertDto dto)
        {
            return new Entity.Cadastro.Usuario()
            {
                Nome = dto.Nome,
                UltimoNome = dto.UltimoNome,
                Celular = dto.Celular,
                Senha = dto.Senha,
                Cpf = dto.Cpf,
                Email = dto.Email
            };
        }
    }
}
