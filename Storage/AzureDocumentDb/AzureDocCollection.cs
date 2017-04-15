using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AzureDocumentDb.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace AzureDocumentDb
{
    public class AzureDocCollection : IAzureDocCollection, IStartable
    {
        private readonly IAzureDocDatabase _database;
        private readonly string _collectionName;
        protected DocumentCollection Collection;

        public AzureDocCollection(IAzureDocDatabase database, string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
        }

        #region IInitializable Method

        public async void Start()
        {
            await ExecuteWithRetries();
        }

        #endregion


        public async Task<bool> CreateAsync(object objectToSave)
        {
            try
            {
                await _database.Client.CreateDocumentAsync(Collection.SelfLink, objectToSave);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(object objectToSave, string id)
        {
            try
            {
                await _database.Client.ReplaceDocumentAsync($"{Collection.AltLink}/docs/{id}", objectToSave);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                await _database.Client.DeleteDocumentAsync($"{Collection.AltLink}/docs/{id}");
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<StoredProcedureResponse<T2>> ExecuteStoredProcByName<T2>(string spName, dynamic parameter)
        {
            var storedProcedure = _database.Client.CreateStoredProcedureQuery(Collection.SelfLink).Where(c => c.Id == spName).AsEnumerable().FirstOrDefault();
            return await _database.Client.ExecuteStoredProcedureAsync<T2>(storedProcedure.SelfLink, parameter);
        }

        public async Task<StoredProcedureResponse<T2>> ExecuteStoredProcByName<T2>(string spName, params dynamic[] parameters)
        {
            var storedProcedure = _database.Client.CreateStoredProcedureQuery(Collection.SelfLink).Where(c => c.Id == spName).AsEnumerable().FirstOrDefault();
            return await _database.Client.ExecuteStoredProcedureAsync<T2>(storedProcedure.SelfLink, parameters);
        }

        public async Task<StoredProcedureResponse<T2>> ExecuteStoredProcByName<T2>(string spName)
        {
            var storedProcedure = _database.Client.CreateStoredProcedureQuery(Collection.SelfLink).Where(c => c.Id == spName).AsEnumerable().FirstOrDefault();
            return await _database.Client.ExecuteStoredProcedureAsync<T2>(storedProcedure.SelfLink);
        }


        #region protected Methods

        protected async Task ExecuteWithRetries()
        {
            var retryAttempts = 0;

            while (true)
            {
                try
                {
                    DocumentCollection collection = _database.Client.CreateDocumentCollectionQuery(_database.SelfLink).Where(coll => coll.Id == _collectionName).AsEnumerable().FirstOrDefault();

                    //// If the collection does not exist, create a new collection
                    if (collection == null)
                        collection = await _database.Client.CreateDocumentCollectionIfNotExistsAsync(_database.SelfLink,
                            new DocumentCollection
                            {
                                Id = _collectionName,
                                IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 })
                            },
                            new RequestOptions { OfferThroughput = 400 });

                    Collection = collection;
                    break;
                }
                catch
                {
                    if (retryAttempts > 3) throw;
                }

                retryAttempts = retryAttempts + 1;

                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        #endregion
    }

    public class AzureDocCollection<T> : AzureDocCollection, IAzureDocCollection<T> where T : CollectionItemEntity
    {
        private readonly IAzureDocDatabase _database;

        public IQueryable<T> Query { get; set; }

        public AzureDocCollection(IAzureDocDatabase database, string collectionName) : base(database, collectionName)
        {
            _database = database;
        }

        #region IAzureDocCollection<T> Methods

        public T2 SetupBaseQuery<T2>() where T2 : class
        {
            return NewQuery() as T2;
        }

        public IList<T> RunQuery()
        {
            return Query.ToList();
        }

        public IAzureDocCollection<T> GetById(Guid id)
        {
            Query = Query.Where(x => x.Id == id);
            return this;
        }        

        #endregion

        #region Private Methods

        private IAzureDocCollection<T> NewQuery()
        {
            // Set some common query options
            var queryOptions = new FeedOptions { MaxItemCount = -1 };

            this.Query = _database.Client.CreateDocumentQuery<T>(Collection.SelfLink, queryOptions);

            return this;
        }

        #endregion
    }
}
