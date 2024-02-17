namespace Infrastructure.Common.Service;

public class AppFileService : IAppFileService
{
    public string GetDatabasePath(string filename)
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename);
    }
}
