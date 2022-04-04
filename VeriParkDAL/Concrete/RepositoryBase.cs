using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veripark.DAL.Abstract;

namespace Veripark.DAL.Concrete
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
     
        protected readonly MyContext _MyContext;

        public RepositoryBase(DbContextOptions<MyContext> options)
        {
            _MyContext = new MyContext(options);
        }
        public async Task<T> Add(T entity)
        {
            await _MyContext.Set<T>().AddAsync(entity);
            await _MyContext.SaveChangesAsync();

            return entity;

        }

        public async Task<T> Delete(int id)
        {
            var dbSet = _MyContext.Set<T>();

            var entity = await dbSet.FindAsync(id);
            dbSet.Remove(entity);

            await _MyContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Get(int id)
        {
            return await _MyContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _MyContext.Set<T>().ToListAsync();
        }
    }
}
