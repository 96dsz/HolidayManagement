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
        private UserDetailsRepository userDetailsRepo = new UserDetailsRepository();
        private TeamRepository teamRepo = new TeamRepository();

        // GET: Dashboard
        public ActionResult Index(bool newUser = false)
        {
            DashboardViewModel vM = new DashboardViewModel()
            {
                Message = newUser ? "hello new user" : "You are logged in"
            };

            vM.UserList = userDetailsRepo.GetUsers();
            vM.TeamList = teamRepo.GetTeams();

            return View(vM);
        }
        public ActionResult Users()
        {
            return View("Users");
        }

    }
}