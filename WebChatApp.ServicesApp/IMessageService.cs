using System;
using System.Collections.Generic;
using System.Text;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.Models.OutputModels;

namespace WebChatApp.ServicesApp
{
    public interface IMessageService
    {
        public int AddMessage(MessageInputDto messageDto);

        public int DeleteMessage(int id);

        public List<MessageOutputDto> GetMaterialsByGroupId(int id);

    }
}
