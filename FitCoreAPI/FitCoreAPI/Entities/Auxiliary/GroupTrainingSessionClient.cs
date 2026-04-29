using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitCore_API.Entities;

namespace FitCoreAPI.Entities.Auxiliary;


[Table("group_training_session_client", Schema = "public")]
public class GroupTrainingSessionClient
{
    [Required]
    [Column("group_training_session_id")]
    public required Guid GroupTrainingSessionId { get; set; }
    
    [Required]
    [Column("client_id")]
    public required Guid ClientId { get; set; }

    public ClientModel Client { get; set; } = null!;
    public GroupTrainingSessionModel GroupTrainingSession { get; set; }  = null!;
}