using AgendaE.Common.Enum.App.HigienizadorEstofados;
using AgendaE.Common.Extension;
using Domain.Entity.App.HigienizadorEstofados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.App.HigienizadorEstofados
{
    public class SolicitacaoOrcamentoHEDto
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string Telefone { get; set; }
        public string? Email { get; set; }
        public string?Observacoes { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }        
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }        
        public string CEP { get; set; }
        public StatusOrcamentoHEEnum Status { get; set; }
        public string StatusDescricao { get { return Status.ToDescription(); } }
        public DateTime DataSolicitacao { get; set; }
        public List<ItemHigienizacaoHEDto> ItensHigianizacao { get; set; }
        public List<FotosOrcamentoHEDto>? Imagens { get; set; }

        public static implicit operator SolicitacaoOrcamentoHEDto(SolicitacaoOrcamentoHE entity)
        {
            return new SolicitacaoOrcamentoHEDto()
            {
                Id = entity.Id,
                DataSolicitacao = entity.DataSolicitacao,
                Status = entity.Status,
                Email = entity.Email,                
                Bairro = entity.Endereco.Bairro,
                CEP = entity.Endereco.CEP,
                Cidade = entity.Endereco.Cidade,
                Complemento = entity.Endereco.Complemento,
                Estado = entity.Endereco.Estado,
                Logradouro = entity.Endereco.Logradouro,
                Numero = entity.Endereco.Numero,
                Imagens = entity.Fotos != null && entity.Fotos.Count > 0 ? entity.Fotos.Select(s => (FotosOrcamentoHEDto)s).ToList() : null,
                ItensHigianizacao = entity.Itens.Select(s => (ItemHigienizacaoHEDto)s).ToList(),
                Observacoes = entity.Observacoes,
                NomeCliente = entity.NomeCliente,
                Telefone = entity.Telefone
            };
        }
    }
}
