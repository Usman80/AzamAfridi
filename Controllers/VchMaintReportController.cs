using AzamAfridi.Data;
using AzamAfridi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzamAfridi.Controllers
{
    [Authorize]
    public class VchMaintReportController : Controller
    {
        private readonly AppDbContext _db;
        public VchMaintReportController(AppDbContext dbContext)
        {
            _db = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ExpenseTypeReport(string truckNumber,DateTime StartDate, DateTime EndDate)
        {
            var builtyNosQuery = _db.RouteDetails
                .Where(route => route.TruckNo == truckNumber && route.Isbuilty == false)
                .Select(route => route.BuiltyNo);
            var vch_maintance = await (from vch in _db.Maintance_Vehicles
                                  where builtyNosQuery.Contains(vch.RouteDetail.BuiltyNo)
                                      && vch.Maintance_Date >= StartDate
                                      && vch.Maintance_Date <= EndDate
                                  select new ExpenseGroupedByType
                                  {
                                      VehicleMaintDescription = vch.Maintance_Description,
                                      VehicleMaintPrice = vch.Maintance_Price,
                                      VehicleMaintDate = vch.Maintance_Date
                                  }).ToListAsync();
            return PartialView("_MaintanceReportPartial", vch_maintance);
        }
    }
}
