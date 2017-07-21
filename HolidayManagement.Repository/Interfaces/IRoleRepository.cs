using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayManagement.Repository.Interfaces
{
    public interface IRoleRepository
    {
        IdentityRole GetRoleById(string roleId);

        List<IdentityRole> GetRoles();
    }
}
