using MemorySignal.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace MemorySignal.Server.Extensions;

public static class DataSeed
{
    public static void SeedDatabase(this IServiceProvider sp)
    {
        using var scope = sp.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        context.Database.Migrate();
        if (context.CardCollections.Any()) return;

        var smallAnimalCards = new List<Card>()
    {
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/bird-gd8d8e4a91_640_sctbst.jpg", "bird-gd8d8e4a91_640_sctbst"),
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/bird-g4e9c2c7b0_640_prkfyy.jpg", "bird-g4e9c2c7b0_640_prkfyy"),
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/black-and-white-warbler-gb189f6056_640_hrhl6f.jpg", "black-and-white-warbler-gb189f6056_640_hrhl6f"),
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/squirrel-gd5b52f387_640_oixdk0.jpg", "squirrel-gd5b52f387_640_oixdk0"),
    };

        var bigAnimalCards = new List<Card>()
    {
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/dog-gec88cbf11_640_ww0aib.jpg","dog-gec88cbf11_640_ww0aib"),
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/dog-gdf5cd6fc2_640_jgcci2.jpg","dog-gdf5cd6fc2_640_jgcci2"),
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/labrador-retriever-g30db09fa2_640_kvcgx0.jpg","labrador-retriever-g30db09fa2_640_kvcgx0"),
    };

        var smallAnimalsCardCollection = new CardCollection("Small Animals", smallAnimalCards);
        var bigAnimalsCardCollection = new CardCollection("Big Animals", bigAnimalCards);
        context.AddRange(smallAnimalsCardCollection, bigAnimalsCardCollection);
        context.SaveChanges();
    }
}