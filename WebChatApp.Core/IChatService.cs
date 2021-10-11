using System.Collections.Generic;
using System.Threading.Tasks;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models;
using WebChatApp.Models.Models.InputModels;

namespace WebChatApp.Core
{
    public interface IChatService
    {
        public Task<ChatEntity> GetChatById(int id);
        public Task<int> AddChat(ChatInputDto chatInputDto, int chatType); 
        public Task UpdateChatName(ChatEntity chatDto, string chatName);
        public Task<List<DashboardModel>> GetDashboardByUserId(int id);
        public int DeleteChat(int id);
        public Task AddUserToChat(int chatId, int userId);

    }
}
