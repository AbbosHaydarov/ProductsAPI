using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Services;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


var builder = WebApplication.CreateBuilder(args);

//DB ulanishini sozlash
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Servislarni ro'yhatdan o'tkazish
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();

//Controllerlarni qo'shish
builder.Services.AddControllers();

//swagger qo'shish
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
    app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
