using System;
using System.Collections.Generic;
using System.Text;

namespace WebChatApp.Models.Models.OutputModels
{
    public class UserOutputDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Login { get; set; }
        public List<int> Roles { get; set; }
    }
}
