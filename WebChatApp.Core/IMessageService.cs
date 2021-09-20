﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.Models.OutputModels;

namespace WebChatApp.Core
{
    public interface IMessageService
    {
        public Task AddMessage(MessageInputDto messageDto);

        public Task DeleteMessage(MessageOutputDto message);
        public Task<MessageOutputDto> GetMessageById(int id);

        public List<MessageOutputDto> GetMaterialsByGroupId(int id);

    }
}
