/*
 * Created by SharpDevelop.
 * User: daniel.thaller
 * Date: 01.11.2021
 * Time: 15:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Net.Mail;

namespace Bibliotheken
{
	/// <summary>
	/// Sammlung von Funktionen im Zusammenhang mit dem Mailversand
	/// </summary>
	public static class Mail
	{
		/// <summary>
		/// versendet eine Mail
		/// </summary>
		/// <param name="to">Empfänger</param>
		/// <param name="from">Absender</param>
		/// <param name="subject">Betreff der Mail</param>
		/// <param name="msgBody">Mailbody als HTML</param>
		/// <param name="mailhost">Mailserver</param>
		/// <param name="user">Benutzer zur Authentifizierung am Mailserver</param>
		/// <param name="pass">Passwort zur Authentifizierung am Mailserver</param>
		/// <returns>Sendeergebnis als Bool</returns>
		public static bool SendMail(String to, String from, String subject, String msgBody, String mailhost, String user, String pass)
		{
			try
	        {
				//to="daniel.thaller@sdv.de";
	            MailMessage msg = new MailMessage("" + from, "" + to, subject, msgBody);
	            msg.IsBodyHtml = true;
	            msg.Priority=MailPriority.High;
	            //msg.CC.Add("daniel.thaller@sdv.de");
	            SmtpClient smtp = new SmtpClient("" + mailhost);
				NetworkCredential basicCredential = new NetworkCredential(user, pass); 
				smtp.Host = mailhost;
				smtp.UseDefaultCredentials = false;
				smtp.Credentials = basicCredential;
	            smtp.Send(msg);
	            msg.Attachments.Dispose();
	            return true;
	        }
            catch
	        {
	            return false;
	        }
		}
	}
}
