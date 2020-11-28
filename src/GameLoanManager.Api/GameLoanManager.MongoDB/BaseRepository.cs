﻿using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.MongoDB
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        public readonly IMongoCollection<T> _collection;
        public BaseRepository(IMongoClient client, MongoDbSettings settings)
        {
            _collection = client
                .GetDatabase(settings.Database)
                .GetCollection<T>(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> FindAsync(CancellationToken cancellationToken = default)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Enabled, true);

            return await _collection.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, id)
                & Builders<T>.Filter.Eq(e => e.Enabled, true);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task InsertOneAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(entity);
        }
    }
}