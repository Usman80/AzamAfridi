using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AzamAfridi.Models
{
    public class ExpenseType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseTypeId { get; set; }
        [Required]
        public string ExpenseTypeCode { get; set; }
        [Required]
        public string ExpenseTypeDescription { get; set; }
        [Required]
        public DateTime Expense_Date { get; set; }
        public bool IsExpenseType {  get; set; }
    }
}
