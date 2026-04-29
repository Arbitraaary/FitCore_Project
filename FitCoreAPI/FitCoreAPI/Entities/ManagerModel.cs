using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCore_API.Entities;

[Table("managers", Schema = "public")]
public class ManagerModel
{
    [Key]
    [Column("user_id")]
    public required Guid UserId { get; set; }
    
    [Required] 
    [Column("location_name")]
    [MaxLength(150)]
    public required string LocationName { get; set; }
    
    public UserModel User { get; set; } = null!;
    public LocationModel Location { get; set; } = null!;
}