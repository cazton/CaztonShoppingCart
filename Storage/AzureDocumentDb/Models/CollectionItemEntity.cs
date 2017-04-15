using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AzureDocumentDb.Models
{
    public class CollectionItemEntity
    {
        public CollectionItemEntity Init(Guid id)
        {
            Id = id;
            return this;
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
    }
}
