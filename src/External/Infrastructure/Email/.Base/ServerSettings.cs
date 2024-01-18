namespace Infrastructure.Email.Base;

public class ServerSettings
{
    //Properties
    public string Provider { get; set; }
    public string Server { get; set; }
    public int Port { get; set; }
    public bool UseSsl { get; set; }

    //Construction
    public ServerSettings(string provider, string server, int port, bool useSsl)
    {
        Provider = provider;
        Server = server;
        Port = port;
        UseSsl = useSsl;
    }
}
