using System;
using System.Collections.Generic;
using System.Text;
using WebChatApp.Models.RelationShip;

namespace WebChatApp.Models.Entities
{
    public class ChatEntity : BaseEntity
    {
        public int? UserCreatorId { get; set; }
        public int Type { get; set; }

        public UserEntity UserCreator { get; set; }
        public List<UserChat> Users { get; set; }
        public List<MessageEntity> Messages { get; set; }
    }
}
