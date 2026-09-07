using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TkdAttendance.Models;

namespace TkdAttendance.Data;

public static class SeedData
{
    public static async Task EnsureSeededAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<AppDbContext>();
        var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userMgr = services.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var role in new[] { "SuperAdmin", "BranchAdmin" })
            if (!await roleMgr.RoleExistsAsync(role))
                await roleMgr.CreateAsync(new IdentityRole(role));

        if (!db.Branches.Any())
        {
            db.Branches.AddRange(
                new Branch { Name = "Bukit Timah" },
                new Branch { Name = "West Coast" },
                new Branch { Name = "Bedok" },
                new Branch { Name = "Hillview" });
            await db.SaveChangesAsync();
        }

        // One-time rename in case your DB already has the old placeholder branch names.
        var renameMap = new Dictionary<string, string>
        {
            ["Branch 1"] = "Bukit Timah",
            ["Branch 2"] = "West Coast",
            ["Branch 3"] = "Bedok",
            ["Branch 4"] = "Hillview",
        };
        var anyRenamed = false;
        foreach (var branch in db.Branches.IgnoreQueryFilters())
        {
            if (renameMap.TryGetValue(branch.Name, out var realName))
            {
                branch.Name = realName;
                anyRenamed = true;
            }
        }
        if (anyRenamed) await db.SaveChangesAsync();

        if (await userMgr.FindByEmailAsync("admin@tkd.local") is null)
        {
            var admin = new ApplicationUser { UserName = "admin@tkd.local", Email = "admin@tkd.local", EmailConfirmed = true };
            await userMgr.CreateAsync(admin, "ChangeMe123!");
            await userMgr.AddToRoleAsync(admin, "SuperAdmin");
        }

        // Starter schedule for Hillview, taken from the studio's own tracking sheet.
        // Add the other branches' schedules via the "Classes" page once running.
        if (!db.ClassDefinitions.IgnoreQueryFilters().Any())
        {
            var hillview = db.Branches.IgnoreQueryFilters().FirstOrDefault(b => b.Name == "Hillview");
            if (hillview is not null)
            {
                var weekdays = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
                foreach (var day in weekdays)
                {
                    db.ClassDefinitions.AddRange(
                        new ClassDefinition { BranchId = hillview.Id, Name = "L.Tiny & Tiny", DayOfWeek = day, StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                        new ClassDefinition { BranchId = hillview.Id, Name = "Kids", DayOfWeek = day, StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(18, 0, 0) },
                        new ClassDefinition { BranchId = hillview.Id, Name = "Sparring", DayOfWeek = day, StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(19, 0, 0) },
                        new ClassDefinition { BranchId = hillview.Id, Name = "Kids", DayOfWeek = day, StartTime = new TimeSpan(19, 0, 0), EndTime = new TimeSpan(20, 0, 0) },
                        new ClassDefinition { BranchId = hillview.Id, Name = "Teens & Adults", DayOfWeek = day, StartTime = new TimeSpan(20, 0, 0), EndTime = new TimeSpan(21, 0, 0) });
                }
                await db.SaveChangesAsync();
            }
        }
    }
}