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
    public class FotosOrcamentoHEInsertDto
    {
        public int? SolicitacaoId { get; set; }

        [Required]
        public string CaminhoFoto { get; set; }

        public string? Descricao { get; set; }

        public static implicit operator FotosOrcamentoHE(FotosOrcamentoHEInsertDto dto)
        {
            return new FotosOrcamentoHE()
            {
                DataUpload = DateTime.Now,
                CaminhoFoto = dto.CaminhoFoto,
                Descricao = dto.Descricao,
                SolicitacaoId = dto.SolicitacaoId ?? 0,
            };
        }
    }
}
