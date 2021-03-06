﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Model = Discord.API.WebhookJson;

namespace Discord.Webhook
{
    [DebuggerDisplay(@"{DebuggerDisplay,nq}")]
    internal class RestInternalWebhook : IWebhook
    {
        private DiscordWebhookClient _client;

        public ulong Id { get; }
        public ulong ChannelId { get; private set; }
        public string Token { get; }

        public string Name { get; private set; }
        public string AvatarId { get; private set; }
        public ulong? GuildId { get; private set; }
        public WebhookFollowGuild? SourceGuild { get; private set; }
        public WebhookFollowChannel? SourceChannel { get; private set; }

        public DateTimeOffset CreatedAt => SnowflakeUtils.FromSnowflake(Id);

        internal RestInternalWebhook(DiscordWebhookClient apiClient, Model model)
        {
            _client = apiClient;
            Id = model.Id;
            ChannelId = model.Id;
            Token = model.Token;
        }
        internal static RestInternalWebhook Create(DiscordWebhookClient client, Model model)
        {
            RestInternalWebhook entity = new RestInternalWebhook(client, model);
            entity.Update(model);
            return entity;
        }

        internal void Update(Model model)
        {
            if (ChannelId != model.ChannelId)
                ChannelId = model.ChannelId;
            if (model.Avatar.IsSpecified)
                AvatarId = model.Avatar.Value;
            if (model.GuildId.IsSpecified)
                GuildId = model.GuildId.Value;
            if (model.Name.IsSpecified)
                Name = model.Name.Value;
            if (model.SourceGuild.IsSpecified)
                SourceGuild = new WebhookFollowGuild
                {
                    Id = model.SourceGuild.Value.Id,
                    Icon = model.SourceGuild.Value.Icon,
                    Name = model.SourceGuild.Value.Name
                };
            if (model.SourceChannel.IsSpecified)
                SourceChannel = new WebhookFollowChannel
                {
                    Id = model.SourceChannel.Value.Id,
                    Name = model.SourceChannel.Value.Name
                };
        }

        public string GetAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
           => CDN.GetUserAvatarUrl(Id, AvatarId, size, format);

        public async Task ModifyAsync(Action<WebhookProperties> func, RequestOptions options = null)
        {
            Model model = await WebhookClientHelper.ModifyAsync(_client, func, options).ConfigureAwait(false);
            Update(model);
        }

        public Task DeleteAsync(RequestOptions options = null)
            => WebhookClientHelper.DeleteAsync(_client, options);

        public override string ToString() => $"Webhook: {Name}:{Id}";
        private string DebuggerDisplay => $"Webhook: {Name} ({Id})";

        IUser IWebhook.Creator => null;
        ITextChannel IWebhook.Channel => null;
        IGuild IWebhook.Guild => null;
        WebhookFollowGuild IWebhook.SourceGuild => SourceGuild;
        WebhookFollowChannel IWebhook.SourceChannel => SourceChannel;
    }
}
