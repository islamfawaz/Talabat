using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.Dashboard.Models
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(255)]
        public string Name { get; set; } = null!;
    }
}
