
# ToDo
- add fluent validation - use client and server side
    - form: detect changes
- NavigationManager.RegisterLocationChanging 
	- warn about unsaved changes
	- save unsaved changes to localStorage
- save page size/number when navigating away from lists
- add cache on clientside    
- add variables functionality to template editor





# snipets

## send email
    
private async Task Send()
    {
        MailMessage mail = new MailMessage();
        mail.To.Add(new MailAddress(userName));
        mail.From = new MailAddress(userName);
        mail.Subject = _emailTemplate.Title;
        mail.Body = _emailTemplate.Text;
        mail.IsBodyHtml = _emailTemplate.Html;

        SmtpClient smtp = new SmtpClient();
        smtp.Port = 587; // 25 465
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Host = "smtp.gmail.com";
        smtp.Credentials = new System.Net.NetworkCredential(userName, password);
        var task = smtp.SendMailAsync(mail);
        var awaiter = task.GetAwaiter();
        awaiter.OnCompleted(() => NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Detail = "Email was sent." }));
    }

    private string userName;
    private string password;