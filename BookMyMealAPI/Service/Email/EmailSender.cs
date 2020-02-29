using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyMealAPI.Services.Email
{
    public class EmailSender
    {
        public Boolean sendVerificationEmail(string TO, String Link)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Book My Meal", "bookmymeal.verify@gmail.com"));
                message.To.Add(new MailboxAddress(TO, TO));
                message.Subject = "Verify Email";
                message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                { Text = string.Format("<a href='{0 }' style='font - size:22px; padding: 10px; color: #ffffff'>Confirm Email Now </ a > ", Link) };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {

                    client.Connect("smtp.gmail.com", 587, false);

                    //SMTP server authentication if needed
                    client.Authenticate("bookmymeal.verify@gmail.com", "01910778878");

                    client.Send(message);

                    client.Disconnect(true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
    }
}
