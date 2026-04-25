using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCore_API.Entities.Auxiliary;

[Table("client_membership", Schema = "public")]
public class ClientMembership
{
    [Key]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("membership_type_id")]
    public required Guid MembershipTypeId { get; set; }
    
    [Required]
    [Column("client_id")]
    public required Guid ClientId { get; set; }    
    
    //дата, без часу
    [Required]
    [Column("start_date")]
    public required DateOnly StartDate { get; set; }
    
    [Required]
    [Column("end_date")]
    public required DateOnly EndDate { get; set; }
    
    [Required]
    [Column("status")]
    public required EMembershipStatus Status { get; set; }

    public ClientModel Client { get; set; } = null!;
    public MembershipTypeModel MembershipType { get; set; } = null!;
}
