﻿using System.Linq;

using Model = Discord.API.AuditLogJson;
using EntryModel = Discord.API.AuditLogEntryJson;

namespace Discord.Rest
{
    /// <summary>
    ///     Contains a piece of audit log data related to a kick.
    /// </summary>
    public class KickAuditLogData : IAuditLogData
    {
        private KickAuditLogData(RestUser user)
        {
            Target = user;
        }

        internal static KickAuditLogData Create(BaseDiscordClient discord, Model log, EntryModel entry)
        {
            API.UserJson userInfo = log.Users.FirstOrDefault(x => x.Id == entry.TargetId);
            return new KickAuditLogData(RestUser.Create(discord, userInfo));
        }

        /// <summary>
        ///     Gets the user that was kicked.
        /// </summary>
        /// <returns>
        ///     A user object representing the kicked user.
        /// </returns>
        public IUser Target { get; }
    }
}
