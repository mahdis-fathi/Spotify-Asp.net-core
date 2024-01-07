using System.Net.Mail;
using System.Net;

namespace Spotify.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false)
        {
            // SMTP server settings
            string smtpHost = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "dumbydumbyness@gmail.com";
            string smtpPassword = "cmbg wezs rdso qlit";

            // Recipient email address
            string recipientEmail = toEmail;

            // Create a Client Instance
            var client = new SmtpClient
            {
                Port = smtpPort,
                Host = smtpHost,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword)
            };

            try
            {
                // Send an Email
                client.SendMailAsync(smtpUsername, recipientEmail, subject, message);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while sending the email: " + ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
