using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebChatApp.Core
{
    public class AuthOptions
    {
        public const string ISSUER = "TestDataBase";
        public const string AUDIENCE = "SimbirChat";
        const string KEY = "djwaoijdajwdiawjdiawjdwaidjawodjawdjawodjoa*%#$$@efwsefw";
        public const int LIFETIME = 2880;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
