using Microsoft.EntityFrameworkCore;
using TeamFinderAPI.DB.Models;


namespace TeamFinderAPI.Data;

public class TeamFindAPIContext : DbContext
{

   public TeamFindAPIContext(DbContextOptions<TeamFindAPIContext> options) : base(options)
        {
        }

    public DbSet<User> Users { get; set;}
    public DbSet<Post> Posts{ get; set;}


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            
#if DEBUG
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
#else
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresProd"));
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>()
                .HasMany(e => e.Posts)
                .WithOne(e=> e.CreatedBy)
                .HasForeignKey(e=>e.CreatedById);
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Login)
                .IsUnique();
            modelBuilder.Entity<Post>().ToTable("Post");
        }
}