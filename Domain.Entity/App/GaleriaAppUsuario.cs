using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.App
{
    public class GaleriaAppUsuario
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public string Descricao { get; set; }
        public string Legenda { get; set; }
        public int ConfiguracaoAppUsuarioId { get; set; }
        public bool Inativo { get; set; }
        public DateTime DataCriacao { get; set; }

        // Propriedade de navegação para o relacionamento
        public virtual ConfiguracaoAppUsuario ConfiguracaoAppUsuario { get; set; }
    }
}
