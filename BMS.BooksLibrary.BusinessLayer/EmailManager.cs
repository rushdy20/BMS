using System;
using System.Threading.Tasks;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using BMS.AWS;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;

namespace BMS.BusinessLayer
{
    public class EmailManager : IEmailManager
    {
        private readonly IAmazonSecretsManager _secretsManager;
        private const string DefaultFrom = "Noreply@bic-library.org";
        private const string SMPServer = "email-smtp.us-east-2.amazonaws.com";
       
        private const int Port = 587; // 465;
     
        private const string MagazineEditor = "editormagazine.bic@gmail.com"; 
        private const string NoReplyLibrary = "Noreply@bic-library.org"; 

        private const string SesSecretKey = "prod/bms/ses";

        private string UserName { get; set; }
        private string Key { get; set; }
        private readonly IErrorLog _errorLog;

        public EmailManager(IErrorLog errorLog, IAmazonSecretsManager secretsManager)
        {
            _errorLog = errorLog;
            _secretsManager = secretsManager;
            SetSecrets(SesSecretKey);
        }

        public async Task<bool> SendEmail(string to, string message, string subject)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(DefaultFrom));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) {Text = message};

                using var smtp = BuildSmptClient();
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return true;
        }

        public async Task<bool> SendEmail(string to, string from, string message, string subject)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("Noreply@bic-library.org"));
                email.To.Add(MailboxAddress.Parse("najathRushdy@gmail.com"));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) {Text = message};

                using var smtp = BuildSmptClient();
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)

            {
                var m = ex.Message;
            }

            return true;
        }

        public async Task<bool> MagazineFeedBackEmail(string message)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(DefaultFrom));
                email.To.Add(MailboxAddress.Parse("editormagazine.bic@gmail.com"));
                email.Subject = "Magazine Feedback";
                email.Body = new TextPart(TextFormat.Html) {Text = message};

                using var smtp = BuildSmptClient();
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                await _errorLog.Error($"Sending Email: {ex.Message}");
                Console.Write(ex.Message);
            }

            return true;
        }

        private async Task SetSecrets(string SesSecretKey)
        {
            var request = new GetSecretValueRequest();
            request.SecretId = SesSecretKey;
            request.VersionStage = "AWSCURRENT";

            try
            {
                var response = _secretsManager.GetSecretValueAsync(request).Result;
                var secretsData = JsonConvert.DeserializeObject<AWSSecretsModel>(response.SecretString);
                UserName = secretsData.Username;
                Key = secretsData.Key;
            }
            catch (Exception ex)
            {
            }
        }

        private SmtpClient BuildSmptClient()
        {
            using var smtp = new SmtpClient();
            smtp.Connect(SMPServer, Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(UserName, Key);

            return smtp;
        }
    }
}