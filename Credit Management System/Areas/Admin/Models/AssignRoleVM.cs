namespace Credit_Management_System.Areas.Admin.Models
{
    public class AssignRoleVM
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public List<RoleVM> Roles { get; set; } = new List<RoleVM>();
        public List<UserVM> Users { get; set; } = new List<UserVM>();
        public string SelectedUserRole { get; set; }


    }
}
