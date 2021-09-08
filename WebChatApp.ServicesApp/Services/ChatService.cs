using AutoMapper;
using System;
using WebChatApp.Core;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;

namespace WebChatApp.ServicesApp
{
    public class ChatService : IChatService
    {
        private IMapper _mapper;

        public ChatService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public int AddChat(ChatInputDto chatDto)
        {
            throw new NotImplementedException();
        }

        public int DeleteChat(int id)
        {
            throw new NotImplementedException();
        }

        public ChatEntity GetChatById(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateChat(ChatEntity chatDto)
        {
            throw new NotImplementedException();
        }
    }
}
