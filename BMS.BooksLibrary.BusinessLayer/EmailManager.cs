using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BMS.BusinessLayer
{
    public class EmailManager : IEmailManager
    {
        private const string DefaultFrom = "info@britanniaislamiccentre.org";
        private const string SMPServer = "email-smtp.us-east-2.amazonaws.com";  // "britanniaislamiccentre.org";
        private const int Port = 587;// 465;
        private const string userName = "AKIAVGJPQ6JCMJJ44GDC"; //"info@britanniaislamiccentre.org";
        private const string key = "BIsw7nalwF5+5Qg1jlsS4jtX1SOXUzDNOBTl7OvG+kao";
                                                     //"cbk7362m1t%@";                                // 
        private const string MagazineEditor = "editormagazine.bic@gmail.com";  // "editor-magazine@britanniaislamiccentre.org";

        private readonly IErrorLog _errorLog;

        public EmailManager(IErrorLog errorLog)
        {
            _errorLog = errorLog;
        }

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
            message = $"{message} ::Email From {from}";
            var smtpClient = new SmtpClient(SMPServer)
            {
                Credentials = new NetworkCredential(userName, key)
            };
            var mailMessage = new MailMessage(from, to, subject, message);

            smtpClient.Send(mailMessage);
            return true;
        }

        public async Task<bool> MagazineFeedBackEmail(string message)
        {
            try

            {
                var smtpClient = new SmtpClient(SMPServer)
                {
                    Credentials = new NetworkCredential(userName, key),
                    EnableSsl = true
                    
                };
                var mailMessage = new MailMessage(MagazineEditor, "editormagazine.bic@gmail.com", "Feedback for The Soul Journal", message);
                
                smtpClient.Send(mailMessage);
                return true;


            }
            catch (Exception ex)
            {
                await _errorLog.Error($"Sending Email: {ex.Message}");
                Console.Write(ex.Message);

            }

            return true;

        }
    }
}