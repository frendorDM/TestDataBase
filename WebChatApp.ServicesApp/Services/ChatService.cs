using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WebChatApp.Core;
using WebChatApp.Core.Session;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.RelationShip;
using WebChatApp.Models.Models.OutputModels;

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

        public async Task<int> AddChat(ChatInputDto inputModel,int chatType)
        {
            var chatEntity = _mapper.Map<ChatEntity>(inputModel);
            chatEntity.Type = chatType;

            await _session.AddEntityAsync(chatEntity);
            return chatEntity.Id;
        }

        public int DeleteChat(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ChatEntity> GetChatById(int id)
        {
            var chatEntity = await _session.Query<ChatEntity>().AsNoTracking().Include(x=> x.Users).FirstOrDefaultAsync(x => x.Id == id);
            return chatEntity;


        }
        public async Task<List<DashboardModel>> GetDashboardByUserId(int id)
        {
            var chatsEntity = await _session.Query<UserChat>().AsNoTracking().Where(x => x.UserId == id)
                .Select( x => new DashboardModel(x.User.FirstName, x.Chat.ChatName, x.Chat.Messages.OrderBy(y => y.CreateTime).FirstOrDefault()))
                .ToListAsync();
            return chatsEntity;
        }
        // Example of working with anonymous models --->

        //public async Task<List<DashboardModel>> GetDashboardByUserId(int id)
        //{
        //    var chatsEntity = await _session.Query<UserChat>().AsNoTracking().Where(x => x.UserId == id)
        //        .Select(x => new
        //        {
        //            x.User.FirstName,
        //            x.Chat.ChatName,
        //            Messages = x.Chat.Messages.OrderBy(y => y.CreateTime).FirstOrDefault()
        //        })
        //        .ToListAsync();
        //    return null;

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

