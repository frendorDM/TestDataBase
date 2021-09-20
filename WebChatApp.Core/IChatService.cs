using System.Threading.Tasks;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;

namespace WebChatApp.Core
{
    public interface IChatService
    {
        public Task<ChatEntity> GetChatById(int id);

        public Task AddChat(ChatInputDto chatInputDto);

        public Task UpdateChatName(ChatEntity chatDto, string chatName);

        public int DeleteChat(int id);

        public Task AddUserToChat(int chatId, int userId);

    }
}
