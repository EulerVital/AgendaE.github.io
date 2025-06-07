using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.App.HigienizadorEstofados
{
    [Table("FotosOrcamentoHE")]
    public class FotosOrcamentoHE
    {
        [Key]
        public int Id { get; set; }
        public int SolicitacaoId { get; set; }
        public string CaminhoFoto { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataUpload { get; set; }

        [NotMapped]
        public virtual SolicitacaoOrcamentoHE SolicitacaoOrcamento { get; set; }

        [NotMapped]
        public virtual ItemHigienizacaoHE? ItemHigienizacao { get; set; }

    }
}
