using IdentityServer.MVC.Settings;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServer.MVC.Data.Stores
{

    public class RoleStore : IRoleStore<IdentityRole<ObjectId>>, IQueryableRoleStore<IdentityRole<ObjectId>>
    {
        private readonly IMongoCollection<IdentityRole<ObjectId>> _collection;

        public IQueryable<IdentityRole<ObjectId>> Roles => _collection.AsQueryable();

        public RoleStore(IMongoClient client, MongoSettings mongoSettings)
        {
            _collection = client.GetDatabase(mongoSettings.Database).GetCollection<IdentityRole<ObjectId>>(nameof(IdentityRole));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IdentityResult> CreateAsync(IdentityRole<ObjectId> role, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(role, cancellationToken: cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(IdentityRole<ObjectId> role, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(filter => filter.Id == role.Id, cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityRole<ObjectId>> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await _collection.Find(filter => filter.Id.ToString() == roleId).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IdentityRole<ObjectId>> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await _collection.Find(filter => filter.NormalizedName == normalizedRoleName).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<string> GetNormalizedRoleNameAsync(IdentityRole<ObjectId> role, CancellationToken cancellationToken)
        {
            return await Task.FromResult(role.NormalizedName);
        }

        public async Task<string> GetRoleIdAsync(IdentityRole<ObjectId> role, CancellationToken cancellationToken)
        {
            return (await Task.FromResult(role.Id)).ToString();
        }

        public async Task<string> GetRoleNameAsync(IdentityRole<ObjectId> role, CancellationToken cancellationToken)
        {
            return await Task.FromResult(role.Name);
        }

        public async Task SetNormalizedRoleNameAsync(IdentityRole<ObjectId> role, string normalizedName, CancellationToken cancellationToken)
        {
            var builder = Builders<IdentityRole<ObjectId>>.Update.Set(x => x.NormalizedName, normalizedName);
            await _collection.UpdateOneAsync(filter => filter.Id == role.Id, builder, cancellationToken: cancellationToken);
        }

        public async Task SetRoleNameAsync(IdentityRole<ObjectId> role, string roleName, CancellationToken cancellationToken)
        {
            var builder = Builders<IdentityRole<ObjectId>>.Update.Set(x => x.Name, roleName);
            await _collection.UpdateOneAsync(filter => filter.Id == role.Id, builder, cancellationToken: cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(IdentityRole<ObjectId> role, CancellationToken cancellationToken)
        {
            await _collection.ReplaceOneAsync(filter => filter.Id == role.Id, role, cancellationToken: cancellationToken);
            return IdentityResult.Success;
        }
    }
}
