﻿using System;
using System.Threading.Tasks;
using NicmanGroup.AuditLogging.EntityFramework.Entities;
using Undersoft.IDP.Admin.EntityFramework.Extensions.Common;

namespace Undersoft.IDP.Admin.EntityFramework.Repositories.Interfaces
{
    public interface IAuditLogRepository<TAuditLog> where TAuditLog : AuditLog
    {
        Task<PagedList<TAuditLog>> GetAsync(string @event, string source, string category, DateTime? created, string subjectIdentifier, string subjectName, int page = 1, int pageSize = 10);

        Task DeleteLogsOlderThanAsync(DateTime deleteOlderThan);

        bool AutoSaveChanges { get; set; }
    }
}