﻿using Infrastructure.Email.Base;

namespace Infrastructure.Email.Settings;

public class Pop3ServerSettings : ServerSettings
{
    //Construction
    public Pop3ServerSettings(string provider, string server, int port, bool useSsl) : base(provider, server, port, useSsl)
    {
    }

    //Constants
    public static readonly Pop3ServerSettings Gmail = new("Gmail", "pop.gmail.com", 995, true);
    public static readonly Pop3ServerSettings Yahoo = new("Yahoo", "pop.mail.yahoo.com", 995, true);
    public static readonly Pop3ServerSettings Outlook = new("Outlook", "outlook.office365.com", 995, true);

    //Helper methods
    public static Pop3ServerSettings FindPop3ServerSettings(string emailDomain)
    {
        return emailDomain.ToLower() switch
        {
            "gmail.com" => Pop3ServerSettings.Gmail,
            "yahoo.com" => Pop3ServerSettings.Yahoo,
            "outlook.com" or "office365.com" => Pop3ServerSettings.Outlook,
            _ => throw new NotSupportedException($"Email provider for domain '{emailDomain}' is not supported."),
        };
    }
}
