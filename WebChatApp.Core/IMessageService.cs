using System.Collections.Generic;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.Models.OutputModels;

namespace WebChatApp.Core
{
    public interface IMessageService
    {
        public int AddMessage(MessageInputDto messageDto);

        public int DeleteMessage(int id);

        public List<MessageOutputDto> GetMaterialsByGroupId(int id);

    }
}
