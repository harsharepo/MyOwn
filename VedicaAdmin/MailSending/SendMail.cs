using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace MailSending
{
    public class SendMail
    {
        public SendMail()
        {

        }
        public string sendMail(string name, string email, string message)
        {
            string result = string.Empty; ;
            try
            {
                MailAddress fromAddress = new MailAddress(email, name);
                MailAddress toAddress = new MailAddress("harsha669966@gmail.com");
                MailMessage msg = new MailMessage(fromAddress, toAddress);
                msg.Body = message;
                msg.Subject = "Request for Appointment";
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;
                SmtpClient smtp = new SmtpClient(Properties.Settings.Default.MailSMTP);
                smtp.Credentials = new NetworkCredential(Properties.Settings.Default.MailUserName, Properties.Settings.Default.MailPassword);
                smtp.Port = Properties.Settings.Default.MailPort;
                //smtp.EnableSsl = true;
                smtp.Send(msg);
                result = "Success";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
