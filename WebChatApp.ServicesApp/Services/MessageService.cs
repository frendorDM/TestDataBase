using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebChatApp.Core;
using WebChatApp.Core.Enum;
using WebChatApp.Core.Session;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.Models.OutputModels;

namespace WebChatApp.ServicesApp
{
    public class MessageService : IMessageService
    {
        private IMapper _mapper;
        private ISession _session;
        public MessageService(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }
        public Task AddMessage(MessageInputDto messageDto)
        {
            var messageModel = _mapper.Map<MessageEntity>(messageDto);
            messageModel.CreateTime = System.DateTime.Now;
            var messageId = _session.AddEntityAsync(messageModel);
            return messageId;
        }

        public async Task DeleteMessage(MessageOutputDto message)
        {
            var messageEntity = _mapper.Map<MessageEntity>(message);
            await _session.RemoveEntityPhysical(messageEntity);
        }

        public async Task<MessageOutputDto> GetMessageById(int id) 
        {
            var messageEntity = await _session.Query<MessageEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            var result = _mapper.Map<MessageOutputDto>(messageEntity);
            return result;
        }
        public async Task<List<MessageOutputDto>> GetAllMessageByChatId(int chatid)
        {
            var messageEntities = await _session.Query<ChatEntity>().AsNoTracking().Include(x => x.Messages).FirstOrDefaultAsync(x => x.Id == chatid);
            List<MessageOutputDto> messageOutput = new List<MessageOutputDto>();
            foreach (var message in messageEntities.Messages) 
            {
                var messageOutputDto = _mapper.Map<MessageOutputDto>(message);
                messageOutput.Add(messageOutputDto);
            }
            return messageOutput;
        }
        public async Task<List<MessageEntity>> GetAllMessageByUserId(int userId, int chatType, char filter, DateTime dateTime)
        {
            var total = await _session.Query<MessageEntity>().AsNoTracking().Where(x => x.UserCreatorId == userId).CountAsync();
            var pageSize = 2;
            var page = 1;
            var skip = pageSize * (page - 1);
            var canPage = skip < total;


            //DateTime time = new DateTime(2021, 9, 29);
            if (filter == '>')
            {
                var chatsEntity = await _session.Query<MessageEntity>().AsNoTracking()
                   .Where(x => x.UserCreatorId == userId && x.CreateTime > dateTime && x.Chat.Type == chatType)
                   .OrderBy(x => x.CreateTime)
                   .Skip(skip)
                   .Take(pageSize)
                   .ToListAsync();
                return chatsEntity;
            }
            else if (canPage)
                return null;
            return null;
        }

        //public async Task<List<MessageEntity>> GetAllMessageByUserId(int userId)
        //{
        //    List<MessageEntity> message = new List<MessageEntity>();
        //    var argument = Expression.Parameter(typeof(MessageEntity));
        //    var valueProperty = Expression.Property(argument, "UserCreatorId");
        //    var containsCall = Expression.Call(valueProperty,
        //      typeof(MessageEntity).GetMethod(
        //        "Contains", new Type[] { typeof(MessageEntity) }),
        //      Expression.Constant(userId, typeof(int)));
        //    var wherePredicate = Expression.Lambda<Func<MessageEntity, bool>>(
        //      containsCall, argument);
        //    var whereCall = Expression.Call(typeof(Queryable), "Where",
        //      new Type[] { typeof(MessageEntity) },
        //      message.AsQueryable().Expression, wherePredicate);
        //    var expressionResults = message.AsQueryable()
        //      .Provider.CreateQuery<MessageEntity>(whereCall);
        //    return null;

        //}
    }
}
