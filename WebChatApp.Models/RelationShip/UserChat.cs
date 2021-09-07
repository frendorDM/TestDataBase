using System;
using System.Collections.Generic;
using System.Text;
using WebChatApp.Models.Entities;

namespace WebChatApp.Models.RelationShip
{
    public class UserChat : BaseEntity
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public ChatEntity Chat { get; set; }
        public UserEntity User { get; set; }
    }
}
