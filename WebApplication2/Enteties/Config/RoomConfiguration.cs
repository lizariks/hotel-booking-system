using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace WebApplication2.Enteties.Config;


public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.RoomNumber).IsRequired();
        builder.Property(r => r.PricePerNight).HasPrecision(10, 2);
        builder.Property(r => r.RoomType).IsRequired();

        builder.HasData(
            new Room
            {
                Id = "room-1",
                RoomNumber = "101",
                RoomType = RoomType.Standard,
                PricePerNight = 100,
                IsAvailable = true
            },
            new Room
            {
                Id = "room-2",
                RoomNumber = "102",
                RoomType = RoomType.Deluxe,
                PricePerNight = 200,
                IsAvailable = false
            }
        );
    }

}