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
    public class ItemHigienizacaoHEInsertDto
    {
        public TipoItemHEEnum TipoItem { get; set; }
        public string? Descricao { get; set; }
        public int Quantidade { get; set; }
        public TamanhoItemHEEnum? Tamanho { get; set; }
        public string? Material { get; set; }
        public string? Cor { get; set; }
        public string? Observacoes { get; set; }

        public ItemHigienizacaoHEInsertDto()
        {
            Quantidade = 1;
        }

        public static implicit operator ItemHigienizacaoHE(ItemHigienizacaoHEInsertDto dto)
        {
            return new ItemHigienizacaoHE()
            {
                TipoItem = dto.TipoItem,
                Descricao = dto.Descricao,
                Quantidade = dto.Quantidade,
                Material = dto.Material,
                Cor = dto.Cor,
                Observacoes = dto.Observacoes,  
                Tamanho = dto.Tamanho
            };
        }
    }
}
