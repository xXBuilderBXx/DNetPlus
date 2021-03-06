using System;
using System.Diagnostics;
using Model = Discord.API.ChannelJson;

namespace Discord.Rest
{
    /// <summary>
    ///     Represents a REST-based news channel in a guild that has the same properties as a <see cref="RestTextChannel"/>.
    /// </summary>
    [DebuggerDisplay(@"{DebuggerDisplay,nq}")]
    public class RestNewsChannel : RestTextChannel, INewsChannel
    {
        internal RestNewsChannel(BaseDiscordClient discord, IGuild guild, ulong id)
            :base(discord, guild, id)
        {
        }
        internal new static RestNewsChannel Create(BaseDiscordClient discord, IGuild guild, Model model)
        {
            RestNewsChannel entity = new RestNewsChannel(discord, guild, model.Id);
            entity.Update(model);
            return entity;
        }
        public override int SlowModeInterval => throw new NotSupportedException("News channels do not support Slow Mode.");
    }
}
