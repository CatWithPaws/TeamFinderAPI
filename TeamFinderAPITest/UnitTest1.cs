using Google.Apis.Util;
using TeamFinderAPI.Data;
using TeamFinderAPI.DB.Models;
namespace TeamFinderAPITest;

public class Tests
{

    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Test]
    public void Convert_UserToUserDTO_Equals(){
        User user = FakeDataGenerator.GenerateUser();
        UserDTO userDTO = user.ToDTO();
        
        Assert.That(userDTO.Name.Equals(user.Login), "User's DisplayName and UserDTO's name not equal");
        Assert.That(userDTO.DisplayName.Equals(user.DisplayName), "User's DisplayName and UserDTO's DisplayName not equal");
        Assert.That(userDTO.DiscordUsername.Equals(user.DiscordUsername), "User's DiscordUsername and UserDTO's DiscordUsername not equal");
        Assert.That(userDTO.Email.Equals(user.Email), "User's Email and UserDTO's Email not equal");
        Assert.That(userDTO.TelegramLink.Equals(user.TelegramLink), "User's TelegramLink and UserDTO's TelegramLink not equal");
        
    }
}