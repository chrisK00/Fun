namespace MemorySignal.Core.Data;

public class DataContext : DbContext
{
    public DbSet<CardCollection> CardCollections { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Card>()
            .ToTable("Cards");

        builder.Entity<Card>()
            .Property(x => x.ImageUrl).IsUnicode(false).IsRequired();

        builder.Entity<Card>()
            .Ignore(x => x.TempId);

        builder.Entity<Card>()
            .Property(x => x.ApiId).IsRequired();

        builder.Entity<Card>()
            .HasIndex(x => x.ApiId);

        builder.Entity<CardCollection>()
            .Property(x => x.Name).HasMaxLength(128).IsRequired();

        builder.Entity<CardCollection>()
            .HasIndex(x => x.Name);
    }
}