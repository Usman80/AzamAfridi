using System.ComponentModel.DataAnnotations;

namespace AzamAfridi.Models
{
    public class LoginVm
    {
        [Required(ErrorMessage = "Required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
