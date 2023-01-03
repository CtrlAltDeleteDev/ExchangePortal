using Carter;
using Exchange.Portal.ApplicationCore;
using Exchange.Portal.ApplicationCore.Interface;
using Exchange.Portal.Infrastructure;
using Exchange.Portal.Presentation;
using Exchange.Portal.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.

ApiConfigurationSettings settings = builder.Services.AddSettings(builder.Configuration);

builder.Services.AddApplication();
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
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapCarter();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
