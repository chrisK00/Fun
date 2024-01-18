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
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/bird-gd8d8e4a91_640_sctbst.jpg", "bird-gd8d8e4a91_640_sctbst"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/bird-g4e9c2c7b0_640_prkfyy.jpg", "bird-g4e9c2c7b0_640_prkfyy"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/black-and-white-warbler-gb189f6056_640_hrhl6f.jpg", "black-and-white-warbler-gb189f6056_640_hrhl6f"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/squirrel-gd5b52f387_640_oixdk0.jpg", "squirrel-gd5b52f387_640_oixdk0"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1705523278/Cards/animal-468228_640_ztczgo.jpg", "animal-468228_640_ztczgo"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1705523278/Cards/cat-2083492_640_ejrrwi.jpg", "cat-2083492_640_ejrrwi"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1705523278/Cards/bird-2295436_640_bsyadq.jpg", "bird-2295436_640_bsyadq"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1705523278/Cards/dove-2516641_640_spve9m.jpg", "Cards/dove-2516641_640_spve9m"),
    };

        var bigAnimalCards = new List<Card>()
    {
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/dog-gec88cbf11_640_ww0aib.jpg","dog-gec88cbf11_640_ww0aib"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/dog-gdf5cd6fc2_640_jgcci2.jpg","dog-gdf5cd6fc2_640_jgcci2"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/labrador-retriever-g30db09fa2_640_kvcgx0.jpg","labrador-retriever-g30db09fa2_640_kvcgx0"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1705523278/Cards/dolphin-203875_640_ptvdgd.jpg","dolphin-203875_640_ptvdgd"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1705523278/Cards/puppy-2785074_640_bobmkm.jpg","puppy-2785074_640_bobmkm"),
        new("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1705523278/Cards/sea-2361247_640_faks6e.jpg","sea-2361247_640_faks6e"),
    };

        var smallAnimalsCardCollection = new CardCollection("Small Animals", smallAnimalCards);
        var bigAnimalsCardCollection = new CardCollection("Big Animals", bigAnimalCards);
        context.AddRange(smallAnimalsCardCollection, bigAnimalsCardCollection);
        context.SaveChanges();
    }
}