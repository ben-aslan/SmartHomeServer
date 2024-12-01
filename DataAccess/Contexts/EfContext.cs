using Entities.Concrete;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts;

public class EfContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(ConnectionStrings.ConnectionString, new MySqlServerVersion(new Version(8, 0, 39))/*, (e) => { e.EnableRetryOnFailure(5, TimeSpan.FromSeconds(100), null); }*/);
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<StepIndex> StepIndices { get; set; }
    public DbSet<UserStep> UserSteps { get; set; }
    public DbSet<Bot> Bots { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Step datas
        modelBuilder.Entity<Step>().HasData(new Step() { Id = (int)EStep.Home, Name = "Home", Description = "Home" });
        modelBuilder.Entity<Step>().HasData(new Step() { Id = (int)EStep.GroupVideoFirstAlgorithm, Name = "GroupVideoFirstAlgorithm", Description = "GroupVideoFirstAlgorithm" });
        modelBuilder.Entity<Step>().HasData(new Step() { Id = (int)EStep.GroupVideoSecondAlgorithm, Name = "GroupVideoSecondAlgorithm", Description = "GroupVideoSecondAlgorithm" });
        modelBuilder.Entity<Step>().HasData(new Step() { Id = (int)EStep.Video, Name = "Video", Description = "Video" });
        #endregion

        #region StepIndex datas
        modelBuilder.Entity<StepIndex>().HasData(new StepIndex() { Id = (int)EStepIndex.Home, Name = "Home", Description = "Home" });
        modelBuilder.Entity<StepIndex>().HasData(new StepIndex() { Id = (int)EStepIndex.UploadWithSameFileId, Name = "UploadWithSameFileId", Description = "UploadWithSameFileId" });
        #endregion

        #region OperationClaim datas
        modelBuilder.Entity<OperationClaim>().HasData(new OperationClaim { Id = (int)EOperationClaim.User, Name = "user", Periority = 3 });
        modelBuilder.Entity<OperationClaim>().HasData(new OperationClaim { Id = (int)EOperationClaim.Admin, Name = "admin", Periority = 2 });
        modelBuilder.Entity<OperationClaim>().HasData(new OperationClaim { Id = (int)EOperationClaim.Owner, Name = "owner", Periority = 1 });
        #endregion

        #region Language datas
        modelBuilder.Entity<Language>().HasData(new Language { Id = (int)ELang.EN, LangCode = "EN-US" });
        modelBuilder.Entity<Language>().HasData(new Language { Id = (int)ELang.FA, LangCode = "FA-IR" });
        modelBuilder.Entity<Language>().HasData(new Language { Id = (int)ELang.RU, LangCode = "RU-RU" });
        modelBuilder.Entity<Language>().HasData(new Language { Id = (int)ELang.ZH, LangCode = "ZN-HN" });
        #endregion

        #region Language relations
        //modelBuilder.Entity<User>()
        //   .HasOne(x => x.Language)
        //   .WithMany(x => x.Users)
        //   .HasForeignKey(x => x.LanguageId)
        //   .OnDelete(DeleteBehavior.ClientSetNull);
        #endregion

        base.OnModelCreating(modelBuilder);
    }
}
