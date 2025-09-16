using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using neuromasters.borders.Entities;

namespace neuromasters.repositories;

public class AuthDbContext(DbContextOptions<AuthDbContext> options)
    : IdentityDbContext<User>(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Users
        builder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.Property(u => u.UserName).HasMaxLength(150);
        });

        // Roles
        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable("Roles");
        });

        // Many-to-Many User ↔ Role
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
        });

        // Claims por User
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });

        // Claims por Role
        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });

        // External Logins (Google, Facebook etc.)
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });

        // User Tokens (reset de senha, email confirmation, 2FA)
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });
    }
}