using Exchange.Portal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Portal.ApplicationCore.Services;

internal class MigrationService : IMigrationService
{
    private readonly IInitiateRateExchange _initiateRateExchange;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<MigrationService> _logger;

    public MigrationService(IInitiateRateExchange initiateRateExchange,
        ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager, ILogger<MigrationService> logger)
    {
        _initiateRateExchange = initiateRateExchange;
        _applicationDbContext = applicationDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task MigrateAsync()
    {
        _logger.LogInformation("Migration has started");
        await _initiateRateExchange.InstantiateAsync();

        if ((await _applicationDbContext.Database.GetAppliedMigrationsAsync()).Any())
        {
            _logger.LogInformation("Ef migration's starting");
            
            await _applicationDbContext.Database.MigrateAsync();
            
            _logger.LogInformation("Ef migration's finished");
        }

        if (await _userManager.FindByNameAsync("Admin") == null)
        {
            _logger.LogInformation("Adding admin credentials");
            
            var adminRole = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin".ToUpperInvariant()
            };

            await _roleManager.CreateAsync(adminRole);

            await _userManager.CreateAsync(new IdentityUser
            {
                UserName = "Admin",
                Email = "admin@gmail.com"
            }, "Silvercrown1!");

            IdentityUser? user = await _userManager.FindByNameAsync("Admin");
            
            if (user is null)
            {
                throw new AggregateException();
            }
            
            await _userManager.AddToRoleAsync(user, adminRole.Name);
            
            _logger.LogInformation("Admin credentials have been added");
        }
    }
}