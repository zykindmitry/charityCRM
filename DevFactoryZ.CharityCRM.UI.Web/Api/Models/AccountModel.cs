﻿using DevFactoryZ.CharityCRM.Services;

namespace DevFactoryZ.CharityCRM.UI.Web.Api.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
        }

        public string Login { get; set; }

        public char[] PasswordClearText { get; set; }

        public IPasswordConfig PasswordConfig { get; set; }

        public AccountData ToDtoForUpdate()
        {
            return new AccountData
            {
                Login = Login,
                Password = new Password(PasswordConfig, PasswordClearText)
            };
        }

        public AccountData ToDtoForCreate()
        {
            return new AccountData
            {
                Login = Login,
                Password = new Password(PasswordConfig)
            };
        }
    }
}