using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;

namespace WebChatApp.Core
{
    public interface IChatService
    {
        public ChatEntity GetChatById(int id);

        public int AddChat(ChatInputDto chatDto);

        public int UpdateChat(ChatEntity chatDto);

        public int DeleteChat(int id);

    }
}
