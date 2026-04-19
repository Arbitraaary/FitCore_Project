using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCore_API.Entities;

[Table("users", Schema = "public")]
public class UserModel
{
    [Key]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("email")]
    [MaxLength(254)]
    public required string Email { get; set; }
    
    [Required]
    [Column("first_name")]
    [MaxLength(50)]
    public required string FirstName { get; set; }
    
    [Required]
    [Column("last_name")]
    [MaxLength(50)]
    public required string LastName { get; set; }
    
    [Required]
    [Column("phone_number")]
    [MinLength(13)]
    [MaxLength(15)]
    public required string PhoneNumber { get; set; }
    
    [Required]
    [Column("user_type")]
    public required EUserType UserType { get; set; }
    
    [Required]
    [Column("password_hash")]
    [MinLength(44)]
    [MaxLength(44)]
    public required string? PasswordHash { get; set; }
    
    [Required]
    [Column("password_salt")]
    [MinLength(24)]
    [MaxLength(24)]
    public required string? PasswordSalt { get; set; }
    
    public ClientModel? Client { get; set; }
    public AdminModel? Admin { get; set; }
    public ManagerModel? Manager { get; set; }
    public CoachModel? Coach { get; set; }
}