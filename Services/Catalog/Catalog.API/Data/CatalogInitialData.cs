using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if(await session.Query<Product>().AnyAsync()) 
            return;
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();

    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return new List<Product>()
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "IPhone X",
                Description = "Desctiopn of IPhone X",
                ImageFile = "product-1.png",
                Price = 500,
                Category = new List<string>{"Smart Phone"}
            }
            ,new Product
            {
                Id = Guid.NewGuid(),
                Name = "S25 Ultra",
                Description = "Desctiopn of S25 Ultra",
                ImageFile = "product-2.png",
                Price = 400,
                Category = new List<string>{"Smart Phone"}
            }
            ,new Product
            {
                Id = Guid.NewGuid(),
                Name = "LG 27GP850",
                Description = "Desctiopn of LG 27GP850",
                ImageFile = "product-2.png",
                Price = 300,
                Category = new List<string>{"Monitor"}
            }
        };
    }
}
