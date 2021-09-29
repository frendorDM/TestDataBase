using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebChatApp.Core;
using WebChatApp.Core.Session;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.Models.OutputModels;

namespace WebChatApp.ServicesApp
{
    public class UserService : IUserService
    {
        private readonly ISession _session;
        private IMapper _mapper;
        public UserService(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        public async Task AddUser(UserInputDto inputModel)
        {
            var userEntity = _mapper.Map<UserEntity>(inputModel);
            await _session.AddEntityAsync(userEntity);
        }

        public int DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            var user = await _session.Query<UserEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task UpdateUserLogin( UserEntity inputModel, string newLogin)
        {
            //var userEntity = _mapper.Map<UserEntity>(inputModel);
            inputModel.Login = newLogin;
            await _session.UpdateEntity(inputModel);

        }
    }
}
