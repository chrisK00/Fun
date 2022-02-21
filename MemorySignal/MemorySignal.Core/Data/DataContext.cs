using Microsoft.EntityFrameworkCore;

namespace MemorySignal.Core.Data;

public class DataContext : DbContext
{
    public DbSet<CardCollection> CardCollections { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Card>().ToTable("Cards");

        builder.Entity<Card>()
            .Property(x => x.ImageUrl).IsUnicode(false);

        builder.Entity<CardCollection>()
            .Property(x => x.Name).HasMaxLength(128);

        builder.Entity<CardCollection>().HasIndex(x => x.Name);
    }
}