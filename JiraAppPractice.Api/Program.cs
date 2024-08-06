using FluentValidation;
using FluentValidation.AspNetCore;
using JiraAppPractice.Api.Middlewares;
using JiraAppPractice.Api.Validators;
using JiraAppPractice.Data.Context;
using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Interfaces;
using JiraAppPractice.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBoardValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateItemValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<JiraContext>();

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
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapIdentityApi<User>();
app.MapControllers();

app.Run();
