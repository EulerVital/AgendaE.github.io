using AgendaE.Common.Enum.App.HigienizadorEstofados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.App.HigienizadorEstofados
{
    [Table("ItemHigienizacaoHE")]
    public class ItemHigienizacaoHE
    {
        [Key]
        public int Id { get; set; }
        public int SolicitacaoId { get; set; }
        public TipoItemHEEnum TipoItem { get; set; }
        public string? Descricao { get; set; }
        public int Quantidade { get; set; }
        public TamanhoItemHEEnum? Tamanho { get; set; }
        public string? Material { get; set; }
        public string? Cor { get; set; }
        public string? Observacoes { get; set; }

        [NotMapped]
        public virtual SolicitacaoOrcamentoHE SolicitacaoOrcamento { get; set; }

        [NotMapped]
        public virtual List<FotosOrcamentoHE>? FotosOrcamentoHEs { get; set; }

    }
}
