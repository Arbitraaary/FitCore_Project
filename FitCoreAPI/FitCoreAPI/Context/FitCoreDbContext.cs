using FitCore_API.Entities;
using FitCore_API.Entities.Auxiliary;
using FitCoreAPI.Entities.Auxiliary;

namespace FitCore_API.Context;
using Microsoft.EntityFrameworkCore;

public sealed class FitCoreDbContext: DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<ClientModel> Clients { get; set; }
    public DbSet<AdminModel> Admins { get; set; }
    public DbSet<ManagerModel> Managers { get; set; }
    public DbSet<CoachModel> Coaches { get; set; }
    public DbSet<LocationModel> Locations { get; set; }
    public DbSet<RoomModel> Rooms { get; set; }
    public DbSet<EquipmentModel> Equipments { get; set; }
    public DbSet<RoomEquipmentModel> RoomEquipments { get; set; }
    public DbSet<OccupiedEquipmentModel> OccupiedEquipments { get; set; }
    public DbSet<MembershipTypeModel> MembershipTypes { get; set; }
    public DbSet<PersonalTrainingSessionModel> PersonalTrainingSessions { get; set; }
    public DbSet<GroupTrainingSessionModel> GroupTrainingSessions { get; set; }
    public DbSet<ClientMembership> ClientMemberships { get; set; }
    public DbSet<GroupTrainingSessionClient> GroupTrainingSessionClients { get; set; }

    public FitCoreDbContext()
    {
        Database.EnsureCreated();
    }

    public FitCoreDbContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated(); 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.HasPostgresExtension("uuid-ossp");
        
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id)
                  .HasDefaultValueSql("uuid_generate_v4()");

            entity.Property(u => u.Email).IsRequired().HasMaxLength(254);
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            entity.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(15);
            entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(44);
            entity.Property(u => u.PasswordSalt).IsRequired().HasMaxLength(24);
            entity.Property(u => u.UserType).IsRequired()
                  .HasConversion(
                  v => v.ToString().ToLower(),
                  v => (EUserType)Enum.Parse(typeof(EUserType), v, true));
            
            entity.HasIndex(u => u.Email).IsUnique();
            
            entity.HasIndex(u => u.PhoneNumber).IsUnique();

            entity.HasOne(u => u.Client)
                  .WithOne(c => c.User)
                  .HasForeignKey<ClientModel>(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(u => u.Admin)
                  .WithOne(a => a.User)
                  .HasForeignKey<AdminModel>(a => a.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(u => u.Manager)
                  .WithOne(m => m.User)
                  .HasForeignKey<ManagerModel>(m => m.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(u => u.Coach)
                  .WithOne(c => c.User)
                  .HasForeignKey<CoachModel>(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AdminModel>(entity =>
        {
            entity.HasKey(a => a.UserId);
        });

        modelBuilder.Entity<ManagerModel>(entity =>
        {
            entity.HasKey(m => m.UserId);

            entity.HasOne(m => m.Location)
                  .WithMany(l => l.Managers)
                  .HasForeignKey(m => m.LocationName)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CoachModel>(entity =>
        {
            entity.HasKey(c => c.UserId);
            entity.Property(c => c.Specialization)
                  .IsRequired()
                  .HasConversion<string>();
            entity.HasOne(c => c.Location)
                  .WithMany(l => l.Coaches)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ClientModel>(entity =>
        {
            entity.HasKey(c => c.UserId);
        });

        modelBuilder.Entity<LocationModel>(entity =>
        {
            entity.HasKey(l => l.Name);

            entity.Property(l => l.Name).IsRequired().HasMaxLength(150);
            entity.Property(l => l.Address).IsRequired().HasMaxLength(150);
        });

        modelBuilder.Entity<RoomModel>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id)
                  .HasDefaultValueSql("uuid_generate_v4()");

            entity.Property(r => r.RoomType)
                  .IsRequired()
                  .HasConversion<string>();

            entity.HasOne(r => r.Location)
                  .WithMany(l => l.Rooms)
                  .HasForeignKey(r => r.LocationName)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EquipmentModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EquipmentType)
                  .IsRequired()
                  .HasConversion<string>();

            entity.HasOne(e => e.Location)
                  .WithMany(l => l.Equipments)
                  .HasForeignKey(e => e.LocationName)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RoomEquipmentModel>(entity =>
        {
            entity.HasKey(re => re.Id);
            entity.Property(re => re.Id)
                  .HasDefaultValueSql("uuid_generate_v4()");

            entity.Property(re => re.EquipmentType)
                  .IsRequired()
                  .HasConversion<string>();

            entity.HasOne(re => re.Location)
                  .WithMany(l => l.RoomEquipments)
                  .HasForeignKey(re => re.LocationName)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(re => re.Room)
                  .WithMany(r => r.RoomEquipments)
                  .HasForeignKey(re => re.RoomId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OccupiedEquipmentModel>(entity =>
        {
            entity.HasKey(oe => oe.Id);
            entity.Property(oe => oe.Id)
                  .HasDefaultValueSql("uuid_generate_v4()");

            entity.HasOne(oe => oe.GroupTrainingSession)
                  .WithMany(g => g.OccupiedEquipments)
                  .HasForeignKey(oe => oe.SessionId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(oe => oe.RoomEquipment)
                  .WithMany(re => re.OccupiedEquipment)
                  .HasForeignKey(oe => oe.EquipmentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<MembershipTypeModel>(entity =>
        {
            entity.HasKey(mt => mt.Id);
            entity.Property(mt => mt.Name).IsRequired().HasMaxLength(50);
            entity.Property(mt => mt.Description).IsRequired().HasMaxLength(500);
        });

        modelBuilder.Entity<ClientMembership>(entity =>
        {
            entity.HasKey(cm => cm.Id);
            entity.Property(cm => cm.Id)
                  .HasDefaultValueSql("uuid_generate_v4()");

            entity.Property(cm => cm.Status)
                  .IsRequired()
                  .HasConversion<string>();

            entity.HasOne(cm => cm.Client)
                  .WithMany(c => c.ClientMemberships)
                  .HasForeignKey(cm => cm.ClientId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(cm => cm.MembershipType)
                  .WithMany(mt => mt.ClientMemberships)
                  .HasForeignKey(cm => cm.MembershipTypeId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PersonalTrainingSessionModel>(entity =>
        {
            entity.HasKey(pt => pt.Id);
            entity.Property(pt => pt.Id)
                  .HasDefaultValueSql("uuid_generate_v4()");

            entity.Property(pt => pt.Name).IsRequired().HasMaxLength(150);

            entity.HasOne(pt => pt.Client)
                  .WithMany(pt => pt.PersonalTrainingSessions)
                  .HasForeignKey(pt => pt.ClientId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(pt => pt.Coach)
                  .WithMany(c => c.PersonalTrainingSessions)
                  .HasForeignKey(pt => pt.CoachId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(pt => pt.Room)
                  .WithMany(r => r.PersonalTrainingSessions)
                  .HasForeignKey(pt => pt.RoomId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GroupTrainingSessionModel>(entity =>
        {
            entity.HasKey(gt => gt.Id);
            entity.Property(gt => gt.Id)
                  .HasDefaultValueSql("uuid_generate_v4()");

            entity.Property(gt => gt.Name).IsRequired().HasMaxLength(150);
            entity.Property(gt => gt.Description).IsRequired().HasMaxLength(500);

            entity.HasOne(gt => gt.Coach)
                  .WithMany(c => c.GroupTrainingSessions)
                  .HasForeignKey(gt => gt.CoachId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(gt => gt.Room)
                  .WithMany(r => r.GroupTrainingSessions)
                  .HasForeignKey(gt => gt.RoomId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GroupTrainingSessionClient>(entity =>
        {
            entity.HasKey(gsc => new { gsc.GroupTrainingSessionId, gsc.ClientId });

            entity.HasOne(gsc => gsc.Client)
                  .WithMany(c => c.ClientGroupSessions)
                  .HasForeignKey(gsc => gsc.ClientId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(gsc => gsc.GroupTrainingSession)
                  .WithMany(gt => gt.ClientGroupSessions)
                  .HasForeignKey(gsc => gsc.GroupTrainingSessionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}