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
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class MessageController : ControllerBase
    {
        private IMessageService _service;
        private IMapper _mapper;


        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _service = messageService;
            _mapper = mapper;
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
        public ActionResult<int> AddMessage([FromBody] MessageInputDto messageInputDto)
        {
            var newEntityId = _service.AddMessage(messageInputDto);
            //var newMessageDto = _service.GetMessageById(newEntityId);
            //var result = _mapper.Map<MessageOutputDto>(newMessageDto);
            return Ok(newEntityId);
        }

        /// <summary>
        /// Delete Message(by Id)
        /// </summary>
        /// <param name="id">Id of the message to be deleted</param>
        /// <returns>Return deleted MessageOutputModel</returns>
        // https://localhost:/api/chat/../message/
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Администратор, Модератор")]
        public ActionResult DeleteMessage(int id)
        {
            //if (_service.GetMessageById(id) is null)
            //    return NotFound($"Message {id} not found");
            //_service.DeleteMessageById(id);
            return NoContent();
        }

        // https://localhost:/api/message/by-chat/2
        /// <summary>Get all message related to chat</summary>
        /// <param name="id">Id of chat, which chat is needed</param>
        /// <returns>List of message to chat</returns>
        [ProducesResponseType(typeof(List<MessageOutputDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("by-chat/{id}")]
        //[Authorize]
        public ActionResult<List<MessageOutputDto>> GetMessagesByChatId(int id)
        {
            //if (_serviceGroup.GetChatById(id) is null)
            //    return NotFound($"Chat {id} not found");
            var listMessage = _service.GetMaterialsByGroupId(id);
            return Ok(listMessage);

        }
    }
}
