using AzamAfridi.Data;
using AzamAfridi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AzamAfridi.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public ExpenseController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<RouteDetail> list;
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("ADMIN"))
            {
               list = await _db.RouteDetails.ToListAsync();
            }
            else
            {
               list = await _db.RouteDetails.Where(x=>x.Isbuilty!=true).ToListAsync();
            }
            
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var builty_No = "";
            var stations = await _db.StationNames
                .Select(x => new SelectListModel { Code = x.StationCode, Description = x.StationDescription })
                .ToListAsync();

            ViewData["StationDDL"] = new SelectList(stations, "Code", "Description");
            var expensetype = await _db.ExpenseTypes
                .Select(x => new SelectListModel { Code = x.ExpenseTypeId.ToString(), Description = x.ExpenseTypeDescription })
                .ToListAsync();

            ViewData["ExpenseTypeDDL"] = new SelectList(expensetype, "Code", "Description");

            var lastBuiltyNo = await _db.RouteDetails
            .OrderByDescending(r => r.BuiltyNo)
            .Select(r => r.BuiltyNo)
            .FirstOrDefaultAsync();
            if (!string.IsNullOrEmpty(lastBuiltyNo) && int.TryParse(lastBuiltyNo, out int lastBuiltyNoInt))
            {
                builty_No = (lastBuiltyNoInt + 1).ToString("D10");
            }
            else
            {
                builty_No = "0000000000";
            }

            //PopulateStationName();
            RouteDetail Model = new RouteDetail();
            Model.Expenses = new List<ExpenseOnRoute>();
            Model.BuiltyNo = builty_No;
            Model.Expenses.Add(new ExpenseOnRoute());
            return View(Model);
        }

        public async Task<IActionResult> ExpenseOnRoute(ExpenseOnRoute Model)
        {
            var expensetype = await _db.ExpenseTypes
                .Select(x => new SelectListModel { Code = x.ExpenseTypeId.ToString(), Description = x.ExpenseTypeDescription })
                .ToListAsync();

            ViewData["ExpenseTypeDDL"] = new SelectList(expensetype, "Code", "Description");

            if (Model != null && Model.ExpenseOnRouteID >= 1)
            {
                var ViewModel = _db.ExpenseOnRoutes.Where(x => x.ExpenseOnRouteID == Model.ExpenseOnRouteID).FirstOrDefault();
                if (ViewModel != null && ViewModel.ExpenseOnRouteID > 0)
                {
                    return PartialView(ViewModel);
                }
            }
            Model = new ExpenseOnRoute();
            return PartialView(Model);
        }

        public async Task<IActionResult> VehicleMaintance(Vehicle_Maintance Model)
        {
            if (Model != null && Model.VehicleMaintanceId >= 1)
            {
                var ViewModel = await _db.Maintance_Vehicles.Where(x => x.VehicleMaintanceId == Model.VehicleMaintanceId).FirstOrDefaultAsync();
                if (ViewModel != null && ViewModel.VehicleMaintanceId > 0)
                {
                    return PartialView(ViewModel);
                }
            }
            Model = new Vehicle_Maintance();
            return PartialView(Model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBuilty(RouteDetail Model)
        {
            if (string.IsNullOrEmpty(Model.BuiltyNo))
            {
                return Json(new { isSaved = false });
            }
            var isAlreadyExist = await _db.RouteDetails.Where(x => x.BuiltyNo == Model.BuiltyNo).FirstOrDefaultAsync();
            if (isAlreadyExist != null && !string.IsNullOrEmpty(isAlreadyExist.BuiltyNo))
            {
                return Json(new { isSaved = false, isAlreadyExist = true });
            }
            var RouteId = await _db.RouteDetails.AddAsync(Model);
            _db.SaveChanges();
            return Json(new { isSaved = true });
        }

        public async Task<IActionResult> Edit(string builtyNo)
        {
            var stations = await _db.StationNames
                .Select(x => new SelectListModel { Code = x.StationCode, Description = x.StationDescription })
                .ToListAsync();

            ViewData["StationDDL"] = new SelectList(stations, "Code", "Description");
            var expensetype = await _db.ExpenseTypes
                .Select(x => new SelectListModel { Code = x.ExpenseTypeId.ToString(), Description = x.ExpenseTypeDescription })
                .ToListAsync();

            ViewData["ExpenseTypeDDL"] = new SelectList(expensetype, "Code", "Description");
            if (!string.IsNullOrEmpty(builtyNo))
            {
                var Model = await _db.RouteDetails.Where(x => x.BuiltyNo == builtyNo).FirstOrDefaultAsync();
                if (Model != null)
                {
                    if (Model.Expenses == null)
                    {
                        var lstExpenseOnRoute = await _db.ExpenseOnRoutes.Where(x => x.RouteDetail.RouteID == Model.RouteID).ToListAsync();
                        Model.Expenses = new List<ExpenseOnRoute>();
                        Model.Expenses = lstExpenseOnRoute;
                    }
                    return View(Model);
                }
            }
            RouteDetail ViewModel = new RouteDetail();
            ViewModel.Expenses = new List<ExpenseOnRoute>();
            ViewModel.Expenses.Add(new ExpenseOnRoute());
            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBuilty(RouteDetail Model)
        {
            if (string.IsNullOrEmpty(Model.BuiltyNo))
            {
                return Json(new { isSaved = false });
            }
            var isAlreadyExist = await _db.Set<RouteDetail>().SingleOrDefaultAsync(s => s.RouteID == Model.RouteID);
            //var isAlreadyExist = _db.RouteDetails.Where(x => x.RouteID == Model.RouteID).FirstOrDefault();
            if (isAlreadyExist != null && !string.IsNullOrEmpty(isAlreadyExist.BuiltyNo))
            {
                isAlreadyExist.BuiltyNo = Model.BuiltyNo;
                isAlreadyExist.DriveName = Model.DriveName;
                isAlreadyExist.TruckNo = Model.TruckNo;
                isAlreadyExist.Start_Date = Model.Start_Date;
                isAlreadyExist.Weight = Model.Weight;
                isAlreadyExist.FromStation = Model.FromStation;
                isAlreadyExist.ToStation = Model.ToStation;
                isAlreadyExist.FromFare = Model.FromFare;
                isAlreadyExist.Return_Date = Model.Return_Date;
                isAlreadyExist.Return_Weight = Model.Return_Weight;
                isAlreadyExist.Return_FromStation = Model.Return_FromStation;
                isAlreadyExist.Return_ToStation = Model.Return_ToStation;
                isAlreadyExist.ToFare = Model.ToFare;
                isAlreadyExist.TotalExpense = Model.TotalExpense;
                isAlreadyExist.TotalFare = Model.TotalFare;
                isAlreadyExist.TotalIncome = Model.TotalIncome;
                if (isAlreadyExist.Expenses == null)
                {
                    var lstExpenseOnRoute = await _db.ExpenseOnRoutes.Where(x => x.RouteDetail.RouteID == Model.RouteID).ToListAsync();
                    isAlreadyExist.Expenses = new List<ExpenseOnRoute>();
                    isAlreadyExist.Expenses = lstExpenseOnRoute;
                }
                if (isAlreadyExist.Expenses != null && isAlreadyExist.Expenses.Count() > 0)
                {
                    //Update Old Records
                    foreach (var expense in isAlreadyExist.Expenses)
                    {
                        var updateExpense = Model.Expenses.Where(x => x.ExpenseOnRouteID == expense.ExpenseOnRouteID).FirstOrDefault();
                        if (updateExpense != null)
                        {
                            expense.ExpenseType = updateExpense.ExpenseType;
                            expense.RouteDetail = updateExpense.RouteDetail;
                            expense.ExpenseTypeId = updateExpense.ExpenseTypeId;
                            expense.Amount = updateExpense.Amount;
                            expense.ExpenseOnRouteID = updateExpense.ExpenseOnRouteID;
                            Model.Expenses.Remove(updateExpense);
                        }
                    }
                    //Add New Records
                    if (Model.Expenses != null && Model.Expenses.Count() > 0)
                    {
                        foreach (var expense in Model.Expenses)
                        {
                            expense.RouteDetail = isAlreadyExist;
                            isAlreadyExist.Expenses.Add(expense);
                        }
                    }
                }
                _db.Update(isAlreadyExist);
                if (isAlreadyExist.Expenses != null && isAlreadyExist.Expenses.Count() > 0)
                {
                    foreach (var data in isAlreadyExist.Expenses)
                    {
                        await _db.ExpenseOnRoutes.AddAsync(data);
                    }
                }
                await _db.SaveChangesAsync();
                return Json(new { isSaved = true });
            }
            else
            {
                return Json(new { isSaved = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExpense(string builtyNo)
        {
            var Model = await _db.RouteDetails.Where(x => x.BuiltyNo == builtyNo).FirstOrDefaultAsync();
            if (Model != null)
            {
                Model.Isbuilty = true;
                _db.Update(Model);
                await _db.SaveChangesAsync();
                return Json(new { isSaved = true });
            }
            return Json(new { isSaved = false });
        }

        [HttpGet]
        public async Task<IActionResult> ExpenseDetail(string BuiltyNo, int RouteId)
        {
            var routeDetailsList = await (
            from rd in _db.RouteDetails
            join sn in _db.StationNames on rd.FromStation equals sn.StationCode
            where rd.BuiltyNo == BuiltyNo && rd.RouteID == RouteId && rd.Isbuilty == false
            select new RouteDetailsModel
            {
                RouteID = rd.RouteID,
                BuiltyNo = rd.BuiltyNo,
                DriveName = rd.DriveName,
                TruckNo = rd.TruckNo,
                StartDate = rd.Start_Date,
                Weight = rd.Weight,
                FromStation = sn.StationDescription,
                ToStation = _db.StationNames.FirstOrDefault(x => x.StationCode == rd.ToStation).StationDescription,
                FromFare = Convert.ToDecimal(rd.FromFare),
                ReturnDate = rd.Return_Date,
                ReturnWeight = rd.Return_Weight,
                ReturnFromStation = _db.StationNames.FirstOrDefault(x => x.StationCode == rd.Return_FromStation).StationDescription,
                ReturnToStation = _db.StationNames.FirstOrDefault(x => x.StationCode == rd.Return_ToStation).StationDescription,
                ToFare = Convert.ToDecimal(rd.ToFare),
                TotalFare = Convert.ToDecimal(rd.TotalFare),
                TotalExpense = Convert.ToDecimal(rd.TotalExpense),
                TotalIncome = Convert.ToDecimal(rd.TotalIncome),
                TotalMaintance = Convert.ToDecimal(rd.TotalMaintance)
            }
            ).FirstOrDefaultAsync();
            var obj = new ExpenseDetailModel();
            if (routeDetailsList != null)
            {
                var expenses = await (
                from a in _db.ExpenseOnRoutes
                join b in _db.ExpenseTypes on a.ExpenseTypeId equals b.ExpenseTypeId
                where a.RouteDetail.BuiltyNo == routeDetailsList.BuiltyNo
                select new ExpenseModel
                {
                    ExpenseTypeDescription = b.ExpenseTypeDescription,
                    ExpenseAmount = Convert.ToDecimal(a.Amount),
                    Expense_Date = a.Expense_Date
                }
            ).ToListAsync();

                var vehicle_Maintaince = await (
                    from a in _db.Maintance_Vehicles
                    where a.RouteDetail.BuiltyNo.Contains(routeDetailsList.BuiltyNo)
                    select new Vehicle_MaintanceModel
                    {
                        Maintance_Description = a.Maintance_Description,
                        Maintance_Price = Convert.ToDecimal(a.Maintance_Price),
                        Maintance_Date = a.Maintance_Date
                    }
                ).ToListAsync();

                obj = new ExpenseDetailModel
                {
                    RouteDetails = routeDetailsList,
                    Expenses = expenses,
                    vch_mant = vehicle_Maintaince
                };
            }
            return View(obj);
        }

        [NonAction]
        private async void PopulateStationName()
        {
            var StationDLL = await _db.StationNames.ToListAsync();
            StationName stationName = new StationName() { StationId = 0, StationDescription = "Choose a Category" };
            StationDLL.Insert(0, stationName);
            ViewData["StationNameDLL"] = StationDLL;
        }
    }
}
