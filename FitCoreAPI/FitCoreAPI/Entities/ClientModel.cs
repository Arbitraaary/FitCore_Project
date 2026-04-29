using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitCore_API.Entities.Auxiliary;
using FitCoreAPI.Entities.Auxiliary;

namespace FitCore_API.Entities;

[Table("clients", Schema = "public")]
public class ClientModel
{
    [Key]
    [Column("user_id")]
    public required Guid UserId { get; set; }
    
    public UserModel User { get; set; } = null!;
    public ICollection<GroupTrainingSessionClient> ClientGroupSessions { get; set; } = new List<GroupTrainingSessionClient>();
    //історія покупок абонементів
    public ICollection<ClientMembership> ClientMemberships { get; set; } = new List<ClientMembership>();
    public ICollection<PersonalTrainingSessionModel> PersonalTrainingSessions { get; set; } = new List<PersonalTrainingSessionModel>();
}