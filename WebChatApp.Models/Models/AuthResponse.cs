using System;
using System.Collections.Generic;
using System.Text;
using WebChatApp.Models.Entities;

namespace WebChatApp.Models.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public List<RoleEntity> Roles { get; set; }
    }
}
