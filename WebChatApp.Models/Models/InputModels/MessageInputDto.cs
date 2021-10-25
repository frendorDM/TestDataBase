using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebChatApp.Models.Models.InputModels
{
    public class MessageInputDto
    {
        [Required]
        [StringLength(1000)]
        public string Text { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ChatId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int UserCreatorId { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

    }
}
