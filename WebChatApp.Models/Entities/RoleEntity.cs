using System;
using System.Collections.Generic;
using System.Text;
using WebChatApp.Models.RelationShip;

namespace WebChatApp.Models.Entities
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public List<RoleAccessRule> AccessRules { get; set; }
    }
}
