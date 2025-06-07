using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaE.DataAccess.Interface
{
    public interface IRepository<T>
    {
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
        Task<T?> GetById(int id);
        int RetornaId(T model);
    }
}
