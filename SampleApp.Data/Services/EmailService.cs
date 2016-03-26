namespace SampleApp.Data.Services
{
    using Core.Interfaces;
    using Microsoft.AspNet.Identity;
    using SendGrid;
    using System.Configuration;
    using System.Diagnostics;
    using System.Net;
    using System.Threading.Tasks;

    public class EmailService : IIdentityMessageService, IEmailService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigSendGridAsync(message);
        }

        public async Task<bool> SendMail(string from, string to, string message, string subject)
        {
            var msg = new SendGridMessage();
            msg.AddTo(to);
            msg.From = new System.Net.Mail.MailAddress(
                                from, subject);
            msg.Subject = subject;
            msg.Text = message;
            msg.Html = "<p>" + message + "<p/>";

            var credentials = new NetworkCredential(
                       ConfigurationManager.AppSettings["SendGrid:UserName"],
                       ConfigurationManager.AppSettings["SendGrid:Password"]
                       );

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(msg);
                return true;
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
                return false;
            }
        }

        private async Task ConfigSendGridAsync(IdentityMessage message)
        {
            var msg = new SendGridMessage();
            msg.AddTo(message.Destination);
            msg.From = new System.Net.Mail.MailAddress(
                                "no_reply_admin@sampleapp.com", "Sample App Website Admin");
            msg.Subject = message.Subject;
            msg.Text = message.Body;
            msg.Html = message.Body;

            var credentials = new NetworkCredential(
                       ConfigurationManager.AppSettings["SendGrid:UserName"],
                       ConfigurationManager.AppSettings["SendGrid:Password"]
                       );

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(msg);
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }
    }
}