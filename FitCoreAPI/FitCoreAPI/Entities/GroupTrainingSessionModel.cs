using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitCore_API.Entities.Auxiliary;

namespace FitCore_API.Entities;

[Table("group_training_sessions", Schema = "public")]
public class GroupTrainingSessionModel
{
    [Key]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("coach_id")]
    public required Guid CoachId { get; set; }
    
    [Required]
    [Column("room_id")]
    public required Guid RoomId { get; set; }
    
    [Required]
    [Column("name")]
    [MaxLength(150)]
    public required string Name { get; set; }
    
    [Required]
    [Column("description")]
    [MaxLength(500)]
    public required string Description { get; set; }

    [Required]
    [Column("capacity")]
    public required int Capacity { get; set; }
    
    [Required]
    [Column("start_time")]
    public DateTime StartTime { get; set; }
    
    [Required]
    [Column("end_time")]
    public DateTime EndTime { get; set; }
    
    public CoachModel? Coach { get; set; }
    public RoomModel Room { get; set; } = null!;
    public ICollection<OccupiedEquipmentModel> OccupiedEquipments { get; set; } = new List<OccupiedEquipmentModel>();
    public ICollection<GroupTrainingSessionClient> ClientGroupSessions { get; set; } = new List<GroupTrainingSessionClient>();
}