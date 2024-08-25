using Microsoft.EntityFrameworkCore;
using TeamFinderAPI.Models;


namespace TeamFinderAPI.Data;

public class TeamFindAPIContext : DbContext
{

   public TeamFindAPIContext(DbContextOptions<TeamFindAPIContext> options) : base(options)
        {
        }

    public DbSet<User> Users { get; set;}
    public DbSet<Post> Posts{ get; set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
}