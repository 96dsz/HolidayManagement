using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace HolidayManagement.Repository.Models
{
    public class UserDetails
    {
        public ICollection<IdentityUserRole> UserRole;

        [Key]
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? HireDate { get; set; }

        public int? MaxDays { get; set; }

        public int? TeamId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser AspnetUser { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
    }
}