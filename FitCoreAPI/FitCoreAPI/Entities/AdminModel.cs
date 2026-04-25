using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCore_API.Entities;

[Table("admins", Schema = "public")]
public class AdminModel
{
    [Key]
    [Column("user_id")]
    public required Guid UserId { get; set; }

    public UserModel User { get; set; } = null!;
}