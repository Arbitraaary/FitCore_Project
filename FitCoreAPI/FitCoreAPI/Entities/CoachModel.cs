using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCore_API.Entities;

[Table("coaches", Schema = "public")]
public class CoachModel
{
    [Key]
    [Column("user_id")]
    public required Guid UserId { get; set; }
    
    [Required]
    [Column("specialization")]
    public required ESpecializationType Specialization { get; set; }
    
    [Required]
    [Column("location_name")]
    [MaxLength(150)]
    public required string LocationName { get; set; }
    
    public UserModel User { get; set; } = null!;
    public LocationModel Location { get; set; } = null!;
    public ICollection<PersonalTrainingSessionModel> PersonalTrainingSessions { get; set; } = new List<PersonalTrainingSessionModel>();
    public ICollection<GroupTrainingSessionModel> GroupTrainingSessions { get; set; } = new List<GroupTrainingSessionModel>();
}