namespace WebApplication2.Enteties.Config;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Enteties;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasData(new User
        {
            Id = "user-1",
            FirstName = "Elizaveta",
            LastName = "Chigir",
            Email = "elizachigir@gmail.com",
            PasswordHash = "123456" 
        });
    }
}
