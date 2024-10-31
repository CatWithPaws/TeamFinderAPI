using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TeamFinderAPI.DB.Models;

public class User{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Login { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public byte[] Password { get; set; } = {};
    [Required]
    public string Email { get; set; } = string.Empty;
    public string GoogleId { get; set; } = string.Empty;
    public virtual List<Post> Posts{ get;} = new List<Post>();
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiration { get; set; } = DateTime.MinValue;

    public string TelegramLink;
    public string DiscordUsername;

}