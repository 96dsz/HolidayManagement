using HolidayManagement.Repository.Interfaces;
using HolidayManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayManagement.Repository
{
    public class BankHolidayRepository : BaseRepository<BankHoliday>, IBankHolidayRepository
    {
        public BankHoliday GetBankHolidayById(int bankHolidayId)
        {
            return DbContext.BankHolidays.FirstOrDefault(x => x.ID == bankHolidayId);
        }

        public List<BankHoliday> GetBankHolidays()
        {
            return DbContext.BankHolidays.ToList();
        }

        public static implicit operator List<object>(BankHolidayRepository v)
        {
            throw new NotImplementedException();
        }
    }
}
