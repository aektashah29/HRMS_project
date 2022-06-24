using System.Threading.Tasks;

namespace HRMS_project.Repository
{
    public interface IEmailSender
    {
        Task SendEmailAsync(
               string email, string subject, string message);
    }
}
