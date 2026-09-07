namespace TkdAttendance.Models;

public class ClassSession
{
    public int Id { get; set; }
    public int BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public int? ClassDefinitionId { get; set; }
    public ClassDefinition? ClassDefinition { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}