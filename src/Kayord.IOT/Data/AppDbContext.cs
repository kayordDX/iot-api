using System.Reflection;
using Kayord.IOT.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.IOT.Data;

public class AppDbContext : DbContext
{
    public DbSet<Entity> Entity => Set<Entity>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}