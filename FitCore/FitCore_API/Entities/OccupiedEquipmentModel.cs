namespace FitCore_API.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("occupied_equipments", Schema = "public")]
public class OccupiedEquipmentModel
{
    [Key]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("session_id")]
    public required Guid SessionId { get; set; }
    
    [Required]
    [Column("equipment_id")]
    public required Guid EquipmentId { get; set; }
    
    [Required]
    [Column("quantity")]
    public required int Quantity { get; set; }
    
    public GroupTrainingSessionModel GroupTrainingSession { get; set; } = null!;
    public RoomEquipmentModel RoomEquipment { get; set; } = null!;
}