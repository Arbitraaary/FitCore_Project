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
    [Column("location_name")]
    [MaxLength(150)]
    public required string LocationName { get; set; }
    
    [Required]
    [Column("equipment_type")]
    public required EEquipmentType EquipmentType { get; set; }
    
    [Required]
    [Column("quantity")]
    public required int Quantity { get; set; }
    
    public LocationModel Location { get; set; } = null!;
}