using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using UserService.Domain.Interfaces.Auth;

namespace UserService.Infrastructure.Services;

public class EmailService(IConfiguration configuration) 
    : IEmailService
{
    public async Task SendEmailConfirmationAsync(string email, string confirmationLink, 
        CancellationToken ct)
    {
        var smtpServer = configuration["EmailSettings:SmtpServer"]!;
        var port = int.Parse(configuration["EmailSettings:Port"]!);
        var senderName = configuration["EmailSettings:SenderName"]!;
        var senderEmail = configuration["EmailSettings:SenderEmail"];
        var username = configuration["EmailSettings:Username"];
        var password = configuration["EmailSettings:Password"];
        var enableSsl = bool.Parse(configuration["EmailSettings:EnableSsl"]!);

        using var smtpClient = new SmtpClient(smtpServer, port);
        
        smtpClient.Credentials = new NetworkCredential(username, password);
        smtpClient.EnableSsl = enableSsl;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        
        const string subject = "Welcome to User Service!";
        var body = $"""
                    <html>
                    <body>
                        <h1>Welcome, {username}!</h1>
                        <p>Thank you for registering with our service.</p>
                        <p>Please confirm your email by clicking the link below:</p>
                        <p><a href="{confirmationLink}">Confirm Email</a></p>
                        <br/>
                        <p>Best regards,<br/>User Service Team</p>
                    </body>
                    </html>
                    """;
        
        var msg = new MailMessage
        {
            From = new MailAddress(senderEmail!, senderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        
        msg.To.Add(email);

        await smtpClient.SendMailAsync(msg, ct);
    }
    
    public Task SendPasswordResetAsync(Guid userId, string email, string resetToken, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task SendDeactivationNoticeAsync(Guid userId, string email, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task SendActivationNoticeAsync(Guid userId, string email, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}