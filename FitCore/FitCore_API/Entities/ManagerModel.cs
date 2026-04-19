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
    [Column("location_id")]
    public required Guid LocationId { get; set; }
    
    public UserModel User { get; set; } = null!;
    public LocationModel Location { get; set; } = null!;
}