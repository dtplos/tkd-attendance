namespace TkdAttendance.Models;

public class AttendanceRecord
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public int ClassSessionId { get; set; }
    public ClassSession ClassSession { get; set; } = null!;
    public DateTime CheckedInAt { get; set; } = DateTime.UtcNow;
}