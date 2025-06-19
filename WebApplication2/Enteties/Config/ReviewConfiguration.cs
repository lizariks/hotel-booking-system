namespace WebApplication2.Enteties.Config;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Enteties;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Rating).HasPrecision(2, 1);
        builder.HasCheckConstraint("CK_Review_Rating", "\"Rating\" >= 0 AND \"Rating\" <= 5");

        builder.HasOne(r => r.Room)
            .WithMany(r => r.Reviews)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
