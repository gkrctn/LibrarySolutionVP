using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PenaltyWeb.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Veripark.DAL.Abstract;
using Veripark.WebApp.Models;

namespace PenaltyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }


        public async Task<IActionResult> Index(double? penalty)
        {
            List<SelectListItem> countries = new List<SelectListItem>();

            foreach (var item in await _countryRepository.GetCountries())
            {
                countries.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            ViewBag.Countries = countries;

            if (penalty != default)
            {
                ViewBag.Penalty = penalty.GetValueOrDefault();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(BookingModel bookingModel)
        {
            var penalty = 0;
            var countries = await _countryRepository.GetCountries();

            var country = countries.FirstOrDefault(x => x.Name == bookingModel.CountryName);

            if (bookingModel.CheckoutDate.AddDays(10) > bookingModel.ReturnDate)
            {
                penalty = 0;
            }
            else
            {
                var time = bookingModel.ReturnDate.Subtract(bookingModel.CheckoutDate.AddDays(10));

                var holidayDates = 0;
                var currentDate = bookingModel.CheckoutDate.AddDays(10);
                for (int i = 0; i < time.Days; i++)
                {
                    if (country.Holidays.Any(x => x.HolidayDate == currentDate))
                    {
                        holidayDates++;
                    }
                    else if (country.Weekends.Any(x => x.DayOfWeek == (int)currentDate.DayOfWeek))
                    {
                        holidayDates++;
                    }

                    currentDate = currentDate.AddDays(1);
                }

                penalty = (time.Days - holidayDates) * 5;
            }

            return RedirectToAction("Index", new { penalty = penalty });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}