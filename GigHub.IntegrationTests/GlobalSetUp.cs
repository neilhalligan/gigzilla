using GigHub.Core.Models;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GigHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();
            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new GigHub.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();
            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser {Name = "user1", UserName = "user1", Email = "-", PasswordHash = "-"});
            context.Users.Add(new ApplicationUser { Name = "user2", UserName = "user2", Email = "-", PasswordHash = "-" });
            context.SaveChanges();
        }
    }
}
