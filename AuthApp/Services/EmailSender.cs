using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Diagnostics;
using Task = System.Threading.Tasks.Task;

namespace AuthApp.Services
{
    public class EmailSender : IEmailSender
    {
        public AuthMessageSenderOptions Options { get; }

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.BrevoKey))
            {
                throw new Exception("Brevo key is null");
            }
            await Execute(Options.BrevoKey, subject, message, email);
        }

        public async Task Execute(string brevoKey, string subject, string message, string email)
        {

            Configuration.Default.ApiKey.Add("api-key", brevoKey);
            // create transaction API
            var apiInstance = new TransactionalEmailsApi();

            // sender details
            string SenderName = "Rahul Awasthi";
            string SenderEmail = "awasthir314@gmail.com";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(email, $"Name: {email}");
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);

            // html details 
            string HtmlContent = "<html><body><h1>This is my first transactional email. </h1></body></html>";
            string TextContent = $"Hi Hello! text Content Lelo\n {message}";
            string Subject = subject;

            // reply to details
            string ReplyToName = "Rahul Awasthi";
            string ReplyToEmail = "awasthir314@gmail.com";
            SendSmtpEmailReplyTo ReplyTo = new SendSmtpEmailReplyTo(ReplyToEmail, ReplyToName);

            // attachment details
            string AttachmentUrl = null;
            string stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
            byte[] Content = System.Convert.FromBase64String(stringInBase64);
            string AttachmentName = "test.txt";
            SendSmtpEmailAttachment AttachmentContent = new SendSmtpEmailAttachment(AttachmentUrl, Content, AttachmentName);
            List<SendSmtpEmailAttachment> Attachment = new List<SendSmtpEmailAttachment>();
            Attachment.Add(AttachmentContent);

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, Subject, ReplyTo, Attachment);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Debug.WriteLine(result.ToJson());
                Console.WriteLine(result.ToJson());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
            }
        }
    }
}
