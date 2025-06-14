// JobPro.Service/Email/EmailService.cs

using MailKit.Net.Smtp;
using MimeKit;

public class EmailService : IEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("no-reply@jobpro.com"));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart("plain") { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.example.com", 587, false);
        await smtp.AuthenticateAsync("username", "password");
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}