using Microsoft.EntityFrameworkCore;


namespace TeamFinderAPI.Data;

public class TeamFindAPIContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}