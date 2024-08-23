using Microsoft.EntityFrameworkCore;


namespace TeamFinderAPI.Data;

public class TeamFindAPIContext : DbContext
{

   public TeamFindAPIContext(DbContextOptions<TeamFindAPIContext> options) : base(options)
        {
        }

    public DbSet<User> Users { get; set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
}