using InternIntellegence_Portfolio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace InternIntellegence_Portfolio.DbHelper.DatabaseContext
{
	public class ApplicationContext : IdentityDbContext
	{
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

            // ApplicationUser 
            builder.Entity<Achivements>().HasOne(sc => sc.User).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Contact>().HasOne(sc => sc.User).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Projects>().HasOne(sc => sc.User).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Skills>().HasOne(sc => sc.User).WithMany().OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Projects>().HasKey(sc => sc.ProjectId);
            builder.Entity<Skills>().HasKey(sc => sc.SkillId);
            builder.Entity<Contact>().HasKey(sc => sc.Id);
            builder.Entity<Achivements>().HasKey(sc => sc.Id);
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Achivements> Achivements { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Skills> Skills { get; set; }
    }
}
