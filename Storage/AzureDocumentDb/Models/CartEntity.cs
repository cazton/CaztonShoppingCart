using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDocumentDb.Models
{
    public class CartEntity : CollectionItemEntity, IDocumentEntity<CartEntity>
    {
        public CartEntity()
        {
            Products = new List<ProductEntity>();
        }

        #region IDocumentEntity Methods

        public CartEntity Init()
        {
            return Init(Guid.NewGuid()) as CartEntity;
        }

        #endregion

        public IList<ProductEntity> Products { get; set; }
    }
}
