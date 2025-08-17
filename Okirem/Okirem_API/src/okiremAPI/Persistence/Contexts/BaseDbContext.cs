using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<AdminProfile> AdminProfiles { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<ParentProfile> ParentProfiles { get; set; }
    public DbSet<ParentStudentLink> ParentStudentLinks { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<StudentProfile> StudentProfiles { get; set; }
    public DbSet<TeacherParentLink> TeacherParentLinks { get; set; }
    public DbSet<TeacherProfile> TeacherProfiles { get; set; }
    public DbSet<TeacherStudentLink> TeacherStudentLinks { get; set; }
    public DbSet<FeatureFlag> FeatureFlags { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
