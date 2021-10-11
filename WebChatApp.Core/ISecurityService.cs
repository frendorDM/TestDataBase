using System;
using System.Collections.Generic;
using System.Text;

namespace WebChatApp.Core
{
    public interface ISecurityService
    {
        string GetHash(string password);
        bool VerifyHashAndPassword(string hashedPwdFromDatabase, string userEnteredPassword);
    }
}
