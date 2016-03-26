namespace SampleApp.Data.Services
{
    using Microsoft.AspNet.Identity;
    using System.Threading.Tasks;
    using Twilio;

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            const string accountSid = "AC988153885438a5ac2b9ba3f6732a8350";
            const string authToken = "d087a79c362832361bc697b1b95bba9a";
            const string phoneNumber = "781-462-1289";

            var twilioRestClient = new TwilioRestClient(accountSid, authToken);
            twilioRestClient.SendMessage(phoneNumber, message.Destination, message.Body);

            return Task.FromResult(0);
        }
    }
}