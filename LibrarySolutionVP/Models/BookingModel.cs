using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyWeb.Models
{
    public class BookingModel
    {
        public DateTime CheckoutDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
    }
}