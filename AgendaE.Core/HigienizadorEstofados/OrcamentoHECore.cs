using AgendaE.Core.Helper;
using AgendaE.Core.Interface.HigienizadorEstofados;
using AgendaE.DataAccess;
using AgendaE.DataAccess.Base;
using AgendaE.DataAccess.HigienizadorEstofados;
using AgendaE.DataAccess.Interface;
using Domain.Entity.App.HigienizadorEstofados;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.Core.HigienizadorEstofados
{
    public class OrcamentoHECore : BaseService, IOrcamentoHECore
    {
        public OrcamentoHECore(ApplicationDbContext context, IDapperData dapperContext, IConfiguration configuration) : base(context, dapperContext, configuration)
        {
        }

        public async Task<SolicitacaoOrcamentoHE?> Insert(SolicitacaoOrcamentoHE entity)
        {
            int solicitacao = 0;

            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dao = new SolicitacaoOrcamentoHEDao(_context, _dapperContext);
                    var daoEndeco = new EnderecoClienteDao(_context, _dapperContext);
                    var daoItens = new ItemHigienizacaoHEDao(_context, _dapperContext);
                    var daoFotos = new FotosOrcamentoHEDao(_context, _dapperContext);

                    //Por enquanto
                    entity.UsuarioId = 1;

                    if (entity.Endereco == null)
                        return null;

                    entity.Endereco.Id = await daoEndeco.Add(entity.Endereco);
                    entity.EnderecoId = entity.Endereco.Id;

                    solicitacao = await dao.Add(entity);

                    if(solicitacao <= 0)
                    {
                        await trans.RollbackAsync();
                        return null;
                    }

                    foreach(var item in entity.Itens)
                    {
                        if (item == null)
                            continue;

                        item.SolicitacaoId = solicitacao;

                        if (await daoItens.Add(item) <= 0)
                        { 
                            await trans.RollbackAsync(); 
                            return null; 
                        }
                    }

                    if (entity.Fotos != null)
                    {
                        foreach (var item in entity.Fotos)
                        {
                            if (item == null)
                                continue;

                            item.SolicitacaoId = solicitacao;

                            if (await daoFotos.Add(item) <= 0)
                            {
                                await trans.RollbackAsync();
                                return null;
                            }
                        }
                    }

                    await trans.CommitAsync();

                }
                catch (Exception)
                {
                    await trans.RollbackAsync();
                    throw;
                }
            }

            return await GetByCompletoId(solicitacao);
        }

        public async Task<SolicitacaoOrcamentoHE?> GetById(int id)
        {
            var dao = new SolicitacaoOrcamentoHEDao(_context, _dapperContext);
            return await dao.GetById(id);
        }

        public async Task<SolicitacaoOrcamentoHE?> GetByCompletoId(int id)
        {
            var dao = new SolicitacaoOrcamentoHEDao(_context, _dapperContext);
            var daoItens = new ItemHigienizacaoHEDao(_context, _dapperContext);
            var daoFotos = new FotosOrcamentoHEDao(_context, _dapperContext);
            
            var solicitacao = await dao.GetById(id);

            if (solicitacao == null)
                return null;

            solicitacao.Itens = await daoItens.ListPorSolicitacao(solicitacao.Id);
            solicitacao.Fotos = await daoFotos.ListPorSolicitacao(solicitacao.Id);

            return solicitacao;
        }
    }
}
