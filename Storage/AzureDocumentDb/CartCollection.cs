
using AzureDocumentDb.Models;
using Core.Interfaces;

namespace AzureDocumentDb
{
    public class CartCollection : CollectionBase<CartEntity>, ICartCollection
    {
        public CartCollection(IAzureDocDatabase database, string collectionName) : base(database, collectionName)
        {
        }
    }
}
