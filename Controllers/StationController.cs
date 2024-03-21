using AzamAfridi.Data;
using AzamAfridi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzamAfridi.Controllers
{
    public class StationController : Controller
    {
        private readonly AppDbContext _db;
        public StationController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var list = _db.StationNames.ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            StationName model = new StationName();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(StationName stationName)
        {
            _db.StationNames.Add(stationName);
            _db.SaveChanges();
            return Json(new { isSaved = true });
        }
        [HttpGet]
        public IActionResult EditStation(int StationId)
        {
            var data = _db.StationNames.Where(x => x.StationId == StationId).FirstOrDefault();
            if (data != null)
            {
                return View(data);
            }
            return View();
        }
        [HttpPost]
        public IActionResult EditStation(StationName model)
        {
            var data = _db.Set<StationName>().SingleOrDefault(s => s.StationId == model.StationId);
            if (data != null)
            {
                data.StationCode = model.StationCode;
                data.StationDescription = model.StationDescription;
                _db.Update(data);
                _db.SaveChanges();
                return Json(new { isSaved = true });
            }
            else
            {
                return Json(new { isSaved = false });
            }
        }
    }
}
