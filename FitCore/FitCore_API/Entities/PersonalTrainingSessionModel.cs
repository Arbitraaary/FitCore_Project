using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCore_API.Entities;


[Table("personal_trainings_sessions", Schema = "public")]
public class PersonalTrainingSessionModel
{
    [Key]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("client_id")]
    public required Guid ClientId { get; set; }
    
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
    [Column("start_time")]
    public required DateTime StartDate { get; set; }
    
    [Required]
    [Column("end_time")]
    public required DateTime EndDate { get; set; }
    
    public ClientModel Client { get; set; } = null!;
    public CoachModel Coach { get; set; } = null!;
    public RoomModel Room { get; set; } = null!;
}