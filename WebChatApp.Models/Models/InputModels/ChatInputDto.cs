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
        public int UserCreatorId { get; set; }

        public List<int> Users { get; set; }
    }
}
