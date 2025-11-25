using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Silverhand.DAL.Data;
using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.DAL.Utilites
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task DataSeeding()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }

            // here you can seed Silverhand specific data later (plans, titles, etc.)

            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (!await _userManager.Users.AnyAsync())
            {
                var admin = new ApplicationUser
                {
                    Email = "admin@silverhand.com",
                    FullName = "Silverhand Admin",
                    PhoneNumber = "0000000000",
                    UserName = "admin",
                    EmailConfirmed = true
                };
                var user2 = new ApplicationUser()
                {
                    Email = "example@gmail.com",
                    FullName = "example user",
                    PhoneNumber = "0592100104",
                    UserName = "euser",
                    EmailConfirmed = true,
                };

                var user3 = new ApplicationUser()
                {
                    Email = "aboody9068@gmail.com",
                    FullName = "abood issa",
                    PhoneNumber = "0592100105",
                    UserName = "abood69",
                    EmailConfirmed = true,
                };


                await _userManager.CreateAsync(admin, "Pass@1212");
                await _userManager.CreateAsync(user2, "Pass@1212");
                await _userManager.CreateAsync(user3, "Pass@1212");

                await _userManager.AddToRoleAsync(admin, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                await _userManager.AddToRoleAsync(user3, "Customer");
                await _context.SaveChangesAsync();
            }
        }
    }
}
