namespace AzamAfridi.Models
{
    public class RouteDetailsModel
    {
        public int RouteID              { get; set; }
        public string BuiltyNo { get; set; }
        public string DriveName { get; set; }
        public string TruckNo { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Weight { get; set; }
        public string FromStation { get; set; }
        public string ToStation { get; set; }
        public decimal FromFare { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal ReturnWeight { get; set; }
        public string ReturnFromStation { get; set; }
        public string ReturnToStation { get; set; }
        public decimal ToFare { get; set; }
        public decimal TotalFare { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
