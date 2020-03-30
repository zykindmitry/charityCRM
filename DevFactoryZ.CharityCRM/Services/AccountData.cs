using System;
using System.Collections.Generic;
using System.Text;

namespace DevFactoryZ.CharityCRM.Services
{
    public class AccountData
    {
        public string Login { get; set; }

        public char[] PasswordClearText { get; set; }

        public IPasswordConfig PasswordConfig { get; set; }

        public Password Password { get; set; }
    }
}
