﻿using NicmanGroup.AuditLogging.Events;
using Undersoft.IDP.Admin.BusinessLogic.Identity.Dtos.Identity;

namespace Undersoft.IDP.Admin.BusinessLogic.Identity.Events.Identity
{
    public class ClaimUsersRequestedEvent<TUsersDto> : AuditEvent
    {
        public TUsersDto Users { get; set; }

        public ClaimUsersRequestedEvent(TUsersDto users)
        {
            Users = users;
        }
    }
}