﻿using System;
using System.Threading.Tasks;
using NicmanGroup.AuditLogging.Services;
using Undersoft.IDP.Admin.BusinessLogic.Dtos.Log;
using Undersoft.IDP.Admin.BusinessLogic.Events.Log;
using Undersoft.IDP.Admin.BusinessLogic.Mappers;
using Undersoft.IDP.Admin.BusinessLogic.Services.Interfaces;
using Undersoft.IDP.Admin.EntityFramework.Repositories.Interfaces;

namespace Undersoft.IDP.Admin.BusinessLogic.Services
{
    public class LogService : ILogService
    {
        protected readonly ILogRepository Repository;
        protected readonly IAuditEventLogger AuditEventLogger;

        public LogService(ILogRepository repository, IAuditEventLogger auditEventLogger)
        {
            Repository = repository;
            AuditEventLogger = auditEventLogger;
        }

        public virtual async Task<LogsDto> GetLogsAsync(string search, int page = 1, int pageSize = 10)
        {
            var pagedList = await Repository.GetLogsAsync(search, page, pageSize);
            var logs = pagedList.ToModel();

            await AuditEventLogger.LogEventAsync(new LogsRequestedEvent());

            return logs;
        }

        public virtual async Task DeleteLogsOlderThanAsync(DateTime deleteOlderThan)
        {
            await Repository.DeleteLogsOlderThanAsync(deleteOlderThan);

            await AuditEventLogger.LogEventAsync(new LogsDeletedEvent(deleteOlderThan));
        }
    }
}
