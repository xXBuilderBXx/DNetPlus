﻿using Newtonsoft.Json;

namespace Discord.API.Gateway
{
    internal class InteractionCreateJson
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("type")]
        public InteractionType Type { get; set; }
        [JsonProperty("member")]
        public Optional<GuildMemberJson> Member { get; set; }
        [JsonProperty("user")]
        public Optional<UserJson> User { get; set; }

        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("guild_id")]
        public Optional<ulong> GuildId { get; set; }
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }
        [JsonProperty("data")]
        public InteractionDataJson Data { get; set; }
    }
    internal class InteractionDataJson
    {
        [JsonProperty("options")]
        public InteractionOptionJson[] Options { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public ulong Id { get; set; }
    }
    internal class InteractionOptionJson
    {
        [JsonProperty("options")]
        public InteractionOptionJson[] Options { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}