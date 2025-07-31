using Castle.Core.Configuration;
using EmpsManagement.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace EmpsManagement.BLL.Services.EmailService
{
    public static class EmailSender
    {            
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587); // Replace with your SMTP server
            client.EnableSsl = true; // Enable SSL for secure connection
            client.Credentials = new NetworkCredential("ahmedsamir.as9728@gmail.com", "gwquleyxxtkwhlgx");
            client.Send("ahmedsamir.as9728@gmail.com", email.ToWhom, email.Subject, email.Body);




        }
    }
}
