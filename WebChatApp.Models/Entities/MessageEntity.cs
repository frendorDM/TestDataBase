using System;
using System.Collections.Generic;
using System.Text;

namespace WebChatApp.Models.Entities
{
    public class MessageEntity : BaseEntity
    {
        public string Text { get; set; }
        public int ChatId { get; set; }
        public int UserCreatorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public UserEntity UserCreator { get; set; }
        public ChatEntity Chat { get; set; }
    }
}
