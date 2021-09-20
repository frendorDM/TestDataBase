using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebChatApp.Core;
using WebChatApp.Core.Session;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.RelationShip;

namespace WebChatApp.ServicesApp
{
    public class ChatService : IChatService
    {
        private IMapper _mapper;
        private ISession _session;

        public ChatService(IMapper mapper, ISession session)
        {
            _mapper = mapper;
            _session = session;
        }

        public async Task AddChat(ChatInputDto inputModel)
        {
            var chatEntity = _mapper.Map<ChatEntity>(inputModel);
            await _session.AddEntityAsync(chatEntity);
        }

        public int DeleteChat(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ChatEntity> GetChatById(int id)
        {
            var chatEntity = await _session.Query<ChatEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return chatEntity;
        }

        public async Task UpdateChatName(ChatEntity chatDto, string chatName)
        {
            chatDto.ChatName = chatName;
            await _session.UpdateEntity(chatDto);
        }

        public async Task AddUserToChat(int chatId, int userId) 
        {
            UserChat userChatEntity = new UserChat();
            userChatEntity.ChatId = chatId;
            userChatEntity.UserId = userId;
            await _session.AddEntityAsync(userChatEntity);
        }
    }
}
