using Microsoft.AspNetCore.Components.Authorization;

namespace TkdAttendance.Data;

public interface ICurrentUserService
{
    int? BranchId { get; }
    bool IsSuperAdmin { get; }
}

public class CurrentUserService : ICurrentUserService
{
    private readonly AuthenticationStateProvider _authProvider;
    private bool _loaded;
    private int? _branchId;
    private bool _isSuperAdmin;

    public CurrentUserService(AuthenticationStateProvider authProvider)
    {
        _authProvider = authProvider;
    }

    public int? BranchId { get { EnsureLoaded(); return _branchId; } }
    public bool IsSuperAdmin { get { EnsureLoaded(); return _isSuperAdmin; } }

    private void EnsureLoaded()
    {
        if (_loaded) return;
        _loaded = true;
        try
        {
            var user = _authProvider.GetAuthenticationStateAsync().GetAwaiter().GetResult().User;
            _isSuperAdmin = user.IsInRole("SuperAdmin");
            var claim = user.FindFirst("BranchId")?.Value;
            _branchId = int.TryParse(claim, out var id) ? id : null;
        }
        catch (InvalidOperationException)
        {
            // No active circuit (e.g. startup/migrations) — treat as no user.
            _isSuperAdmin = false;
            _branchId = null;
        }
    }
}