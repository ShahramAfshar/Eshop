using EStore.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Users
{
    public class User:BaseEntity
    {
        public string  FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }

    }

}
