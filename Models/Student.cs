namespace TkdAttendance.Models;

public enum BeltRank { White, Yellow, Green, Blue, Red, Black }

public class Student
{
    public int Id { get; set; }
    public int BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public string Name { get; set; } = "";
    public string? Contact { get; set; }
    public int? Age { get; set; }
    public string? IdNumber { get; set; }
    public BeltRank Belt { get; set; } = BeltRank.White;
    public int? ClassDefinitionId { get; set; }
    public ClassDefinition? ClassDefinition { get; set; }
    public DateTime JoinDate { get; set; } = DateTime.Today;
    public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}