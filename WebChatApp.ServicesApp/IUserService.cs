using System;
using System.Collections.Generic;
using System.Text;
using WebChatApp.Models.Entities;

namespace WebChatApp.ServicesApp
{
    public interface IUserService
    {
        public UserEntity GetUserById(int id);

        public int UpdateUser(int id, UserEntity userDto);

        public int AddUser(UserEntity userDto);

        public int DeleteUser(int id);

        public UserEntity UpdateUserLogin(string login, int userId);

    }
}
