using Exchange.Portal.ApplicationCore.Interface;
using Exchange.Portal.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Portal.ApplicationCore.Services;

public class MigrationService : IMigrationService
{
    private readonly IInitiateRateExchange _initiateRateExchange;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public MigrationService(IInitiateRateExchange initiateRateExchange,
        ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _initiateRateExchange = initiateRateExchange;
        _applicationDbContext = applicationDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task MigrateAsync()
    {
        await _initiateRateExchange.InstantiateAsync();

        if ((await _applicationDbContext.Database.GetAppliedMigrationsAsync()).Any())
        {
            await _applicationDbContext.Database.MigrateAsync();
        }

        if (await _userManager.FindByNameAsync("Admin") == null)
        {
            var adminRole = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin".ToUpperInvariant()
            };

            await _roleManager.CreateAsync(adminRole);

            IdentityResult res = await _userManager.CreateAsync(new IdentityUser
            {
                UserName = "Admin",
                Email = "admin@gmail.com"
            }, "Silvercrown1!");

            IdentityUser? user = await _userManager.FindByNameAsync("Admin");
            await _userManager.AddToRoleAsync(user, adminRole.Name);
        }
    }
}