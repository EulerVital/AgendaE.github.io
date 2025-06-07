using Domain.Entity.Cadastro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.App
{
    [Table("ConfiguracaoAppUsuario")]
    public class ConfiguracaoAppUsuario
    {
        [Key]
        public int Id { get; set; }
        public int TipoAppId { get; set; }
        public string Codigo { get; set; }
        public int UsuarioId { get; set; }
        public int Status { get; set; }
        public string LinkApp { get; set; }
        public string LogoApp { get; set; }
        public DateTime DataCriacao { get; set; }

        // Propriedades de navegação para os relacionamentos
        public virtual TipoApp TipoApp { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<GaleriaAppUsuario> Galerias { get; set; }
    }
}
