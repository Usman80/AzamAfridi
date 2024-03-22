using AzamAfridi.Data;
using AzamAfridi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzamAfridi.Controllers
{
    public class ExpenseReportController : Controller
    {
        private readonly AppDbContext _db;
        public ExpenseReportController(AppDbContext db)
        {   
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GenerateExpenseReport(string truckNo)
        {
            var routeDetailsList = await (
            from rd in _db.RouteDetails
                join sn in _db.StationNames on rd.FromStation equals sn.StationCode
                where rd.TruckNo == truckNo
                select new RouteDetailsModel
                {
                    RouteID             = rd.RouteID,
                    BuiltyNo            = rd.BuiltyNo,
                    DriveName           = rd.DriveName,
                    TruckNo             = rd.TruckNo,
                    StartDate           = rd.Start_Date,
                    Weight              = rd.Weight,
                    FromStation         = sn.StationDescription,
                    ToStation           = _db.StationNames.FirstOrDefault(x => x.StationCode == rd.ToStation).StationDescription,
                    FromFare            = Convert.ToDecimal(rd.FromFare),
                    ReturnDate          = rd.Return_Date,
                    ReturnWeight        = rd.Return_Weight,
                    ReturnFromStation   = _db.StationNames.FirstOrDefault(x => x.StationCode == rd.Return_FromStation).StationDescription,
                    ReturnToStation     = _db.StationNames.FirstOrDefault(x => x.StationCode == rd.Return_ToStation).StationDescription,
                    ToFare              = Convert.ToDecimal(rd.ToFare),
                    TotalFare           = Convert.ToDecimal(rd.TotalFare),
                    TotalExpense        = Convert.ToDecimal(rd.TotalExpense),
                    TotalIncome         = Convert.ToDecimal(rd.TotalIncome)
                }
            ).ToListAsync();
            var combinedModelList = new List<CombinedModel>();
            foreach (var routeDetails in routeDetailsList)
            {
                var expenses = await (
                    from a in _db.ExpenseOnRoutes
                    join b in _db.ExpenseTypes on a.ExpenseTypeId equals b.ExpenseTypeId
                    where a.RouteDetail.BuiltyNo == routeDetails.BuiltyNo
                    select new ExpenseModel
                    {
                        ExpenseTypeDescription = b.ExpenseTypeDescription,
                        ExpenseAmount = Convert.ToDecimal(a.Amount)
                    }
                ).ToListAsync();
                var combinedModel = new CombinedModel
                {
                    //RouteDetails = routeDetails,
                    RouteDetails = new List<RouteDetailsModel> { routeDetails },
                    Expenses = expenses
                };
                combinedModelList.Add(combinedModel);
            }
            //return View(combinedModelList);
            return PartialView("_ExpenseReportPartial", combinedModelList);
        }
    }
}
