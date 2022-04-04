using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veripark.Models;

namespace Veripark.DAL.Abstract
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {
        public Task<List<Country>> GetCountries();
    }
}
