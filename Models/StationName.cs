using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AzamAfridi.Models
{
    public class StationName
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StationId { get; set; }
        [Required]
        public string StationCode { get; set; }
        [Required]
        public string StationDescription { get; set; }
        public bool IsStation {  get; set; }
    }
}
