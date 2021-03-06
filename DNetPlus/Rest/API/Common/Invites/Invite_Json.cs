#pragma warning disable CS1591
using Newtonsoft.Json;

namespace Discord.API
{
    internal class InviteJson
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("guild")]
        public Optional<InviteGuildJson> Guild { get; set; }
        [JsonProperty("inviter")]
        public Optional<UserJson> Inviter { get; set; }
        [JsonProperty("channel")]
        public InviteChannelJson Channel { get; set; }
        [JsonProperty("approximate_presence_count")]
        public Optional<int?> PresenceCount { get; set; }
        [JsonProperty("approximate_member_count")]
        public Optional<int?> MemberCount { get; set; }
        [JsonProperty("target_user")]
        public Optional<UserJson> TargetUser { get; set; }
        [JsonProperty("target_user_type")]
        public Optional<TargetUserType> TargetUserType { get; set; }
    }
}
