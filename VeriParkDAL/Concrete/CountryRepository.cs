using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veripark.DAL.Abstract;
using Veripark.Models;

namespace Veripark.DAL.Concrete
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(DbContextOptions<MyContext> options) : base(options)
        {
        }
        public async Task<List<Country>> GetCountries()
        {
            return await _MyContext.Countries
                .Include(x => x.Weekends)
                .Include(x => x.Holidays).ToListAsync();
        }
    }
}
