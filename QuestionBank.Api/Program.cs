using QuestionBank.Api;
using FluentValidation.AspNetCore;
using FluentValidation;
using NLog.Web;
using QuestionBank.Common;
using QuestionBank.Validator;
using QuestionBank.Api.CustomMiddleware;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using QuestionBank.DataManagement;

var builder = WebApplication.CreateBuilder(args);





builder.Services.AddControllers();

builder.Services.AddInternationalization();



builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
                new CultureInfo("en"),
                new CultureInfo("fr"),
                new CultureInfo("es")
            };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


builder.Services.AddDatabaseContext(builder.Configuration.GetConnectionString("QuestionBank"));

builder.Services.AddMapper();

builder.Logging.ClearProviders();

builder.Host.UseNLog();


builder.Services.AddValidatorsFromAssemblyContaining<UserAccountValidator>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddRepository();



builder.Services.AddBusinessLayer();
builder.Services.SetUpCors();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions.Value);

await using var scope = app.Services.CreateAsyncScope();
await using var db = scope.ServiceProvider.GetService<DbContext>();
await db.GetService<IMigrator>().MigrateAsync();


app.UseHttpsRedirection();

app.UseCors();
app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();