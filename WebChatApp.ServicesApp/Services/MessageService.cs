using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebChatApp.Core;
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
            var messageEntity = await _session.Query<MessageEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
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
    }
}
