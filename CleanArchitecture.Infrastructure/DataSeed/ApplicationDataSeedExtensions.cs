using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.DataSeed {
    public static class ApplicationDataSeedExtensions {
        
        public static void EnsureDatabaseIsCreatedAndSeeded(this IApplicationBuilder applicationBuilder) {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
                InitializeApplicationData(serviceScope);
                InitializeIdentityData(serviceScope);
            }
        }

        private static void InitializeApplicationData(IServiceScope serviceScope) {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDataContext>();
            context.Database.Migrate();

            if (context.Teams.Any()) {
                return;
            }
            var teams = new List<Team> { 
                new Team { Name = "Mercedes", FoundationYear = 1960, Wins = 10, EntryFeePaid = true },
                new Team { Name = "McLaren", FoundationYear = 1970, Wins = 3, EntryFeePaid = true },
                new Team { Name = "Ferrari", FoundationYear = 1985, Wins = 2, EntryFeePaid = true },
                new Team { Name = "Honda", FoundationYear = 2012, Wins = 0, EntryFeePaid = false },
                new Team { Name = "Red Bull", FoundationYear = 1994, Wins = 7, EntryFeePaid = true },
                new Team { Name = "Renault", FoundationYear = 2002, Wins = 5, EntryFeePaid = false }
            };
            foreach (var team in teams) {
                context.Teams.Add(team);
            }
            context.SaveChanges();
        }

        private async static void InitializeIdentityData(IServiceScope serviceScope) {
            var adminUser = "admin";
            var adminPassword = "f1test2018";

            var context = serviceScope.ServiceProvider.GetService<IdentityDataContext>();
            context.Database.Migrate();

            if (context.Set<IdentityUser>().Where(x => x.UserName == adminUser).Any()) {
                return;
            }
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var user = new IdentityUser { 
                UserName = adminUser
            };
            await userManager.CreateAsync(user, adminPassword);
        }
    }
}
