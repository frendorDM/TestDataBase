using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models;

namespace WebChatApp.Core
{
    public interface IAuthenticationService
    {
        public AuthResponse GenerateToken(UserEntity user);
        public Task<UserEntity> GetAuthentificatedUser(string login);
    }
}
