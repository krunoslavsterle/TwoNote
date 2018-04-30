using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TwoNote.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "ksterle", Email = "ksterle@mail.com" };
            await userManager.CreateAsync(defaultUser, "Password@1");
        }
    }
}
