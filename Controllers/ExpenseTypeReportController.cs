using AzamAfridi.Data;
using AzamAfridi.Models;
using AzamAfridi.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AzamAfridi.Controllers
{
    [Authorize]
    public class ExpenseTypeReportController : Controller
    {
        private readonly AppDbContext _db;
        public ExpenseTypeReportController(AppDbContext dbContext)
        {
                _db = dbContext;
        }
        public async Task<IActionResult> Index() 
        {
            var expensetype = await _db.ExpenseTypes
               .Select(x => new SelectListModel { Code = x.ExpenseTypeId.ToString(), Description = x.ExpenseTypeDescription })
               .ToListAsync();

            ViewData["ExpenseTypeDDL"] = new SelectList(expensetype, "Code", "Description");
            return View();   
        }
        public async Task<IActionResult> ExpenseTypeReport(string truckNumber,int expenseTypeId, DateTime StartDate, DateTime EndDate)
        {
            var builtyNosQuery = _db.RouteDetails
                .Where(route => route.TruckNo == truckNumber && route.Isbuilty == false)
                .Select(route => route.BuiltyNo);
            var expenses = await (from expense in _db.ExpenseOnRoutes
                            join expenseType in _db.ExpenseTypes on expense.ExpenseTypeId equals expenseType.ExpenseTypeId
                            where builtyNosQuery.Contains(expense.RouteDetail.BuiltyNo)
                                && expense.ExpenseTypeId == expenseTypeId
                                && expense.Expense_Date >= StartDate
                                && expense.Expense_Date <= EndDate
                            select new ExpenseGroupedByType
                            {
                                ExpenseTypeDescription = expenseType.ExpenseTypeDescription,
                                TotalExpenseAmount = expense.Amount,
                                ExpenseDate = expense.Expense_Date,
                                DieselLitre = expense.DieselLitre
                            }).ToListAsync();
            return PartialView("_ExpenseTypeReportPartial", expenses);
        }
    }
}
