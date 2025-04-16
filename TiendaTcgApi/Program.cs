using Microsoft.EntityFrameworkCore;
using TiendaTcgApi.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//Area de Servicios

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));



var app = builder.Build();

//Area de MiddleWares

app.MapControllers();

app.Run();
