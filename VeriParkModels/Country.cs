using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veripark.Models
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Weekend> Weekends { get; set; }
        public List<Holiday> Holidays { get; set; }

    }
}
    
