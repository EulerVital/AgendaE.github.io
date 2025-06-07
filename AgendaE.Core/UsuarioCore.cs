using AgendaE.Core.Helper;
using AgendaE.Core.Interface;
using AgendaE.DataAccess.Base;
using AgendaE.DataAccess.Interface;
using Domain.Entity.Cadastro;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaE.Common.Extension;
using AgendaE.DataAccess;

namespace AgendaE.Core
{
    public class UsuarioCore : BaseService, IUsuarioCore
    {
        public UsuarioCore(ApplicationDbContext context, IDapperData dapperContext, IConfiguration configuration) : base(context, dapperContext, configuration)
        {
        }

        public async Task<int> Insert(Usuario model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException(nameof(model));

                return await new UsuarioDao(_context, _dapperContext).Add(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Update(Usuario model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException(nameof(model));

                var dao = new UsuarioDao(_context, _dapperContext);

                var obj = await dao.GetByIdAsNoTracking(model.Id);

                if (obj == null)
                    throw new ArgumentNullException(nameof(obj));

                model.Senha = obj.Senha;
                model.Status = obj.Status;

                return await dao.Update(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario?> GetById(int id)
        {
            try
            {
                var dao = new UsuarioDao(_context, _dapperContext);
                return await dao.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario?> GetPorCodigo(string codigo)
        {
            try
            {
                var dao = new UsuarioDao(_context, _dapperContext);
                return await dao.GetPorCodigo(codigo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario?> GetLogin(string email, string senha)
        {
            try
            {
                var dao = new UsuarioDao(_context, _dapperContext);
                return await dao.GetLogin(email, senha);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario?> GetByIdAsNoTracking(int id)
        {
            try
            {
                var dao = new UsuarioDao(_context, _dapperContext);
                return await dao.GetByIdAsNoTracking(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> RedefinirSenha(int id, string senha)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("Id inválido");

                var dao = new UsuarioDao(_context, _dapperContext);

                var obj = await dao.GetById(id);

                if (obj == null)
                    throw new ArgumentNullException(nameof(obj));

                obj.Senha = obj.Senha;

                return await dao.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
