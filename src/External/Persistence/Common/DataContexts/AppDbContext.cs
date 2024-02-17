using Application.Common.Services;

namespace Persistence.Common.DataContexts;

public class AppDbContext : DbContext
{
    //Fiekds
    private readonly IAppFileService _appFileService;

    //Data sets
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<EmailEntity> Emails => Set<EmailEntity>();

    //Construction
    public AppDbContext
    (
        IAppFileService appFileService, 
        DbContextOptions<AppDbContext> options
    ) : base(options)
    {
        _appFileService = appFileService;

        Database.EnsureCreated();
        Database.Migrate();
    }

    //Lifecycle
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        options.UseSqlite($"Filename={_appFileService.GetDatabasePath("Umbrella.db")}");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}
