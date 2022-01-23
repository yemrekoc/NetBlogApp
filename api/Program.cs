using Services.Abstract;
using Services.BlogServices;
using DataAccess.Repository.Abstract;
using DataAccess.Repository.Concreate;
using DataAccess.MSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBlogDataAccess, BlogDataAccess>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.Configure<MongoSettings>(options =>
{
    options.ConnectionString = builder.Configuration.GetSection("MongoConnection:ConnectionString").Value;
    options.Database = builder.Configuration.GetSection("MongoConnection:Database").Value;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
