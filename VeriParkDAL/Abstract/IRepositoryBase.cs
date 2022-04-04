using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veripark.DAL.Abstract
{
    public interface IRepositoryBase<T> where T : class, new()
    {
        public Task<T> Add(T entity);

        public Task<T> Get(int id);

        public Task<T> Delete(int id);

        public Task<IEnumerable<T>> Get();
    }
}
