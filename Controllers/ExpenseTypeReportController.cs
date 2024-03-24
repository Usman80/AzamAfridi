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
        private readonly IExpenseRepository _expense;
        private readonly AppDbContext _db;
        public ExpenseTypeReportController(IExpenseRepository expense, AppDbContext dbContext)
        {
                _expense = expense;
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
        public async Task<IActionResult> ExpenseTypeReport(string truckNumber,int expenseTypeId)
        {
            var routeDetails = await _expense.GetRouteDetailsByTruckNoAsync(truckNumber);
            var expenses = await _expense.GetExpenseByRouteDetailsAsync(routeDetails, expenseTypeId);

            var viewModel = new ExpenseCombineType
            {
                RouteDetails = routeDetails,
                Expenses = expenses
            };

            return View(viewModel);
        }
    }
}
