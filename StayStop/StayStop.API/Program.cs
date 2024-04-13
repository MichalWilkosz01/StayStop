using FluentValidation;
using StayStop.BLL.Dtos.User;
using StayStop.BLL.Pagination;
using StayStop.BLL.Validators;
using StayStop.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<HotelPagination>, HotelPaginationValidator>();
builder.Services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();

builder.Services.AddDbContext<StayStopDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();