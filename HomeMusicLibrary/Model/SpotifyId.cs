using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HomeMusicLibrary.Model;

public class SpotifyId
{
    [Key, Required]
    public string Id { get; set; }
    
    [AllowNull]
    public string ClientSecret { get; set; }
    
    [AllowNull]
    public string ClientId { get; set; }
}