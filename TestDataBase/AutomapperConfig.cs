using AutoMapper;
using System;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.Models.OutputModels;

namespace TestDataBase
{
    public class AutomapperConfig : Profile
    {
        private const string _dateFormat = "dd.MM.yyyy";
        private const string _dateFormatWithTime = "dd.MM.yyyy H:mm:ss";
        public AutomapperConfig()
        {
            CreateMap<UserEntity, UserOutputDto>();
            CreateMap<ChatEntity, ChatOutputDto>();
            CreateMap<MessageEntity, MessageOutputDto>();

            CreateMap<UserInputDto, UserEntity>();
            CreateMap<ChatInputDto, ChatEntity>();
            CreateMap<MessageInputDto, MessageEntity>();
            CreateMap<MessageOutputDto,MessageEntity>();
        }

    }
}
