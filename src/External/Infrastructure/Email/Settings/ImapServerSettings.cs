using Infrastructure.Email.Base;

namespace Infrastructure.Email.Settings;

public class ImapServerSettings : ServerSettings
{
    //Construction
    public ImapServerSettings(string provider, string server, int port, bool useSsl) : base(provider, server, port, useSsl)
    {
    }

    //Constants
    public static readonly ImapServerSettings Gmail = new("Gmail", "imap.gmail.com", 993, true);
    public static readonly ImapServerSettings Yahoo = new("Yahoo", "imap.mail.yahoo.com", 993, true);
    public static readonly ImapServerSettings Outlook = new("Outlook", "outlook.office365.com", 993, true);

    //Helper methods
    public static ImapServerSettings FindServerSettings(string emailDomain)
    {
        return emailDomain.ToLower() switch
        {
            "gmail.com" => ImapServerSettings.Gmail,
            "yahoo.com" => ImapServerSettings.Yahoo,
            "outlook.com" or "office365.com" => ImapServerSettings.Outlook,
            _ => throw new NotSupportedException($"Email provider for domain '{emailDomain}' is not supported."),
        };
    }
}
