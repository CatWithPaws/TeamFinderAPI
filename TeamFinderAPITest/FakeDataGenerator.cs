using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamFinderAPI.DB.Models;
using Bogus;
using System.Text;

namespace TeamFinderAPITest
{
    public class FakeDataGenerator 
    {
        public static User GenerateUser(){
            User newUser = new Faker<User>()
                .RuleFor(x => x.Login, f => f.Lorem.Word())
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Password, f => Encoding.UTF8.GetBytes(f.Internet.Password()))
                .RuleFor(x => x.DisplayName, f => f.Internet.UserName())
                .RuleFor(x => x.DiscordUsername, f => f.Internet.UserName())
                .RuleFor(x => x.TelegramLink, f => f.Internet.Url());
            return newUser;
        }
    }
}