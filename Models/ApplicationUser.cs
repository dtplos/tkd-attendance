using Microsoft.AspNetCore.Identity;

namespace TkdAttendance.Models;

public class ApplicationUser : IdentityUser
{
    public int? BranchId { get; set; }
    public Branch? Branch { get; set; }
}