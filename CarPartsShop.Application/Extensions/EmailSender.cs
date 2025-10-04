using System.Net;
using System.Net.Mail;

namespace CarPartsShop.Application.Extensions;

public static class EmailSender 
{
    public static void SendEmail(this string toEmail, string subject, string body)
    {
        var fromEmail = "sorooshh7@gmail.com";
        var password = "isyywyjthhodtmdy";

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);

        smtpClient.Send(mailMessage);
    }
}