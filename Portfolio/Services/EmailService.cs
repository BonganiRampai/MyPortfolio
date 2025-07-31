using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using Portfolio.Models.ViewModels;
using System.Net.Mail;
using System.Net;

namespace Portfolio.Services
{
    
    public interface IEmailService
    {
        void SendEmail(ContactFormViewModel model);
    }
    public class EmailService : IEmailService
    {
        public void SendEmail(ContactFormViewModel model)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(model.Name, model.Email));
            email.To.Add(MailboxAddress.Parse("bonganerampai@gmail.com")); // ✅ Replace with your Gmail
            email.Subject = model.Subject;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = $"From: {model.Name} ({model.Email})\n\n{model.Message}"
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("bonganerampai@gmail.com", ""); // ✅ Replace with your Gmail + app password
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
    

}
