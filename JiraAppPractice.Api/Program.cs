using JiraAppPractice.Data.Context;
using JiraAppPractice.Services.Interfaces;
using JiraAppPractice.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var ConnectionString = builder.Configuration["ConnectionStrings:JiraAppDB"];

builder.Services.AddDbContext<JiraContext>(options =>
{
    options.UseSqlServer(ConnectionString);
});

builder.Services.AddScoped<IJiraItemService, JiraItemService>();
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

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
