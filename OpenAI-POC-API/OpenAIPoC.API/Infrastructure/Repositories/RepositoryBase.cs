using MongoDB.Driver;
using OpenAIPoC.API.Domain.Common;

namespace OpenAIPoC.API.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T>? _collection;

        protected RepositoryBase(IMongoDatabase mongoDatabase, string collectionName)
        {
            if (!string.IsNullOrEmpty(collectionName))
            {
                _collection = mongoDatabase.GetCollection<T>(collectionName);
            }
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var results = await _collection.Find(FilterDefinition<T>.Empty).ToListAsync();
            return results;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(entity => entity.Id, id);
            var result = await _collection.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }
    }
}
