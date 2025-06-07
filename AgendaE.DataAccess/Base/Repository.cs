using AgendaE.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.DataAccess.Base
{
    public abstract class Repository<T> : BaseDao, IRepository<T> where T : class
    {
        public int RowsAffetds { get; set; }

        protected Repository(ApplicationDbContext context, IDapperData dapperData) : base(context, dapperData)
        {
            RowsAffetds = 0;
        }

        public virtual async Task<int> Add(T entity)
        {
            _context.Add(entity);
            RowsAffetds = await _context.SaveChangesAsync();
            return RetornaId(entity);
        }

        public async Task<int> Delete(T entity)
        {
            _context.Remove(entity);
            RowsAffetds = await _context.SaveChangesAsync();
            return RowsAffetds;
        }

        public abstract Task<T?> GetById(int id);
        public abstract Task<T?> GetByIdAsNoTracking(int id);

        public abstract int RetornaId(T model);

        public async Task<int> Update(T entity)
        {
            _context.Update(entity);
            RowsAffetds = await _context.SaveChangesAsync();
            return RetornaId(entity);
        }
    }
}
