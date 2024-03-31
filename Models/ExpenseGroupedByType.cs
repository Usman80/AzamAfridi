namespace AzamAfridi.Models
{
    public class ExpenseGroupedByType
    {
        public string ExpenseTypeCode { get; set; }
        public string ExpenseTypeDescription { get; set; }
        public double TotalExpenseAmount { get; set; }
        public DateTime ExpenseDate { get; set; }

        public string VehicleMaintanceId { get; set; } 
        public string VehicleMaintDescription { get; set; }
        public double VehicleMaintPrice { get; set; }
        public DateTime VehicleMaintDate { get; set; }

    }
}
