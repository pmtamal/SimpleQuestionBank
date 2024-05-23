using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuestionBank.DataManagement;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var config=LoadConfiguration();


builder.Services.AddDbContext<QuestionBankContext>(options =>
{
    options.UseNpgsql(config.GetConnectionString("QuestionBank"));


});

var host=builder.Build();





await host.RunAsync();


static IConfiguration LoadConfiguration()
{
    return new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();
}