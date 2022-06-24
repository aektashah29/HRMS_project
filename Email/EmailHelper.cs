using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HRMS_project.Models
{
    public class EmailHelper
    {
        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("aektashah29@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Password Reset";
          
            mailMessage.Body = "Link"; 

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network; 
            client.Credentials = new System.Net.NetworkCredential("aektashah29@gmail.com", "Aektashah@2912");
            client.UseDefaultCredentials = true;
            client.EnableSsl = true;
            client.Host = "smtpout.secureserver.net";
            client.Port = 465;
           
            try
            {
                client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception)
            {
                
            }
            return false;
        }
    }
}
    
