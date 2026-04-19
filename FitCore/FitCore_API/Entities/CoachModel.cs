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
    
    public UserModel User { get; set; } = null!;
    public PersonalTrainingSessionModel? PersonalTrainingSession { get; set; }
    public GroupTrainingSessionModel? GroupTrainingSession { get; set; }
}