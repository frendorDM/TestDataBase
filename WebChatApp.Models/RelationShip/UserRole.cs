using System;
using System.Collections.Generic;
using System.Text;
using WebChatApp.Models.Entities;

namespace WebChatApp.Models.RelationShip
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public UserEntity User { get; set; }
        public RoleEntity Role { get; set; }

    }
}
