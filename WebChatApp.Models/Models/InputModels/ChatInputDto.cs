using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebChatApp.Models.Models.InputModels
{
    public class ChatInputDto
    {
        [Required]
        [StringLength(50)]
        public string ChatName { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int UserCreatorId { get; set; }
        [Required]
        [Range(1, 3)]
        public int Type { get; set; }

        public List<int> Users { get; set; }
        public List<MessageInputDto> Messages { get; set; }
    }
}
