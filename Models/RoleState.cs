using Microsoft.AspNetCore.Identity;

namespace ASP.NetCoreMVC_SchoolSystem.Models
{
    public class RoleState
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUsers> Members { get; set; }
        public IEnumerable<AppUsers> NonMembers { get; set; }
    }
}
