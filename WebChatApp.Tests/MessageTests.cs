using NUnit.Framework;
using WebChatApp.Core;
using AutoMapper;
using Moq;
using WebChatApp.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebChatApp.Models.Models.OutputModels;
using WebChatApp.Core.Session;
using WebChatApp.ServicesApp;
using TestDataBase;
using TestDataBase.Controllers;
using WebChatApp.Models.Models.InputModels;

namespace WebChatApp.Tests
{
    public class Tests
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly ISession _session;
        private MessageController _messageController;
        private Mock<IMessageService> _messsageServiceMock;

        [SetUp]
        public void Setup()
        {
            _messsageServiceMock = new Mock<IMessageService>();
            

            var automapperConfig = new AutomapperConfig();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(automapperConfig));
            IMapper mapper = new Mapper(configuration);

            _messageController = new MessageController(_messsageServiceMock.Object, mapper);
        }

        [Test]
        public async Task Test1()
        {
            var sessionMock = new Mock<ISession>();
            sessionMock.Setup(sess => sess.Query<MessageEntity>(false)).Verifiable();
            var sut = new MessageService(sessionMock.Object, null);

            await sut.GetMessageById(1);

            sessionMock.Verify();
        }
        [Test]  
        public async Task Test2()
        {
            var sessionMock = new Mock<ISession>();
            sessionMock.Setup(sess => sess.Query<MessageEntity>(false)).Verifiable();
            var sut = new MessageService(sessionMock.Object, null);

            await sut.GetMessageById(1);

            sessionMock.Verify();
        }

        [Test]
        public async Task Test3()
        {
            _messsageServiceMock.Setup(repo => repo.GetMessageById(1)).ReturnsAsync(
                new MessageOutputDto
                {
                    Id = 1,
                    Text = "Hello, Sasha",
                    ChatId = 1,
                    UserCreatorId = 1,
                    IsDeleted = false,
                    CreateTime = System.DateTime.UtcNow
                }).Verifiable();

            var response = await _messageController.GetMessage(1);
            //var a = await response.ExecuteResultAsync(null);
            //var viewResult = Assert.IsType<ViewResult>(result);
            _messsageServiceMock.VerifyAll();
        }
        [Test]
        public async Task Test4()
        {
            var modelAdd = new MessageInputDto
            {
                Text = "Hello, Sasha",
                ChatId = 1,
                UserCreatorId = 1,
                IsDeleted = false
            };
            _messsageServiceMock.Setup(repo => repo.AddMessage(modelAdd));

            await _messageController.AddMessage(modelAdd);

            _messsageServiceMock.VerifyAll();
        }

        [Test]
        public async Task Test5()
        {
            _messsageServiceMock.Setup(repo => repo.GetAllMessageByChatId(1)).ReturnsAsync(new List<MessageOutputDto>
            {
                new MessageOutputDto{Id = 1,
                Text = "Hello, Sasha",
                ChatId = 1,
                UserCreatorId = 1,
                IsDeleted = false,
                CreateTime = System.DateTime.UtcNow }
                
            });

            await _messageController.GetAllMessageByChatId(1);

            _messsageServiceMock.VerifyAll();
        }
    }
}