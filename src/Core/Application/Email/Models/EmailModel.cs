namespace Application.Email.Models;

public class EmailModel : Model
{
    public EmailType Type { get; set; }
    public EmailStatus EmailStatus { get; set; }
    public EmailBodyText Body { get; set; }
    public EmailSubjectLine Subject { get; set; }
    public EmailAddress Sender { get; set; }
    public List<EmailAddress> Recipients { get; set; }
}
