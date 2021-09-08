using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebChatApp.Core;
using WebChatApp.Core.Session;
using WebChatApp.Models.Entities;

namespace WebChatApp.ServicesApp
{
    public class UserService : IUserService
    {
        private readonly ISession _session;
        public UserService(ISession session)
        {
            _session = session;
        }

        public int AddUser(UserEntity userDto)
        {
            throw new NotImplementedException();
        }

        public int DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            var user = await _session.Query<UserEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public int UpdateUser(int id, UserEntity userDto)
        {
            throw new NotImplementedException();
        }

        public UserEntity UpdateUserLogin(string login, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
