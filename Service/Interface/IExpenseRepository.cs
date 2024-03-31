using AzamAfridi.Models;

namespace AzamAfridi.Service.Interface
{
    public interface IExpenseRepository
    {
        Task<List<RouteDetail>> GetRouteDetailsByTruckNoAsync(string truckNumber);
        Task<List<ExpenseGroupedByType>> GetExpenseByRouteDetailsAsync(List<RouteDetail> routeDetails, int expenseTypeId,DateTime StartDate, DateTime EndDate);
    }
}
