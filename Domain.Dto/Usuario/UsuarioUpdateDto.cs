using AgendaE.Common.Enum.Cadastro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaE.Common.Extension;

namespace Domain.Dto.Usuario
{
    public class UsuarioUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string UltimoNome { get; set; }

        [Required]
        public string Celular { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Email { get; set; }

        public static implicit operator Entity.Cadastro.Usuario(UsuarioUpdateDto entity)
        {
            return new Entity.Cadastro.Usuario()
            {
                Id = entity.Id,
                Nome = entity.Nome,
                UltimoNome = entity.UltimoNome,
                Celular = entity.Celular,
                Cpf = entity.Cpf,
                Email = entity.Email,
            };
        }
    }
}
