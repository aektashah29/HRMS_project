using System.Threading.Tasks;

namespace HRMS_project.Repository
{
    public class EmailSender : IEmailSender 
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
