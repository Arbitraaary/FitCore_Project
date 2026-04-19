using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCore_API.Entities;


[Table("rooms", Schema = "public")]
public class RoomModel
{
    [Key]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("location_id")]
    public required Guid LocationId { get; set; }
    
    [Required]
    [Column("room_type")]
    public required ERoomType RoomType { get; set; }
    
    [Required]
    [Column("capacity")]
    public required int Capacity { get; set; }

    public LocationModel Location { get; set; } = null!;
    public ICollection<GroupTrainingSessionModel> GroupTrainingSessions { get; set; } = new List<GroupTrainingSessionModel>();
    public ICollection<PersonalTrainingSessionModel> PersonalTrainingSessions { get; set; } = new List<PersonalTrainingSessionModel>();
    public ICollection<RoomEquipmentModel> RoomEquipments { get; set; } = new List<RoomEquipmentModel>();
}