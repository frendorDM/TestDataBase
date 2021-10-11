using System;
using System.Collections.Generic;
using System.Text;
using WebChatApp.Models.Entities;
using WebChatApp.Models.RelationShip;

namespace WebChatApp.Models.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public List<UserRole> Roles { get; set; }
    }
}
