﻿namespace FriendOrganizer.Domain
{
    public class NullLookupItem : LookupItem
    {
        public new int? Id { get { return null; } }
    }
}
