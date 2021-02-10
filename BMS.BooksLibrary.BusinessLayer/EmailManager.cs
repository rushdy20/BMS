using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BMS.BusinessLayer
{
    public class EmailManager : IEmailManager
    {
        private const string DefaultFrom = "info@britanniaislamiccentre.org";
        private const string SMPServer = "britanniaislamiccentre.org";
        private const int Port = 465;
        private const string userName = "info@britanniaislamiccentre.org";
        private const string key = "cbk7362m1t%@";

        public async Task<bool> SendEmail(string to, string message, string subject)
        {
            var smtpClient = new SmtpClient(SMPServer)
            {
              Credentials = new NetworkCredential(userName, key)
            };
            
            var mailMessage = new MailMessage(DefaultFrom,to,subject,message);
            try
            {
               
                smtpClient.Send(mailMessage);
               
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
           
            return true;
        }

        public async Task<bool> SendEmail(string to, string from, string message, string subject)
        {
            var smtpClient = new SmtpClient(SMPServer)
            {
                Credentials = new NetworkCredential(userName, key)
            };
            var mailMessage = new MailMessage(from, to, subject, message);

            smtpClient.Send(mailMessage);
            return true;
        }
    }
}