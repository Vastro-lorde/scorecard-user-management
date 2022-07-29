using scorecard_user_mgt.Models;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Interfaces
{
    public interface IMailService
    {
        string GetEmailTemplate(string templateName);
        Task SendEmailAsync(MailRequest mailRequest);
    }
}