namespace SampleApp.Data.Services
{
    using Microsoft.AspNet.Identity;
    using System.Configuration;
    using System.Threading.Tasks;
    using Twilio;

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string accountSid = ConfigurationManager.AppSettings["Twilio:AccountSID"];
            string authToken = ConfigurationManager.AppSettings["Twilio:AuthToken"];
            string phoneNumber = ConfigurationManager.AppSettings["Twilio:PhoneNumber"];

            var twilioRestClient = new TwilioRestClient(accountSid, authToken);
            twilioRestClient.SendMessage(phoneNumber, message.Destination, message.Body);

            return Task.FromResult(0);
        }
    }
}