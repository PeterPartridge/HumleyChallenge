using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class SendGridEmailer
    {

        public async Task SendEmail(string key, string errorMessage)
        {
            var client = new SendGridClient(key);
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress("info@noReply.com"));

            var recipients = new List<EmailAddress>
                {
                    new EmailAddress("petertoby@aol.com")
                };
            msg.AddTos(recipients);

            msg.SetSubject("Humley Challenge Error");

            msg.AddContent(MimeType.Text, errorMessage);
           
            var response = await client.SendEmailAsync(msg);

        }
    }
}
