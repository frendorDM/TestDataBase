using System;
using System.Collections.Generic;
using System.Text;
using WebChatApp.Models.Entities;

namespace WebChatApp.Models.Models
{
    public class DashboardModel
    {
        public DashboardModel(string firstName, string chatName, MessageEntity lastMessage)
        {
            FirstName = firstName;
            ChatName = chatName;
            LastMessage = lastMessage;
        }
        public string FirstName { get; set; }
        public string ChatName { get; set; }
        public MessageEntity LastMessage { get; set; }
        
    }
}
