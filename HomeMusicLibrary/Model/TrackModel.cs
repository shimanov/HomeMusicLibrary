using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HomeMusicLibrary.Model;

public class TrackModel
{
    [Key, Required]
    public string Id { get; set; }
    
    [AllowNull]
    public string AlbumId { get; set; }
    
    [AllowNull]
    public int TrackNumber { get; set; }
    
    [AllowNull]
    public string TrackName { get; set; }
    
    [AllowNull]
    public int Duration { get; set; }
}