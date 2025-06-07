using AgendaE.Common.Enum.Cadastro;
using AgendaE.Common.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Usuario
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string UltimoNome { get; set; }

        public string Celular { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public string StatusDescricao { get { return Status.ToDescription(); } }
        public string? Codigo { get; set; }

        public UsuarioStatusEnum Status { get; set; }

        public static implicit operator UsuarioDto(Entity.Cadastro.Usuario entity)
        {
            return new UsuarioDto()
            {
                Id = entity.Id,
                Nome = entity.Nome,
                UltimoNome = entity.UltimoNome,
                Celular = entity.Celular,
                Cpf = entity.Cpf,
                Email = entity.Email,
                Status = entity.Status
            };
        }

        public static implicit operator Entity.Cadastro.Usuario(UsuarioDto entity)
        {
            return new Entity.Cadastro.Usuario()
            {
                Id = entity.Id,
                Nome = entity.Nome,
                UltimoNome = entity.UltimoNome,
                Celular = entity.Celular,
                Cpf = entity.Cpf,
                Email = entity.Email,
                Status = entity.Status
            };
        }
    }
}
