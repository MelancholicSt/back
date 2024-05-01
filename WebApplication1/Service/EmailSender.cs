using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;

namespace WebApplication1;
//25 port
//TuejTc4cYfytEsaw0MyV pwd
public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress("Marketplace", "draindev@internet.ru"));
        msg.To.Add(new MailboxAddress("", email));
        msg.Subject = subject;
        msg.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = htmlMessage
        };
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.mail.ru", 465 , true);
            await client.AuthenticateAsync("draindev@internet.ru", "TuejTc4cYfytEsaw0MyV");
            await client.SendAsync(msg);
 
            await client.DisconnectAsync(true);
        }
    }
}