using System.Linq;

using Model = Discord.API.AuditLogJson;
using EntryModel = Discord.API.AuditLogEntryJson;

namespace Discord.Rest
{
    /// <summary>
    ///     Contains a piece of audit log data related to an invite creation.
    /// </summary>
    public class InviteCreateAuditLogData : IAuditLogData
    {
        private InviteCreateAuditLogData(int maxAge, string code, bool temporary, IUser inviter, ulong channelId, int uses, int maxUses)
        {
            MaxAge = maxAge;
            Code = code;
            Temporary = temporary;
            Creator = inviter;
            ChannelId = channelId;
            Uses = uses;
            MaxUses = maxUses;
        }

        internal static InviteCreateAuditLogData Create(BaseDiscordClient discord, Model log, EntryModel entry)
        {
            API.AuditLogChangeJson[] changes = entry.Changes;

            API.AuditLogChangeJson maxAgeModel = changes.FirstOrDefault(x => x.ChangedProperty == "max_age");
            API.AuditLogChangeJson codeModel = changes.FirstOrDefault(x => x.ChangedProperty == "code");
            API.AuditLogChangeJson temporaryModel = changes.FirstOrDefault(x => x.ChangedProperty == "temporary");
            API.AuditLogChangeJson inviterIdModel = changes.FirstOrDefault(x => x.ChangedProperty == "inviter_id");
            API.AuditLogChangeJson channelIdModel = changes.FirstOrDefault(x => x.ChangedProperty == "channel_id");
            API.AuditLogChangeJson usesModel = changes.FirstOrDefault(x => x.ChangedProperty == "uses");
            API.AuditLogChangeJson maxUsesModel = changes.FirstOrDefault(x => x.ChangedProperty == "max_uses");

            int maxAge = maxAgeModel.NewValue.ToObject<int>(discord.ApiClient.Serializer);
            string code = codeModel.NewValue.ToObject<string>(discord.ApiClient.Serializer);
            bool temporary = temporaryModel.NewValue.ToObject<bool>(discord.ApiClient.Serializer);
            ulong channelId = channelIdModel.NewValue.ToObject<ulong>(discord.ApiClient.Serializer);
            int uses = usesModel.NewValue.ToObject<int>(discord.ApiClient.Serializer);
            int maxUses = maxUsesModel.NewValue.ToObject<int>(discord.ApiClient.Serializer);

            RestUser inviter = null;
            if (inviterIdModel != null)
            {
                ulong inviterId = inviterIdModel.NewValue.ToObject<ulong>(discord.ApiClient.Serializer);
                API.UserJson inviterInfo = log.Users.FirstOrDefault(x => x.Id == inviterId);
                inviter = RestUser.Create(discord, inviterInfo);
            }

            return new InviteCreateAuditLogData(maxAge, code, temporary, inviter, channelId, uses, maxUses);
        }

        /// <summary>
        ///     Gets the time (in seconds) until the invite expires.
        /// </summary>
        /// <returns>
        ///     An <see cref="int"/> representing the time in seconds until this invite expires.
        /// </returns>
        public int MaxAge { get; }
        /// <summary>
        ///     Gets the unique identifier for this invite.
        /// </summary>
        /// <returns>
        ///     A string containing the invite code (e.g. <c>FTqNnyS</c>).
        /// </returns>
        public string Code { get; }
        /// <summary>
        ///     Gets a value that determines whether the invite is a temporary one.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if users accepting this invite will be removed from the guild when they log off; otherwise
        ///     <c>false</c>.
        /// </returns>
        public bool Temporary { get; }
        /// <summary>
        ///     Gets the user that created this invite if available.
        /// </summary>
        /// <returns>
        ///     A user that created this invite or <see langword="null"/>.
        /// </returns>
        public IUser Creator { get; }
        /// <summary>
        ///     Gets the ID of the channel this invite is linked to.
        /// </summary>
        /// <returns>
        ///     A <see cref="ulong"/> representing the channel snowflake identifier that the invite points to.
        /// </returns>
        public ulong ChannelId { get; }
        /// <summary>
        ///     Gets the number of times this invite has been used.
        /// </summary>
        /// <returns>
        ///     An <see cref="int"/> representing the number of times this invite was used.
        /// </returns>
        public int Uses { get; }
        /// <summary>
        ///     Gets the max number of uses this invite may have.
        /// </summary>
        /// <returns>
        ///     An <see cref="int"/> representing the number of uses this invite may be accepted until it is removed
        ///     from the guild; <c>null</c> if none is set.
        /// </returns>
        public int MaxUses { get; }
    }
}
