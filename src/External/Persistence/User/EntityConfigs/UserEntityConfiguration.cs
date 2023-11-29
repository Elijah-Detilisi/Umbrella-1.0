using Domain.User.ValueObjects;

namespace Persistence.User.EntityConfigs;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(nameof(UserEntity));
        builder.Property(e => e.UserName).IsRequired();
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        ConfigureValueObjects(builder);
    }

    private void ConfigureValueObjects(EntityTypeBuilder<UserEntity> builder)
    {
        builder.OwnsOne(
            e => e.EmailAddress, emailBuilder =>
            {
                emailBuilder.Property(email => email.Value)
                    .HasColumnName(nameof(EmailAddress));
            }
        );

        builder.OwnsOne(
            e => e.EmailPassword, passwordBuilder =>
            {
                passwordBuilder.Property(password => password.Value)
                    .HasColumnName(nameof(EmailPassword));
            }
        );
    }
}
