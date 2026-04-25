namespace FitCore_API.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("room_equipments", Schema = "public")]
public class RoomEquipmentModel
{
    [Key]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("location_id")]
    public required Guid LocationId { get; set; }
    
    [Required]
    [Column("room_id")]
    public required Guid RoomId { get; set; }
    
    [Required]
    [Column("equipment_type")]
    public required EEquipmentType EquipmentType { get; set; }
    
    [Required]
    [Column("quantity")]
    public required int Quantity { get; set; }
    
    public LocationModel Location { get; set; } = null!;
    public RoomModel Room { get; set; } = null!;
    public ICollection<OccupiedEquipmentModel> OccupiedEquipment { get; set; } = new List<OccupiedEquipmentModel>();
}