using HolidayManagement.Repository.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolidayManagement.Models
{
    public class DashboardViewModel
    {
     
        public List<UserDetails> UserList { get; set; }

        public List<Team> TeamList { get; set; }

        public List<IdentityRole> RoleList { get; set; }
        
        public CalendarViewModel Calendar {get;set;}

        public string Message { get; set; }
    }
}