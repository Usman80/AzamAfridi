using AzamAfridi.Data;
using AzamAfridi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AzamAfridi.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly AppDbContext _db;
        public ExpenseController(AppDbContext db)
        {
                _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var stations = await _db.StationNames
                .Select(x => new SelectListModel { Code = x.StationCode, Description = x.StationDescription })
                .ToListAsync();

            ViewData["StationDDL"] = new SelectList(stations, "Code", "Description");
            //PopulateStationName();

            return View(new RouteDetail());
        }

        [NonAction]
        public void PopulateStationName()
        {
            var StationDLL = _db.StationNames.ToList();
            StationName stationName = new StationName() { StationId = 0, StationDescription = "Choose a Category" };
            StationDLL.Insert(0, stationName);
            ViewData["StationNameDLL"] = StationDLL; 
        }
    }
}
