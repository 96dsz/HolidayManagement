using HolidayManagement.Models;
using HolidayManagement.Repository;
using HolidayManagement.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private BankHolidayRepository getBankHoliday = new BankHolidayRepository();
        private RoleRepository roleRepo = new RoleRepository();
        private VacationRepository vacRep = new VacationRepository();


        // GET: Dashboard
        public ActionResult Index(bool newUser = false)
        {
            DashboardViewModel vM = new DashboardViewModel()
            {
                Message = newUser ? "hello new user" : "You are logged in"
            };

            vM.UserList = userDetailsRepo.GetUsers();
            vM.TeamList = teamRepo.GetTeams();
            vM.RoleList = roleRepo.GetRoles();
            CalendarViewModel calendar = new CalendarViewModel();
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();

           calendar.MonthDays = GetMonthDays(DateTime.Now.Year, DateTime.Now.Month);
           calendar.Month = mfi.GetMonthName(DateTime.Now.Month);
            
            vM.Calendar = calendar;
            return View(vM);
        }

        [HttpGet]
        public ActionResult GetMonth(int year, int month)
        {
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            CalendarViewModel calendar = new CalendarViewModel()
            {
                MonthDays = GetMonthDays(year, month),
                Month= mfi.GetMonthName(month)
            };
            return Json(new {calendar= calendar }, JsonRequestBehavior.AllowGet);
        }

        private List<MonthDayViewModel> GetMonthDays(int year, int month)
        {
            List<MonthDayViewModel> days = new List<MonthDayViewModel>();

            DateTime currentDate = DateTime.Now;

            var bankHolidays = getBankHoliday.GetBankHolidays();

            int dayCount = DateTime.DaysInMonth(year, month);


            var holidays = vacRep.GetVacations(year, month);

            for (int i = 1; i <= dayCount; i++)
            {
                DateTime date = new DateTime(year, month, i);
                MonthDayViewModel day = new MonthDayViewModel();
                day.Day = i;
                day.Name = date.DayOfWeek.ToString();

                var bH = bankHolidays.FirstOrDefault(x => x.Day == i && x.Month == month);

                day.IsFreeDay = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || bH != null;

                day.Vacations = holidays.Where(x => DateTime.Compare(x.StartDate, date) <= 0 && DateTime.Compare(x.EndDate, date) >= 0).ToList();

                day.Description = DateTime.Now.ToString("MMMM");
                DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                day.Month = mfi.GetMonthName(month).ToString();


                days.Add(day);

            }

            return days;
        }

        public ActionResult Users()
        {
            return View("Users");
        }
        [HttpPost]
        public ActionResult AddHoliday(string UserId, DateTime fromDate, DateTime toDate)
        {
            return Json(new {}, JsonRequestBehavior.AllowGet);
        }


        //return Json(new {}, JsonRequestBehavior.DenyGet);

    }

    }

