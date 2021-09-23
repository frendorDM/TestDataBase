using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Models.Models.InputModels;

namespace WebChatApp.Core
{
    public interface IChatBotService
    {
        public Task<string> GetVideoId(string videoName);
        public Task AddMessageBot(MessageInputDto messageDto);
    }
}
