using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebChatApp.Models.RelationShip;

namespace WebChatApp.Models.Entities
{
    public class UserEntity : BaseEntity
    {

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool IsBlocked { get; set; }

        public List<UserRole> Roles { get; set; }

        public List<UserChat> Chats { get; set; }

        public List<MessageEntity> Messages { get; set; }

    }
}
