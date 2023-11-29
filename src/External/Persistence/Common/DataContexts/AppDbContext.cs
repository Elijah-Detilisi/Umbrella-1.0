namespace Persistence.Common.DataContexts;

public class AppDbContext : DbContext
{
    //Data sets
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<EmailEntity> Emails => Set<EmailEntity>();

    //Construction
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
        Database.Migrate();
    }

    //Lifecycle
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {

        options.UseSqlite();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}
