using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCore_API.Entities;


[Table("equipments", Schema = "public")]
public class EquipmentModel
{
    [Key]
    [Column("id")]
    public required int Id { get; set; }
    
    [Required]
    [Column("location_id")]
    public required Guid LocationId { get; set; }
    
    [Required]
    [Column("equipment_type")]
    public required EEquipmentType EquipmentType { get; set; }
    
    [Required]
    [Column("quantity")]
    public required int Quantity { get; set; }
    
    public LocationModel Location { get; set; } = null!;
}