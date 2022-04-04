using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Veripark.Models
{
    public class Holiday
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public DateTime HolidayDate { get; set; }
    }
}
