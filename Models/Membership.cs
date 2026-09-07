namespace TkdAttendance.Models;
public enum MembershipType { ClassPack, Monthly }

public class Membership
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public MembershipType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int? ClassesRemaining { get; set; }   // ClassPack only
    public decimal Price { get; set; }
    public DateTime? RenewalNotifiedAt { get; set; } // prevents re-notifying every day
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}