using QLDaoTao.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QLDaoTao.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppStudent> Students { get; set; }
    public DbSet<AppUnit> Units { get; set; }
    public DbSet<AppCategorie> Categories { get; set; }
    public DbSet<AppNew> News { get; set; }
    public DbSet<AppTeacher> Teachers { get; set; }
    public DbSet<AppBanner> Banners { get; set; }
    public DbSet<AppLink> Links { get; set; }
    public DbSet<AppBackup> Backups { get; set; }
}
