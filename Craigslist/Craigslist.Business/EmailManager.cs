using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Craigslist.Business
{
	public class EmailManager
	{
		public void SendEmail(string recepient, string from, string header, string content)
		{
			try
			{
				using (var client = new SmtpClient(ConfigurationManager.AppSettings["SmtpHost"], int.Parse(ConfigurationManager.AppSettings["SmtpPort"])))
				{
					client.UseDefaultCredentials = false;
					client.EnableSsl = true;
					client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["EmailPassword"]);

					using (var message = new MailMessage())
					{
						message.From = new MailAddress(from);
						message.To.Add(recepient);
						message.Subject = header;
						message.Body = content;

						client.Send(message);
					}
				}
			}
			catch 
			{
				// TODO we don't have logging on this project
			}
			
		}
	}
}
