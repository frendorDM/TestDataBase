using System.Threading.Tasks;
using WebChatApp.Models.Entities;

namespace WebChatApp.Core
{
    public interface IUserService
    {
        public Task<UserEntity> GetUserById(int id);

        public int UpdateUser(int id, UserEntity userDto);

        public int AddUser(UserEntity userDto);

        public int DeleteUser(int id);

        public UserEntity UpdateUserLogin(string login, int userId);

    }
}
