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
        public async Task<IActionResult> ExpenseTypeReport(string truckNumber,int expenseTypeId, DateTime StartDate, DateTime EndDate)
        {
            //var routeDetails = await _expense.GetRouteDetailsByTruckNoAsync(truckNumber);
            //var expenses = await _expense.GetExpenseByRouteDetailsAsync(routeDetails, expenseTypeId,StartDate,EndDate);

            //var viewModel = new ExpenseCombineType
            //{
            //    RouteDetails = routeDetails,
            //    Expenses = expenses
            //};
            //return PartialView("_ExpenseTypeReportPartial", viewModel);
            //var routeDetailsList = await (
            //from rd in _db.RouteDetails
            //join sn in _db.StationNames on rd.FromStation equals sn.StationCode
            //where rd.TruckNo == truckNumber && rd.Isbuilty == false
            //select new RouteDetailsModel
            //    {
            //        RouteID = rd.RouteID,
            //        BuiltyNo = rd.BuiltyNo,
            //        DriveName = rd.DriveName,
            //        TruckNo = rd.TruckNo,
            //        StartDate = rd.Start_Date,
            //        Weight = rd.Weight,
            //        FromStation = sn.StationDescription,
            //        ToStation = _db.StationNames.FirstOrDefault(x => x.StationCode == rd.ToStation).StationDescription,
            //        FromFare = Convert.ToDecimal(rd.FromFare),
            //        ReturnDate = rd.Return_Date,
            //        ReturnWeight = rd.Return_Weight,
            //        ReturnFromStation = _db.StationNames.FirstOrDefault(x => x.StationCode == rd.Return_FromStation).StationDescription,
            //        ReturnToStation = _db.StationNames.FirstOrDefault(x => x.StationCode == rd.Return_ToStation).StationDescription,
            //        ToFare = Convert.ToDecimal(rd.ToFare),
            //        TotalFare = Convert.ToDecimal(rd.TotalFare),
            //        TotalExpense = Convert.ToDecimal(rd.TotalExpense),
            //        TotalIncome = Convert.ToDecimal(rd.TotalIncome),
            //        TotalMaintance = Convert.ToDecimal(rd.TotalMaintance)
            //    }
            //).ToListAsync();
            //var combinedModelList = new List<CombinedModel>();
            //foreach (var routeDetails in routeDetailsList)
            //{
            //    var expenses = await (
            //        from a in _db.ExpenseOnRoutes
            //        join b in _db.ExpenseTypes on a.ExpenseTypeId equals b.ExpenseTypeId
            //        where a.RouteDetail.BuiltyNo == routeDetails.BuiltyNo && a.ExpenseTypeId == expenseTypeId && (a.Expense_Date>= StartDate 
            //           && a.Expense_Date <=EndDate)  
            //        select new ExpenseModel
            //        {
            //            ExpenseTypeDescription = b.ExpenseTypeDescription,
            //            ExpenseAmount = Convert.ToDecimal(a.Amount),
            //            Expense_Date = a.Expense_Date
            //        }
            //    ).ToListAsync();

            //    var combinedModel = new CombinedModel
            //    {
            //        RouteDetails = new List<RouteDetailsModel> { routeDetails },
            //        Expenses = expenses,
            //    };
            //    combinedModelList.Add(combinedModel);
            //}
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
                                ExpenseDate = expense.Expense_Date
                            }).ToListAsync();
            return PartialView("_ExpenseTypeReportPartial", expenses);
        }
    }
}
