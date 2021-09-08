using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChatApp.Core;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.Models.OutputModels;
using WebChatApp.ServicesApp;

namespace TestDataBase.Controllers
{
    // https://localhost:44365/api/chat/
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ChatController : ControllerBase
    {
        private IChatService _service;
        private IMapper _mapper;

        public ChatController(IChatService chatService, IMapper mapper)
        {
            _service = chatService;
            _mapper = mapper;
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
        [HttpPost]
        //[Authorize]
        public ActionResult<int> AddNewChat([FromBody] ChatInputDto chat)
        {
            var addedChatId = _service.AddChat(chat);
            //var result = _mapper.Map<ChatOutputDto>(_service.GetChatById(addedChatId)); //в сервисы убрать 
            //return Ok(result);
            return Ok(addedChatId);
        }
        // можно сделать 3 контроллера на создание комнат. 3 вида комнат, актоматически заполнять поле Тип Чата.
        // убрать id из ассациативнаых таблиц. Добавить таблицу блок позователей. С датой начала блока и временем блока. Chek is bloked user
        // можно выгнать пользователя по DateTime(MAX)
        /// <summary>
        /// Rename Chat
        /// </summary>
        /// <param name="chat"> is used to rename chat </param>
        /// <returns>Returns the ChatOutputModel</returns>
        /// https://localhost:/api/chat/
        [ProducesResponseType(typeof(ChatOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("id")]
        //[Authorize]
        public ActionResult<ChatOutputDto> UpdateChatName(int id, [FromBody] ChatInputDto chat)
        {

            if (_service.GetChatById(id) == null)
            {
                return NotFound("Error. Chat not Found");
            }
//var chatDto = _mapper.Map<Chat>(chat);
            //_service.UpdateChat(chatDto);
            //var result = _mapper.Map<ChatOutputDto>(_service.GetChatById(id));
            return Ok();
        }
    }
}
