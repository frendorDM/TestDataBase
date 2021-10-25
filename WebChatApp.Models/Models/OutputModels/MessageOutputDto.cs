using System;
using System.Collections.Generic;
using System.Text;

namespace WebChatApp.Models.Models.OutputModels
{
    public class MessageOutputDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
        public int UserCreatorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
