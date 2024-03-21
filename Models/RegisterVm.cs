using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AzamAfridi.Models
{
    public class RegisterVm
    {
        [Required]
        public string? Name {  get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password {  get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password don't Match")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword {  get; set; }
    }
}
