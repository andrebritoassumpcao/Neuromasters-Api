using Microsoft.EntityFrameworkCore;
using neuromasters.borders.Entities;

namespace neuromasters.repositories;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    // Adicione os DbSets para outras entidades aqui
}


