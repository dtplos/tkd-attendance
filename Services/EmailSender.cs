namespace TkdAttendance.Services;
public interface IEmailSender
{
    Task SendAsync(string to, string subject, string body);
}

public class ConsoleEmailSender : IEmailSender
{
    public Task SendAsync(string to, string subject, string body)
    {
        Console.WriteLine($"[EMAIL] to {to}: {subject}\n{body}");
        return Task.CompletedTask;
    }
}