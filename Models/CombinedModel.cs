namespace AzamAfridi.Models
{
    public class CombinedModel
    {
        public List<RouteDetailsModel> RouteDetails { get; set; }
        public List<ExpenseModel> Expenses { get; set; }
        public List<Vehicle_MaintanceModel> vch_mant { get; set; }
    }
}
