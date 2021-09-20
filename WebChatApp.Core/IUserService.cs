using System.Threading.Tasks;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.Models.OutputModels;

namespace WebChatApp.Core
{
    public interface IUserService
    {
        public Task<UserEntity> GetUserById(int id);

        public Task UpdateUserLogin( UserEntity userDto, string newLogin);

        public Task AddUser(UserInputDto userDto);

        public int DeleteUser(int id);

        //public UserEntity UpdateUserLogin(string login, int userId);

    }
}
