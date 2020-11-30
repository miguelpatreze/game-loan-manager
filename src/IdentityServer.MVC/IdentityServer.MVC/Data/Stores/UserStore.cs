using IdentityServer.MVC.Models;
using IdentityServer.MVC.Settings;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServer.MVC.Data.Stores
{
    public class UserStore :
        IUserStore<ApplicationUser>
        , IUserEmailStore<ApplicationUser>
        , IUserPasswordStore<ApplicationUser>
        , IQueryableUserStore<ApplicationUser>
        , IUserLockoutStore<ApplicationUser>
        , IUserRoleStore<ApplicationUser>
    {
        private readonly IMongoCollection<ApplicationUser> _collection;

        public UserStore(IMongoClient client, MongoSettings mongoSettings)
        {
            _collection = client.GetDatabase(mongoSettings.Database).GetCollection<ApplicationUser>(nameof(ApplicationUser));
        }

        public void Dispose() => GC.SuppressFinalize(this);

        #region IUserStore

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(user, cancellationToken: cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(filter => filter.Id == user.Id, cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(user => user.Id, ObjectId.Parse(userId));
            return await _collection.Find(filter).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(user => user.NormalizedUserName, normalizedUserName);
            return await _collection.Find(filter).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken) => await Task.FromResult(user.NormalizedUserName);

        public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken) => await Task.FromResult(user.Id.ToString());

        public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken) => await Task.FromResult(user.UserName);

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        #endregion

        #region IUserEmailStore

        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var filter = Builders<ApplicationUser>.Filter.Where(user => user.NormalizedEmail.Equals(normalizedEmail));
            return await _collection.Find(filter).SingleOrDefaultAsync(cancellationToken);
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        #endregion

        #region IUserPasswordStore

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public async Task<int> GetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return (await _collection.FindAsync<ApplicationUser>(x => x.Id == user.Id)).FirstOrDefault()?.AccessFailedCount ?? 0;
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return (await _collection.FindAsync<ApplicationUser>(x => x.Id == user.Id)).FirstOrDefault()?.LockoutEnd;
        }

        public async Task<int> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(x => x.Id, user.Id);
            user.AccessFailedCount++;

            var update = Builders<ApplicationUser>.Update.Inc(x => x.AccessFailedCount, 1);

            await _collection.UpdateOneAsync(filter, update, null, cancellationToken);
            return user.AccessFailedCount;
        }

        public async Task ResetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(x => x.Id, user.Id);

            var update = Builders<ApplicationUser>.Update.Set(x => x.AccessFailedCount, 0);

            await _collection.UpdateOneAsync(filter, update, null, cancellationToken);
        }

        public async Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(x => x.Id, user.Id);

            var update = Builders<ApplicationUser>.Update.Set(x => x.LockoutEnabled, enabled);

            await _collection.UpdateOneAsync(filter, update, null, cancellationToken);
        }

        public async Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(x => x.Id, user.Id);

            var update = Builders<ApplicationUser>.Update.Set(x => x.LockoutEnd, lockoutEnd);

            await _collection.UpdateOneAsync(filter, update, null, cancellationToken);
        }

        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(x => x.Id, user.Id);

            var update = Builders<ApplicationUser>.Update.Set(x => x.Role, new List<string> { roleName });

            await _collection.UpdateOneAsync(filter, update, null, cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return user.Role;
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            return user?.Role?.Any(role => role == roleName) == true;
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IQueryableUserStore

        public IQueryable<ApplicationUser> Users
        {
            get
            {
                var task = _collection.FindAsync(Builders<ApplicationUser>.Filter.Empty);
                Task.WaitAny(task);
                return task.Result.ToEnumerable().AsQueryable();
            }
        }

        #endregion
    }
}
