using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TkdAttendance.Data;
using TkdAttendance.Models;
using TkdAttendance.Services;

namespace TkdAttendance.Jobs;

public class RenewalReminderJob
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _users;
    private readonly IEmailSender _email;

    public RenewalReminderJob(AppDbContext db, UserManager<ApplicationUser> users, IEmailSender email)
    {
        _db = db; _users = users; _email = email;
    }

    public async Task<int> RunAsync()
    {
        var today = DateTime.Today; // local date — matches how ExpiryDate is entered
        var cutoff = today.AddDays(3);

        var expiring = await _db.Memberships
            .IgnoreQueryFilters()
            .Include(m => m.Student).ThenInclude(s => s.Branch)
            .Where(m => m.ExpiryDate.Date <= cutoff
                     && m.ExpiryDate.Date >= today
                     && m.RenewalNotifiedAt == null)
            .ToListAsync();

        foreach (var group in expiring.GroupBy(m => m.Student.BranchId))
        {
            var admins = await _users.Users.Where(u => u.BranchId == group.Key).ToListAsync();
            var body = string.Join("\n", group.Select(m => $"{m.Student.Name} — expires {m.ExpiryDate:d}"));

            foreach (var admin in admins)
                await _email.SendAsync(admin.Email!, "Students renewing soon", body);

            foreach (var m in group)
                m.RenewalNotifiedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();
        return expiring.Count;
    }
}