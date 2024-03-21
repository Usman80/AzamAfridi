using AzamAfridi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzamAfridi.Controllers
{
    public class ExpenseReportController : Controller
    {
        public IActionResult Index()
        {
            RouteDetail Model = new RouteDetail();
            return View(Model);
        }
    }
}
