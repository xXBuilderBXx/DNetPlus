using System.Collections.Generic;
using System.Linq;

using Model = Discord.API.AuditLogJson;
using EntryModel = Discord.API.AuditLogEntryJson;

namespace Discord.Rest
{
    /// <summary>
    ///     Contains a piece of audit log data related to a channel deletion.
    /// </summary>
    public class ChannelDeleteAuditLogData : IAuditLogData
    {
        private ChannelDeleteAuditLogData(ulong id, string name, ChannelType type, int? rateLimit, bool? nsfw, int? bitrate, IReadOnlyCollection<Overwrite> overwrites)
        {
            ChannelId = id;
            ChannelName = name;
            ChannelType = type;
            SlowModeInterval = rateLimit;
            IsNsfw = nsfw;
            Bitrate = bitrate;
            Overwrites = overwrites;
        }

        internal static ChannelDeleteAuditLogData Create(BaseDiscordClient discord, Model log, EntryModel entry)
        {
            API.AuditLogChangeJson[] changes = entry.Changes;

            API.AuditLogChangeJson overwritesModel = changes.FirstOrDefault(x => x.ChangedProperty == "permission_overwrites");
            API.AuditLogChangeJson typeModel = changes.FirstOrDefault(x => x.ChangedProperty == "type");
            API.AuditLogChangeJson nameModel = changes.FirstOrDefault(x => x.ChangedProperty == "name");
            API.AuditLogChangeJson rateLimitPerUserModel = changes.FirstOrDefault(x => x.ChangedProperty == "rate_limit_per_user");
            API.AuditLogChangeJson nsfwModel = changes.FirstOrDefault(x => x.ChangedProperty == "nsfw");
            API.AuditLogChangeJson bitrateModel = changes.FirstOrDefault(x => x.ChangedProperty == "bitrate");

            List<Overwrite> overwrites = overwritesModel.OldValue.ToObject<API.OverwriteJson[]>(discord.ApiClient.Serializer)
                .Select(x => new Overwrite(x.TargetId, x.TargetType, new OverwritePermissions(x.Allow, x.Deny)))
                .ToList();
            ChannelType type = typeModel.OldValue.ToObject<ChannelType>(discord.ApiClient.Serializer);
            string name = nameModel.OldValue.ToObject<string>(discord.ApiClient.Serializer);
            int? rateLimitPerUser = rateLimitPerUserModel?.OldValue?.ToObject<int>(discord.ApiClient.Serializer);
            bool? nsfw = nsfwModel?.OldValue?.ToObject<bool>(discord.ApiClient.Serializer);
            int? bitrate = bitrateModel?.OldValue?.ToObject<int>(discord.ApiClient.Serializer);
            ulong id = entry.TargetId.Value;

            return new ChannelDeleteAuditLogData(id, name, type, rateLimitPerUser, nsfw, bitrate, overwrites.ToReadOnlyCollection());
        }

        /// <summary>
        ///     Gets the snowflake ID of the deleted channel.
        /// </summary>
        /// <returns>
        ///     A <see cref="ulong"/> representing the snowflake identifier for the deleted channel.
        /// </returns>
        public ulong ChannelId { get; }
        /// <summary>
        ///     Gets the name of the deleted channel.
        /// </summary>
        /// <returns>
        ///     A string containing the name of the deleted channel.
        /// </returns>
        public string ChannelName { get; }
        /// <summary>
        ///     Gets the type of the deleted channel.
        /// </summary>
        /// <returns>
        ///     The type of channel that was deleted.
        /// </returns>
        public ChannelType ChannelType { get; }
        /// <summary>
        ///     Gets the slow-mode delay of the deleted channel.
        /// </summary>
        /// <returns>
        ///     An <see cref="int"/> representing the time in seconds required before the user can send another
        ///     message; <c>0</c> if disabled.
        ///     <c>null</c> if this is not mentioned in this entry.
        /// </returns>
        public int? SlowModeInterval { get; }
        /// <summary>
        ///     Gets the value that indicates whether the deleted channel was NSFW.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this channel had the NSFW flag enabled; otherwise <c>false</c>.
        ///     <c>null</c> if this is not mentioned in this entry.
        /// </returns>
        public bool? IsNsfw { get; }
        /// <summary>
        ///     Gets the bit-rate of this channel if applicable.
        /// </summary>
        /// <returns>
        ///     An <see cref="int"/> representing the bit-rate set of the voice channel.
        ///     <c>null</c> if this is not mentioned in this entry.
        /// </returns>
        public int? Bitrate { get; }
        /// <summary>
        ///     Gets a collection of permission overwrites that was assigned to the deleted channel.
        /// </summary>
        /// <returns>
        ///     A collection of permission <see cref="Overwrite"/>.
        /// </returns>
        public IReadOnlyCollection<Overwrite> Overwrites { get; }
    }
}
