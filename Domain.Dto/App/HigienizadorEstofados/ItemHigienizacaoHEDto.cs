using AgendaE.Common.Enum.App.HigienizadorEstofados;
using Domain.Entity.App.HigienizadorEstofados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.App.HigienizadorEstofados
{
    public class ItemHigienizacaoHEDto
    {
        public int Id { get; set; }
        public int SolicitacaoId { get; set; }
        public TipoItemHEEnum TipoItem { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public TamanhoItemHEEnum? Tamanho { get; set; }
        public string? Material { get; set; }
        public string? Cor { get; set; }
        public string? Observacoes { get; set; }

        public static implicit operator ItemHigienizacaoHEDto(ItemHigienizacaoHE entity)
        {
            return new ItemHigienizacaoHE()
            {
                Id = entity.Id,
                TipoItem = entity.TipoItem,
                Descricao = entity.Descricao,
                Quantidade = entity.Quantidade,
                Material = entity.Material,
                Cor = entity.Cor,
                Observacoes = entity.Observacoes,  
                Tamanho = entity.Tamanho
            };
        }
    }
}
