using WebApplication2.Enteties;

using WebApplication2.Repository;
using WebApplication2.UnitOfWork;

using Microsoft.EntityFrameworkCore;
using WebApplication2.Services.Interfaces;
using WebApplication2.Services.Implement;

var builder = WebApplication.CreateBuilder(args);

//contection to db
builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//services and dto
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();