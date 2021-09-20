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
        private IUserService _userService;

        public ChatController(IChatService chatService, IMapper mapper, IUserService userService)
        {
            _service = chatService;
            _mapper = mapper;
            _userService = userService;
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
        public async Task<ActionResult> AddNewChat([FromBody] ChatInputDto chat)
        {
            await _service.AddChat(chat);
            //var result = _mapper.Map<ChatOutputDto>(_service.GetChatById(addedChatId)); //в сервисы убрать 
            //return Ok(result);
            return Ok();
        }
        // можно сделать 3 контроллера на создание комнат. 3 вида комнат, актоматически заполнять поле Тип Чата.
        // убрать id из ассациативнаых таблиц. Добавить таблицу блок позователей. С датой начала блока и временем блока. Chek is bloked user
        // можно выгнать пользователя по DateTime(MAX)

        // https://localhost:44365/api/chat/1
        /// <summary>Get info of chat</summary>
        /// <param name="cahtId">Id of chat</param>
        /// <returns>Info of chat</returns>
        [ProducesResponseType(typeof(ChatOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("{chatId}")]
        //[Authorize]
        public async Task<IActionResult> GetChatById(int chatId)
        {
            var chat = await _service.GetChatById(chatId);

            if (chat == null)
            {
                return NotFound($"User with id {chatId} is not found");
            }

            return Ok(chat);
        }
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
        [HttpPut("chatId")]
        //[Authorize]
        public async Task<ActionResult> UpdateChatName(int id, [FromBody] UpdateChatNameInputDto chatName)
        {
            var chat = await _service.GetChatById(id);
            if (chat == null)
            {
                return NotFound($"Error. Chat with id{id} not Found");
            }
            //var chatDto = _mapper.Map<Chat>(chat);
            await _service.UpdateChatName(chat, chatName.ChatName);
            //var result = _mapper.Map<ChatOutputDto>(_service.GetChatById(id));
            return Ok();
        }

        /// <summary>
        /// Creates the connection between one chat and one user
        /// </summary>
        /// <param name="chatId"> is used to find the group which one user wants to connect with user</param>
        /// <param name="userId"> is used to find the student which one user wants to connect with chat</param>
        /// <returns>Returns Created result</returns>
        // https://localhost:XXXXX/api/chat/id/user/id
        [HttpPost("{chatId}/user/{userId}")]
        //[Authorize(Roles = "Администратор")]
        public async Task<ActionResult> AddUserToChat(int chatId, int userId)
        {
            var chat = await _service.GetChatById(chatId);
            if (chat is null)
                return NotFound($"Chat with id{chatId} not found");

            var user = _userService.GetUserById(userId);
            if (user is null)
                return NotFound($"Пользователь c id {userId} не найден");

            await _service.AddUserToChat(chatId, userId);
            return Ok();
        }
    }
}
