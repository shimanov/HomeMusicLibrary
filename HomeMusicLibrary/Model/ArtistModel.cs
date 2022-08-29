using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HomeMusicLibrary.Model;

public class ArtistModel
{
    [Required, Key]
    public string Id { get; set; }
    
    [AllowNull]
    public string ArtistName { get; set; }
    
    [AllowNull]
    public string ArtistId { get; set; }
}