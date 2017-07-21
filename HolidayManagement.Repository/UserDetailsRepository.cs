using HolidayManagement.Repository.Models;
using System.Collections.Generic;
using System.Linq;

namespace HolidayManagement.Repository
{
    public class UserDetailsRepository : BaseRepository<UserDetails>, IUserDetailsRepository
    {
        public UserDetails GetUserDetailsById(int userDetailsId)
        {
            return DbContext.UsersDetails.FirstOrDefault(x => x.ID == userDetailsId);
        }

        public List<UserDetails> GetUsers()
        {
            return DbContext.UsersDetails.ToList();
        }

        public bool EditUserDetail(UserDetails model)
        {
            bool success = false;

            var user = DbContext.UsersDetails.FirstOrDefault(x => x.ID == model.ID);

            if (user != null)
            {         
                var existingUser = DbContext.UsersDetails.FirstOrDefault(x => x.AspnetUser.Email == model.AspnetUser.Email);

                if (existingUser == null || existingUser.ID == model.ID)
                    user.AspnetUser.Email = model.AspnetUser.Email;
                else               
                    return false;              

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MaxDays = model.MaxDays;
                user.HireDate = model.HireDate;

                DbContext.SaveChanges();

                success = true;
            }

            return success;
        }
    }
}
