using Lab11_MercyJanampa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab11_MercyJanampa.Infrastructure.Context;

public class TicketeraDbContext : DbContext
{
    public TicketeraDbContext(
        DbContextOptions<TicketeraDbContext> options)
        : base(options)
    {
    }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Response> Responses => Set<Response>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // ROLE
        // =========================
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.RoleId)
                .HasColumnName("role_id");

            entity.Property(e => e.RoleName)
                .HasColumnName("role_name")
                .HasMaxLength(50)
                .IsRequired();

            entity.HasIndex(e => e.RoleName)
                .IsUnique();
        });

        // =========================
        // USER
        // =========================
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId)
                .HasColumnName("user_id");

            entity.Property(e => e.Username)
                .HasColumnName("username")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(150);

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp without time zone");

            entity.HasIndex(e => e.Username)
                .IsUnique();

            entity.HasIndex(e => e.Email)
                .IsUnique();
        });

        // =========================
        // USER ROLE
        // =========================
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("user_roles");

            entity.HasKey(e => new
            {
                e.UserId,
                e.RoleId
            });

            entity.Property(e => e.UserId)
                .HasColumnName("user_id");

            entity.Property(e => e.RoleId)
                .HasColumnName("role_id");

            entity.Property(e => e.AssignedAt)
                .HasColumnName("assigned_at")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(e => e.User)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Role)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================
        // TICKET
        // =========================
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("tickets");

            entity.HasKey(e => e.TicketId);

            entity.Property(e => e.TicketId)
                .HasColumnName("ticket_id");

            entity.Property(e => e.UserId)
                .HasColumnName("user_id");

            entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasColumnName("description");

            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.ClosedAt)
                .HasColumnName("closed_at")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(e => e.User)
                .WithMany(e => e.Tickets)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // =========================
        // RESPONSE
        // =========================
        modelBuilder.Entity<Response>(entity =>
        {
            entity.ToTable("responses");

            entity.HasKey(e => e.ResponseId);

            entity.Property(e => e.ResponseId)
                .HasColumnName("response_id");

            entity.Property(e => e.TicketId)
                .HasColumnName("ticket_id");

            entity.Property(e => e.ResponderId)
                .HasColumnName("responder_id");

            entity.Property(e => e.Message)
                .HasColumnName("message")
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(e => e.Ticket)
                .WithMany(e => e.Responses)
                .HasForeignKey(e => e.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Responder)
                .WithMany(e => e.Responses)
                .HasForeignKey(e => e.ResponderId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}