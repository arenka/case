using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Management.UI.Data
{
    public class ApplicationDbContextSeed
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationDbContextSeed(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async void SeedAdminUser()
        {
            var user = new IdentityUser()
            {
                UserName = "Email@email.com",
                NormalizedUserName = "email@email.com",
                Email = "Email@email.com",
                NormalizedEmail = "email@email.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            

            if (!_context.Roles.Any(r => r.Name == "admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole {Name = "admin", NormalizedName = "admin"});
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "admin");
            }

            await _context.SaveChangesAsync();
        }
    }
}
