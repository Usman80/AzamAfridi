using AzamAfridi.Data;
using AzamAfridi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzamAfridi.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly AppDbContext _db;
        public ExpenseTypeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var list = _db.ExpenseTypes.ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ExpenseType model = new ExpenseType();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ExpenseType expenseType)
        {
            _db.ExpenseTypes.Add(expenseType);
            _db.SaveChanges();
            return Json(new { isSaved = true });
        }
        [HttpGet]
        public IActionResult EditExpense(int ExpenseType)
        {
            var data = _db.ExpenseTypes.Where(x => x.ExpenseTypeId == ExpenseType).FirstOrDefault();
            if (data != null)
            {
                return View(data);
            }
            return View();
        }
        [HttpPost]
        public IActionResult EditExpense(ExpenseType model)
        {
            var data = _db.Set<ExpenseType>().SingleOrDefault(s => s.ExpenseTypeId == model.ExpenseTypeId);
            if (data != null)
            {
                data.ExpenseTypeCode = model.ExpenseTypeCode;
                data.ExpenseTypeDescription = model.ExpenseTypeDescription;
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
