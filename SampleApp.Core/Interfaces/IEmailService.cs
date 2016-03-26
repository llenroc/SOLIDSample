namespace SampleApp.Core.Interfaces
{
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task<bool> SendMail(string from, string to, string message, string subject);
    }
}
