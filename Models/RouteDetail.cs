using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzamAfridi.Models
{
    public class RouteDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RouteID              { get; set; }
        [Key]
        [Required]
        public string BuiltyNo          { get; set; }
        [Required]
        public string DriveName         { get; set; }
        [Required]
        public string TruckNo           { get; set; }
        [Required]
        public DateTime Start_Date      { get; set; }
        [Required]
        public int Weight               { get; set; }
        [Required]
        public string FromStation       { get; set; }
        [Required]
        public string ToStation         { get; set; }
        [Required]
        public double FromFare          { get; set; }
        [Required]
        public DateTime Return_Date     { get; set; }
        [Required]
        public int Return_Weight        { get; set; }
        [Required]
        public string Return_FromStation { get; set; }
        [Required]
        public string Return_ToStation  { get; set; }
        [Required]
        public double ToFare            { get; set; }
        public double TotalFare         { get; set; }
        public double TotalExpense      { get; set; }
        public double TotalIncome       { get; set; }
        public bool Isbuilty {  get; set; } 
        public List<ExpenseOnRoute> Expenses { get; set; }
        public List<Vehicle_Maintance> Vehicles { get; set; }
    }
}
