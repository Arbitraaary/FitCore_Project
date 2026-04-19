using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCore_API.Entities;

[Table("locations", Schema = "public")]
public class LocationModel
{
    [Key]
    [Column("id")]
    public required Guid Id { get; set; }
    
    [Required]
    [Column("name")]
    [MaxLength(150)]
    public required string Name { get; set; }
    
    [Required]
    [Column("address")]
    [MaxLength(150)]
    public required string Address { get; set; }
    
    public ICollection<RoomModel> Rooms { get; set; } = new List<RoomModel>();
    public ICollection<ManagerModel> Managers { get; set; } = new List<ManagerModel>();
    public ICollection<EquipmentModel> Equipments { get; set; } = new List<EquipmentModel>();
    public ICollection<RoomEquipmentModel> RoomEquipments { get; set; } = new List<RoomEquipmentModel>();
}