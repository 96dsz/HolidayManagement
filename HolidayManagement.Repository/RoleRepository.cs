using HolidayManagement.Repository.Interfaces;
using HolidayManagement.Repository.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayManagement.Repository
{
    public class RoleRepository : BaseRepository<Team>, IRoleRepository
    {
        public IdentityRole  GetRoleById(string roleId)
        {
            return DbContext.Roles.FirstOrDefault(x => x.Id == roleId);
        }

        public List<IdentityRole> GetRoles()
        {
            return DbContext.Roles.ToList();
        }
    }
}

