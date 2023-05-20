using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Lazy.Server.Infra;

public class EmailSender
{
    private readonly ILogger<EmailSender> logger;
    public string SmtpServer { get; }
    public int SmtpPort { get; }
    private bool SmtpSecure { get; }
    private string SmtpUser { get; }
    private string SmtpPass { get; }
    public string FromName { get; }
    public string FromEmail { get; }

    public EmailSender(
        ILogger<EmailSender> logger, string smtpServer, int smtpPort, bool smtpSecure, 
        string smtpUser, string smtpPass, string fromName, string fromEmail)
    {
        this.logger = logger;
        SmtpServer = smtpServer;
        SmtpPort = smtpPort;
        SmtpSecure = smtpSecure;
        SmtpUser = smtpUser;
        SmtpPass = smtpPass;
        FromName = fromName;
        FromEmail = fromEmail;
    }

    public SendResult SendMessage(Recipient recipient, string subject, string body)
    {
        try
        {
            var wrongEmail = string.Empty;
            MailAddress msgFrom = null;
            MailAddress recipientEmail = null;

            try
            {
                msgFrom = new MailAddress(FromEmail, FromName);
            }
            catch (Exception)
            {
                wrongEmail = FromEmail;
            }

            try
            {
                recipientEmail = new MailAddress(recipient.Email, recipient.Name ?? recipient.Email);
            }
            catch (Exception)
            {
                wrongEmail = recipient.Email;
            }


            if (!string.IsNullOrEmpty(wrongEmail))
                return new SendResult
                {
                    Email = recipient.Email,
                    Sent = false,
                    ErrorCode = (int)HttpStatusCode.BadRequest,
                    Error = $"Email is invalid or missing {wrongEmail}"
                };

            var msg = new MailMessage { From = msgFrom, ReplyToList = { "eugen-stefan.balan-ext@socgen.com" } };
            //msg.From = new SecureMailAddress(FromEmail, FromName, encryptionCert, signingCert);  // with signing
            msg.To.Add(recipientEmail);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;

            msg.Headers.Add("X-scrml-privacylevel", "C1");

            using (var client = BuildSmtpClient())
            {
                client.Send(msg);
            }


            logger.LogInformation($"Message subject [{subject}] sent ok to [{recipient.Email}] ");

            return new SendResult { Email = recipient.Email, Sent = true, ErrorCode = (int)HttpStatusCode.OK };
        }
        catch (Exception e)
        {
            logger.LogError($"Could not send secure message [{subject}] to: [{recipient.Email}] error: [{e.Message}]");
            return new SendResult { Email = recipient.Email, Sent = false, ErrorCode = (int)HttpStatusCode.InternalServerError, Error = e.Message };
        }
    }

    private SmtpClient BuildSmtpClient()
    {
        var client = SmtpPort > 0
            ? new SmtpClient(SmtpServer, SmtpPort)
            : new SmtpClient(SmtpServer);

        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = SmtpSecure;
        client.UseDefaultCredentials = !string.IsNullOrEmpty(SmtpUser);

        if (!string.IsNullOrEmpty(SmtpUser))
            client.Credentials = new NetworkCredential(SmtpUser, SmtpPass);

        return client;
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                var domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public class Recipient
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class SendResult
    {
        public string Email { get; set; }
        public bool Sent { get; set; }
        public int ErrorCode { get; set; }
        public string Error { get; set; }
    }
}