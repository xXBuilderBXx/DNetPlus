﻿#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API.Gateway
{
    internal class GuildMemberUpdateEvent : GuildMemberJson
    {
        [JsonProperty("guild_id")]
        public ulong GuildId { get; set; }
    }
}
