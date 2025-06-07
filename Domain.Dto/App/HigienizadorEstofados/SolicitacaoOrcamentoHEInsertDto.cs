using AgendaE.Common.Enum.App.HigienizadorEstofados;
using Domain.Dto.Usuario;
using Domain.Entity.App.HigienizadorEstofados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.App.HigienizadorEstofados
{
    public class SolicitacaoOrcamentoHEInsertDto
    {
        [Required]
        public string NomeCliente { get; set; }

        [Required]
        public string Telefone { get; set; }

        public string? Email { get; set; }

        public string? Observacoes { get; set; }

        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public string Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public List<ItemHigienizacaoHEInsertDto>? ItensHigianizacao { get; set; }

        public List<FotosOrcamentoHEInsertDto>? Imagens { get; set; }


        public static implicit operator SolicitacaoOrcamentoHE(SolicitacaoOrcamentoHEInsertDto dto)
        {
            return new SolicitacaoOrcamentoHE()
            {
                DataSolicitacao = DateTime.Now,
                Email = dto.Email,
                Endereco = new Entity.App.EnderecoCliente()
                {
                    Bairro = dto.Bairro,
                    CEP = dto.CEP,
                    Cidade = dto.Cidade,
                    Complemento = dto.Complemento,
                    Estado = dto.Estado,
                    Logradouro = dto.Logradouro,
                    Numero = dto.Numero,
                },
                Fotos = dto.Imagens != null && dto.Imagens.Count > 0 ? dto.Imagens.Select(s => (FotosOrcamentoHE)s).ToList() : null,
                Itens = dto.ItensHigianizacao != null && dto.ItensHigianizacao.Count > 0 ? dto.ItensHigianizacao.Select(s=> (ItemHigienizacaoHE)s).ToList() : new List<ItemHigienizacaoHE>(),
                Observacoes = dto.Observacoes,
                NomeCliente = dto.NomeCliente,
                Telefone = dto.Telefone,
                UsuarioId = dto.UsuarioId,
            };
        }
    }
}
