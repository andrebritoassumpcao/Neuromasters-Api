using Microsoft.EntityFrameworkCore;
using neuromasters.borders.Entities.Questionnaires;

namespace neuromasters.repositories;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<SkillGroup> SkillGroups { get; set; }
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<FormSection> FormSections { get; set; }
    public DbSet<FormQuestion> FormQuestions { get; set; }

}


