using AgendaE.Common.Enum.App.HigienizadorEstofados;
using Domain.Entity.Cadastro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.App.HigienizadorEstofados
{
    [Table("SolicitacaoOrcamentoHE")]
    public class SolicitacaoOrcamentoHE
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public DateTime DataSolicitacao { get; set; } = DateTime.Now;
        public string NomeCliente { get; set; }
        public string Telefone { get; set; }
        public string? Email { get; set; }
        public StatusOrcamentoHEEnum Status { get; set; } = StatusOrcamentoHEEnum.Pendente;
        public string? Observacoes { get; set; }
        public int UsuarioId { get; set; }
        public int EnderecoId { get; set; }

        public virtual EnderecoCliente Endereco { get; set; }

        public virtual List<FotosOrcamentoHE>? Fotos { get; set; } = new List<FotosOrcamentoHE>(); // URLs ou caminhos das fotos

        public virtual Usuario Usuario { get; set; }

        public virtual List<ItemHigienizacaoHE> Itens { get; set; } = new List<ItemHigienizacaoHE>();
    }
}
