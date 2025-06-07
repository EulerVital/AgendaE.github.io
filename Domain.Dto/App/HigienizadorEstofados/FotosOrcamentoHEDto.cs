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
    public class FotosOrcamentoHEDto
    {
        public int Id { get; set; }
        public int SolicitacaoId { get; set; }
        public string CaminhoFoto { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataUpload { get; set; }

        public static implicit operator FotosOrcamentoHEDto(FotosOrcamentoHE entity)
        {
            return new FotosOrcamentoHE()
            {
                Id = entity.Id,
                DataUpload = entity.DataUpload,
                CaminhoFoto = entity.CaminhoFoto,
                Descricao = entity.Descricao,
                SolicitacaoId = entity.SolicitacaoId,
            };
        }
    }
}
