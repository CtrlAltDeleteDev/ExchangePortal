using Carter;
using Exchange.Portal.ApplicationCore;
using Exchange.Portal.ApplicationCore.Interface;
using Exchange.Portal.Infrastructure;
using Exchange.Portal.Presentation;
using Exchange.Portal.Web.Configurations;
using NLog;
using NLog.Web;

Logger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddControllers();
    // Add services to the container.
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    ApiConfigurationSettings settings = builder.Services.AddSettings(builder.Configuration);

    builder.Services.AddApplication(settings.BinanceClient);
    builder.Services.AddInfrastructure(settings.ConnectionStrings.DefaultConnection);
    builder.Services.AddPresentation();

    var app = builder.Build();

    //TODO: Come up with a better approach
    //Ef core migration along with Marten. Seed data.
    using var scope = app.Services.CreateScope();
    IMigrationService migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService>();
    migrationService.MigrateAsync().GetAwaiter().GetResult();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
    }
    else
    {
        app.UseHsts();
    }

    app.MapCarter();

    app.UseAuthentication();
    app.UseAuthorization();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Unexpectedly terminated!");
    throw;
}
finally
{
    LogManager.Shutdown();
}
