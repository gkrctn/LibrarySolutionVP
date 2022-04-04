using Microsoft.EntityFrameworkCore;
using Nager.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Veripark.Models;

namespace Veripark.DAL.Concrete
{
    public class MyContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Weekend> Weekends { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var client = new HttpClient();

            var countryCodes = new List<CountryCode>()
            {
                CountryCode.TR,
                CountryCode.US
            };

            var countryList = new List<Country>();
            var countryHolidays = new List<Holiday>();
            var countryWeekends = new List<Weekend>();

            foreach (var countryCode in countryCodes)
            {
                var holidays = DateSystem.GetPublicHolidays(2021, countryCode).ToList();
                var weekendDates = DateSystem.GetWeekendProvider(countryCode).WeekendDays;

                var country = new Country
                {
                    Id = Guid.NewGuid(),
                    Name = countryCode.ToString(),
                };
                countryList.Add(country);

                foreach (var holidayInfo in holidays)
                {
                    var holiday = new Holiday
                    {
                        Id = Guid.NewGuid(),
                        CountryId = country.Id,
                        HolidayDate = holidayInfo.Date,
                    };
                    countryHolidays.Add(holiday);
                }

                foreach (var weekendDateInfo in weekendDates)
                {
                    var weekend = new Weekend
                    {
                        Id = Guid.NewGuid(),
                        CountryId = country.Id,
                        DayOfWeek = (int)weekendDateInfo,
                    };
                    countryWeekends.Add(weekend);
                }
            }

            modelBuilder.Entity<Country>().HasData(countryList);
            modelBuilder.Entity<Weekend>().HasData(countryWeekends);
            modelBuilder.Entity<Holiday>().HasData(countryHolidays);
        }
    }
}
