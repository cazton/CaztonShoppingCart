using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AzureDocumentDb.Models
{
    public class ProductEntity : CollectionItemEntity, IDocumentEntity<ProductEntity>
    {
        #region IDocumentEntity Methods

        public ProductEntity Init()
        {
            return Init(Guid.NewGuid()) as ProductEntity;
        }

        #endregion

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
    }
}
