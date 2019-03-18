using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIService.Helpers
{
    public class EMailHelper
    {
        public static readonly string EMAIL_SENDER = "xyz.abc@outlook.com"; // change it to actual sender email id or get it from UI input
        public static readonly string EMAIL_CREDENTIALS = "*******";        // Provide credentials 
        public static readonly string SMTP_CLIENT = "smtp-mail.outlook.com"; // as we are using outlook so we have provided smtp-mail.outlook.com        
        public static readonly string EMAIL_BODY = "Reset your Password <a href='http://localhost.zzz.com/api/..../forgotPassword?'>Here.</a>";
        public static readonly string EMAIL_SUBJECT = "Password Reset Link";

        private string senderAddress;
        private string clientAddress;
        private string netPassword;
        public EMailHelper(string sender, string Password, string client)
        {
            senderAddress = sender;
            netPassword = Password;
            clientAddress = client;
        }

        public bool SendEMail(string recipient, string subject, string message)
        {

            bool isMessageSent = false;
            //Intialise Parameters
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(clientAddress);
            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(senderAddress, netPassword);
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                var mail = new System.Net.Mail.MailMessage(senderAddress.Trim(), recipient.Trim());
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                //System.Net.Mail.Attachment attachment;
                //attachment = new Attachment(@"C:\Users\XXX\XXX\XXX.jpg");
                //mail.Attachments.Add(attachment);

                client.Send(mail);
                isMessageSent = true;
            }
            catch (Exception ex)
            {
                isMessageSent = false;
            }

            return isMessageSent;
        }
    }
}