using Domain.Entity.App;
using Domain.Entity.App.HigienizadorEstofados;
using Domain.Entity.Cadastro;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.DataAccess.Base
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoApp> TipoApps { get; set; }
        public DbSet<EnderecoCliente> EnderecoClientes { get; set; }
        public DbSet<ConfiguracaoAppUsuario> ConfiguracaoAppUsuarios { get; set; }
        public DbSet<GaleriaAppUsuario> GaleriaAppUsuarios { get; set; }
        public DbSet<SolicitacaoOrcamentoHE> SolicitacaoOrcamentoHEs { get; set; }
        public DbSet<ItemHigienizacaoHE> ItemHigienizacaoHEs { get; set; }
        public DbSet<FotosOrcamentoHE> FotosOrcamentoHEs { get; set; }
        
    }
}
