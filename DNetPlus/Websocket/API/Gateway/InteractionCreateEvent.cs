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
        [JsonProperty("message")]
        public Optional<MessageJson> Message { get; set; }


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
        [JsonProperty("custom_id")]
        public Optional<string> CustomId { get; set; }
        [JsonProperty("component_type")]
        public ComponentType ComponentType { get; set; }
        [JsonProperty("options")]
        public InteractionOptionJson[] Options { get; set; }
        [JsonProperty("name")]
        public Optional<string> Name { get; set; }
        [JsonProperty("id")]
        public Optional<ulong> Id { get; set; }
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
