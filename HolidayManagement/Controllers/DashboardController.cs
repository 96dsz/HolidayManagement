using HolidayManagement.Models;
using HolidayManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HolidayManagement.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private UserDetailsRepository UserDetailsRepo = new UserDetailsRepository();
        // GET: Dashboard
        public ActionResult Index(bool newUser = false)
        {
            DashboardViewModel vM = new DashboardViewModel()
            {
                Message = newUser ? "hello new" : "hello old"
            };

            vM.UserList = UserDetailsRepo.GetUsers();

            return View(vM);
        }
        public ActionResult Users()
        {
            return View("Users");
        }

    }
}