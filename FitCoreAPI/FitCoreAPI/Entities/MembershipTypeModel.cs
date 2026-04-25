using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitCore_API.Entities.Auxiliary;

namespace FitCore_API.Entities;


[Table("membership_types", Schema = "public")]
public class MembershipTypeModel
{
    [Key]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("name")]
    [MaxLength(50)]
    public required string Name { get; set; }
    
    [Required]
    [Column("description")]
    [MaxLength(500)]
    public required string Description { get; set; }
    
    [Required]
    [Column("duration")]
    public required int Duration { get; set; }
    
    [Required]
    [Column("price")]
    public required float Price { get; set; }

    public ICollection<ClientMembership> ClientMemberships { get; set; } = new List<ClientMembership>();
}