using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TG.Exam.WebMVC.Models.DBContext
{
   public class SampleData
   {
      public static async void Initialize(ApplicationDbContext context)
      {
         var roleStore = new RoleStore<IdentityRole>(context);
         var roleManager = new RoleManager<IdentityRole>(roleStore);
         if (!roleManager.RoleExists("admin"))
         {
            var role = new IdentityRole {Name = "admin"};
            roleManager.Create(role);
         }

         var userStore = new UserStore<ApplicationUser>(context);


         var resultAsync = userStore.FindByNameAsync("admin@mail.com");

         if (resultAsync.Result == null)
         {
            var user = new ApplicationUser
            {
               UserName = "admin@mail.com",
               PasswordHash = new PasswordHasher().HashPassword("Admin123_"),
               SecurityStamp = "",
            };
            await userStore.CreateAsync(user);
            await userStore.AddToRoleAsync(user, "admin");
         }

         await context.SaveChangesAsync();
      }
   }
}