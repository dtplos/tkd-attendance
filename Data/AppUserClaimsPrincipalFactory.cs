using Microsoft.AspNetCore.Identity;
using TkdAttendance.Models;

namespace TkdAttendance.Data;

public class AppUserClaimsPrincipalFactory
    : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    public AppUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        Microsoft.Extensions.Options.IOptions<IdentityOptions> options)
        : base(userManager, roleManager, options) { }

    protected override async Task<System.Security.Claims.ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        if (user.BranchId is not null)
            identity.AddClaim(new System.Security.Claims.Claim("BranchId", user.BranchId.Value.ToString()));
        return identity;
    }
}