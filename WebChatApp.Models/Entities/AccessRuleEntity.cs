using System;
using System.Collections.Generic;
using System.Text;
using WebChatApp.Models.RelationShip;

namespace WebChatApp.Models.Entities
{
    public class AccessRuleEntity : BaseEntity
    {
        public string Name { get; set; }
        public List<RoleAccessRule> Roles { get; set; }
    }
}
