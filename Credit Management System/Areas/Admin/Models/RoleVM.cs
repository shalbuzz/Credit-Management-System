using System.ComponentModel.DataAnnotations;

namespace Credit_Management_System.Areas.Admin.Models
{
    public class RoleVM
    {
        [Required(ErrorMessage = "Role ID is required.")]
        [RegularExpression(@"^[a-zA-Z0-9\-]+$", ErrorMessage = "Role ID can only contain letters, numbers, or hyphens.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Role name is required.")]
        [StringLength(50, ErrorMessage = "Role name cannot exceed 50 characters.")]
        public string Name { get; set; }

    }
}
