using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TG.Exam.WebMVC.Models;

namespace Salestech.Exam.WebMVC.Controllers
{
   public class UsersController : Controller
   {
      private List<User> users;

      public UsersController()
      {
         users = TG.Exam.WebMVC.Models.User.GetAll();
      }

      // GET: Users
      [Authorize(Roles = "admin")]
      public ActionResult Index()
      {
         var newList = users.ConvertAll(u => new User
            {FirstName = u.FirstName, LastName = u.LastName, Age = u.Age, SyncAsync = "Sync"});
         return View(newList);
      }

      [Authorize(Roles = "admin")]
      public ActionResult UpdateAsync()
      {
         var newList = users.ConvertAll(u => new User
            { FirstName = u.FirstName, LastName = u.LastName, Age = u.Age + 10, SyncAsync = "Async" });
         return PartialView("_UsersList", newList);
      }
   }
}