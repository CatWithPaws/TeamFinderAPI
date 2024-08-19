using System.ComponentModel.DataAnnotations;

namespace TeamFinderAPI.Data;

public sealed class User{
    public int ID { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
}