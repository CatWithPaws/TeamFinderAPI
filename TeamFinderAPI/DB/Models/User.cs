using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TeamFinderAPI.DB.Models;

public sealed partial class User{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [Required]
    public string Login { get; set; } = string.Empty;
    public byte[] Password { get; set; } = {};
    [Required]
    public string Email { get; set; } = string.Empty;
    
    public string GoogleId { get; set; } = string.Empty;
    public ICollection<Post> Posts{ get;}
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiration { get; set; } = DateTime.MinValue;

    public string TelegramLink;
    public string DiscordUsername;

}