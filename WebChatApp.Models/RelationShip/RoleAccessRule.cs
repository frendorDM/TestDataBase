using System;
using System.Collections.Generic;
using System.Text;
using WebChatApp.Models.Entities;

namespace WebChatApp.Models.RelationShip
{
    public class RoleAccessRule : BaseEntity
    {
        public int RoleId { get; set; }
        public int AccessRuleId { get; set; }
        public RoleEntity Role { get; set; }
        public AccessRuleEntity AccessRule { get; set; }

    }
}
