namespace EmpsManagement.PL.ViewModels.Role
{
    public class RoleViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string RoleName { get; set; } = null!;
    }
}
