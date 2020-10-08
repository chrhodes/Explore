﻿using System.Collections.Generic;
using System.Threading.Tasks;

using FriendOrganizer.Domain;

namespace VNCExplore_FriendOrganizer.Core.DomainServices
{
    public interface IMeetingLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetMeetingLookupAsync();
    }
}