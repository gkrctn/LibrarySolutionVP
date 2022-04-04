using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veripark.Models
{
    public class Weekend
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public int DayOfWeek { get; set; }
    }
}
