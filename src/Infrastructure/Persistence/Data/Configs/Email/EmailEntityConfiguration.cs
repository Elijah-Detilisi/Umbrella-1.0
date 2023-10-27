using Domain.Email.ValueObjects;
using System.Text.Json;

namespace Persistence.Data.Configs.Email;

public class EmailEntityConfiguration : IEntityTypeConfiguration<EmailEntity>
{
    public void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
        builder.ToTable(nameof(EmailEntity));
        builder.Property(e => e.Type).IsRequired();
        builder.Property(e => e.EmailStatus).IsRequired();
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        ConfigureValueObjects(builder);
    }

    private void ConfigureValueObjects(EntityTypeBuilder<EmailEntity> builder)
    {
        builder.OwnsOne(
            e => e.Body, bodyBuilder =>
            {
                bodyBuilder.Property(body => body.Value)
                    .HasColumnName("Body").HasMaxLength(EmailBodyText.MAXBODYLENGTH);
            }
        );

        builder.OwnsOne(
            e => e.Subject, subjectBuilder =>
            {
                subjectBuilder.Property(subject => subject.Value)
                    .HasColumnName("Subject").HasMaxLength(EmailSubjectLine.MAXSUBJECTLINELENGTH);
            }
        );

        var jsonOption = new JsonSerializerOptions();
        builder.Property(e => e.Recipients)
            .HasColumnName("Recipients")
            .HasConversion(
                v => JsonSerializer.Serialize(v, jsonOption),
                v => JsonSerializer.Deserialize<List<EmailAddress>>(v, jsonOption)
        ); 


    }
}
