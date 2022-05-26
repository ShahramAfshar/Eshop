﻿using EStore.Domain.Entities.Commons;
using System.Collections.Generic;

namespace EStore.Domain.Entities.Users
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public  virtual ICollection<UserInRole> UserInRoles { get; set; }
    }

}
