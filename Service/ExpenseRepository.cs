using AzamAfridi.Data;
using AzamAfridi.Models;
using AzamAfridi.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace AzamAfridi.Service
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _db;
        public ExpenseRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<ExpenseGroupedByType>> GetExpenseByRouteDetailsAsync(List<RouteDetail> routeDetails, int expenseTypeId, DateTime StartDate, DateTime EndDate)
        {
            var routeIds = routeDetails.Select(rd => rd.RouteID).ToList();
            var builtyNos = routeDetails.Select(rd => rd.BuiltyNo).ToList();

            var result = await _db.ExpenseOnRoutes
                .Include(eor => eor.ExpenseType)
                .Where(eor => routeIds.Contains(eor.RouteID) && builtyNos.Contains(eor.RouteDetail.BuiltyNo) && eor.ExpenseType.ExpenseTypeId == expenseTypeId
                       && (eor.Expense_Date >=StartDate && eor.Expense_Date<=EndDate ))
                .GroupBy(eor => new { eor.ExpenseType.ExpenseTypeCode, eor.ExpenseType.ExpenseTypeDescription })
                .Select(g => new ExpenseGroupedByType
                {
                    ExpenseTypeCode = g.Key.ExpenseTypeCode,
                    ExpenseTypeDescription = g.Key.ExpenseTypeDescription,
                    TotalExpenseAmount = g.Sum(e => e.Amount)
                }).ToListAsync();
            return result;
        }

        public async Task<List<RouteDetail>> GetRouteDetailsByTruckNoAsync(string truckNumber)
        {
            return await _db.RouteDetails
            .Where(rd => rd.TruckNo == truckNumber && rd.Isbuilty == false)
            .ToListAsync();
        }
    }
}
