using HolidayManagement.Repository.Models;
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


        public string Message { get; set; }
    }
}