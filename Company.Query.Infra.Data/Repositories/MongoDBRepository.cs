using MongoDB.Driver;
using Company.Query.Domain.Abstractions.Attributes;
using Company.Query.Domain.Abstractions.Interfaces;
using Company.Query.Domain.Repositories;
using Company.Query.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Query.Infra.Data.Repositories
{
    public class MongoDBRepository<TDocument> : IMongoDBRepository<TDocument> where TDocument : ICompanyEntity
    {
        private readonly Lazy<IMongoCollection<TDocument>> _collection;
        private readonly IMongoDbSettings _settings;

        public MongoDBRepository(IMongoDbSettings settings)
        {
            _settings = settings;
            _collection = new Lazy<IMongoCollection<TDocument>>(() =>
            {
                var database = new MongoClient(_settings.ConnectionString).GetDatabase(_settings.DatabaseName);
                return database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
            }, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.Value.AsQueryable();
        }

        public virtual IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Value.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Value.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Value.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual Task<TDocument> FindByEndtoendAsync(string endtoendid)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.EndToEndId, endtoendid);
                return _collection.Value.Find(filter).SingleOrDefaultAsync();
            });
        }
    }
}
