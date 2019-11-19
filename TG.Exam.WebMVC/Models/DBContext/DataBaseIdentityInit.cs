using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TG.Exam.WebMVC.Models.DBContext
{
   public class DataBaseIdentityInit : MigrateDatabaseToLatestVersion<ApplicationDbContext, MigrationConfig>
   {
   }

   public class MigrationConfig : System.Data.Entity.Migrations.DbMigrationsConfiguration<ApplicationDbContext>
   {
      public MigrationConfig()
      {
         this.AutomaticMigrationsEnabled = true;
         AutomaticMigrationDataLossAllowed = true;
      }
   }
}