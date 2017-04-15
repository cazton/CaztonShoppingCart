using System.Linq;
using AzureDocumentDb.Models;
using Core.Interfaces;

namespace AzureDocumentDb
{
    public class ProductCollection : CollectionBase<ProductEntity>, IProductCollection
    {
        public ProductCollection(IAzureDocDatabase database, string collectionName) : base(database, collectionName)
        {
        }

        public IProductCollection OrderByName()
        {
            Query = Query.OrderBy(x => x.Name);
            return this;
        }
    }
}
