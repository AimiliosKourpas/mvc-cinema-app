using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyCinemaApp.Models;

public partial class ApplicationDBContext : DbContext
{
    public ApplicationDBContext()
    {
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Cinema> Cinemas { get; set; }

    public virtual DbSet<ContentAdmin> ContentAdmins { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Provole> Provoles { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=master;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ADMINS__3214EC2756D6A4C6");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.UserUsernameNavigation).WithMany(p => p.Admins)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ADMINS__user_use__147C05D0");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        j.IndexerProperty<string>("UserId")
                            .HasMaxLength(45)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CINEMAS__3214EC2755DCFB16");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ContentAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CONTENT___3214EC27261122B4");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.UserUsernameNavigation).WithMany(p => p.ContentAdmins)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CONTENT_A__user___1A34DF26");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3214EC27886E487D");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.UserUsernameNavigation).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CUSTOMERS__user___1758727B");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Name }).HasName("PK__MOVIES__853AFED64A2034D8");

            entity.HasOne(d => d.ContentAdmin).WithMany(p => p.Movies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MOVIES__content___1D114BD1");
        });

        modelBuilder.Entity<Provole>(entity =>
        {
            entity.HasKey(e => new { e.MoviesId, e.CinemasId, e.MoviesName }).HasName("PK__PROVOLES__EF31248C8E2B8783");

            entity.HasOne(d => d.Cinemas).WithMany(p => p.Provoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PROVOLES__cinema__22CA2527");

            entity.HasOne(d => d.ContentAdmin).WithMany(p => p.Provoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PROVOLES__conten__23BE4960");

            entity.HasOne(d => d.Movie).WithMany(p => p.Provoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PROVOLES__21D600EE");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => new { e.ProvolesMoviesId, e.ProvolesCinemasId, e.CustomersId }).HasName("PK__RESERVAT__9EC6595B3AC41345");

            entity.HasOne(d => d.Customers).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RESERVATI__custo__278EDA44");

            entity.HasOne(d => d.Provole).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RESERVATIONS__269AB60B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__USERS__F3DBC573B1C0182E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
