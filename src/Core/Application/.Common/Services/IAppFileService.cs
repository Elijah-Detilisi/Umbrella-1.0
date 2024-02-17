namespace Application.Common.Services;

public interface IAppFileService
{
    public string GetDatabasePath(string filename = "Umbrella.db");
}
