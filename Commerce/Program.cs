using Commerce.Data;
using Commerce.Extensions;
using Commerce.Service;
using Commerce.Service.Iservice;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
    });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProduct,ProductService>();
builder.Services.AddScoped<IOrder,OrderService>();
builder.Services.AddScoped<IUser,UserService>();
builder.Services.AddScoped<IJwt,JwtService>();

builder.AddSwaggenGenExtension();

builder.AddAuth();
builder.AddAdminPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
