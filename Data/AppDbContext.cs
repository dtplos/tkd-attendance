using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TkdAttendance.Models;

namespace TkdAttendance.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly ICurrentUserService _currentUser;

    public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUser)
        : base(options) => _currentUser = currentUser;

    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<ClassDefinition> ClassDefinitions => Set<ClassDefinition>();
    public DbSet<ClassSession> ClassSessions => Set<ClassSession>();
    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<Student>().HasQueryFilter(s =>
            _currentUser.IsSuperAdmin || s.BranchId == _currentUser.BranchId);

        b.Entity<ClassDefinition>().HasQueryFilter(c =>
            _currentUser.IsSuperAdmin || c.BranchId == _currentUser.BranchId);

        b.Entity<ClassSession>().HasQueryFilter(c =>
            _currentUser.IsSuperAdmin || c.BranchId == _currentUser.BranchId);

        b.Entity<Membership>().HasQueryFilter(m =>
            _currentUser.IsSuperAdmin || m.Student.BranchId == _currentUser.BranchId);

        b.Entity<AttendanceRecord>().HasQueryFilter(a =>
            _currentUser.IsSuperAdmin || a.Student.BranchId == _currentUser.BranchId);
    }
}