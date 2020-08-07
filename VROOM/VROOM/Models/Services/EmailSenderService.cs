using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Http;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace VROOM.Models.Services
{
    public class EmailSenderService : IEmailSender
    {
        private IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SendGridClient client = new SendGridClient(_configuration["SENDGRID_API_KEY"]);

            SendGridMessage message = new SendGridMessage();

            message.SetFrom("admin@vroom.com", "The Vroom Team");
            message.AddTo(email);
            message.SetSubject(subject);
            message.AddContent(MimeType.Html, htmlMessage);
            await client.SendEmailAsync(message);
        }
    }
}
