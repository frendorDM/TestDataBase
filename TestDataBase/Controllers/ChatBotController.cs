using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChatApp.Core;
using WebChatApp.Core.Enum;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.Models.OutputModels;
using WebChatApp.Models.RelationShip;
using WebChatApp.ServicesApp;

namespace TestDataBase.Controllers
{
    // https://localhost:44365/api/chat/
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ChatBotController : ControllerBase
    {
        private IChatBotService _service;
        private IMapper _mapper;
        private RestClient _client;

        public ChatBotController(IChatBotService youtubeService, IMapper mapper)
        {
            _service = youtubeService;
            _mapper = mapper;
            _client = new RestClient();
        }

        /// <summary>
        /// Creates Chat
        /// </summary>
        /// <param name="chat"> is used to get all the information about new chat that is necessary to create it</param>
        /// <returns>Returns the ChatOutputModel</returns>
        // https://localhost:/api/chat/
        [ProducesResponseType(typeof(ChatOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("public")]
        //[Authorize] 
        public async Task<IActionResult> Search1(string videoName)
        {
            var anything = await _service.GetVideoId(videoName);
            //return Ok(result);
            //var queryResult = (await GetResponseAsync<string>("https://www.googleapis.com/youtube/v3/commentThreads?key=AIzaSyBsh8XU8bAL1FtP5GECDDnscMUTmY8o41A&textFormat=plainText&part=snippet&videoId=E9AJvGkfAiU&maxResults=5", Method.GET));
            //return Ok(queryResult);
            return Ok(anything);
        }

        private async Task<IRestResponse> GetResponseAsync<T>(string url, Method method)
        {
            var request = new RestRequest(url, method);

            return await _client.ExecuteAsync(request);
        }

        /// <summary>
        /// Create new message
        /// </summary>
        /// <param name="messageInputModel">Model object of message to be created</param>
        /// <returns>Return OutputModel of created attachment</returns>
        // https://localhost:/api/chat/message
        [ProducesResponseType(typeof(MessageOutputDto), StatusCodes.Status200OK)]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddMessage([FromBody] MessageInputDto messageInputDto)
         {
            await _service.AddMessageBot(messageInputDto);
            //var newMessageDto = _service.GetMessageById(newEntityId);
            //var result = _mapper.Map<MessageOutputDto>(newMessageDto);
            return Ok();
        }
    }
}
