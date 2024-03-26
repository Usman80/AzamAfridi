using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzamAfridi.Models
{
    public class Vehicle_Maintance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleMaintanceId { get; set; }
        [ForeignKey("RouteID")]
        public virtual int RouteID { get; set; }
        public virtual RouteDetail RouteDetail { get; set; }

        public string Maintance_Description { get; set; }
        public double Maintance_Price { get; set;}  
        public DateTime Maintance_Date { get; set; }
    }
}
