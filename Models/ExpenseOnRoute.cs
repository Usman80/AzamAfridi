using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AzamAfridi.Models
{
    public class ExpenseOnRoute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseOnRouteID { get; set; }
        
        [ForeignKey("RouteID")]
        public virtual int RouteID { get; set; }
        public virtual RouteDetail RouteDetail { get; set; }

        [ForeignKey("ExpenseTypeId")]
        public int ExpenseTypeId { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }

        public double Amount { get; set; }
    }
}
