﻿using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Undersoft.IDP.Admin.EntityFramework.Entities;
using Undersoft.IDP.Admin.EntityFramework.Extensions.Common;

namespace Undersoft.IDP.Admin.EntityFramework.Repositories.Interfaces
{
	public interface IPersistedGrantRepository
    {
		Task<PagedList<PersistedGrantDataView>> GetPersistedGrantsByUsersAsync(string search, int page = 1, int pageSize = 10);
		Task<PagedList<PersistedGrant>> GetPersistedGrantsByUserAsync(string subjectId, int page = 1, int pageSize = 10);
	    Task<PersistedGrant> GetPersistedGrantAsync(string key);
	    Task<int> DeletePersistedGrantAsync(string key);
	    Task<int> DeletePersistedGrantsAsync(string userId);
        Task<bool> ExistsPersistedGrantsAsync(string subjectId);
	    Task<int> SaveAllChangesAsync();
        bool AutoSaveChanges { get; set; }
    }
}