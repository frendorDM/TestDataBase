using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebChatApp.Models.Models.InputModels
{
    public class UpdateChatNameInputDto
    {
        [Required]
        [StringLength(50)]
        public string ChatName { get; set; }
    }
}
