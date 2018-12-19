using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace bot.Modules
{
    public class UserGuildUpdateService
    {
        internal Task SetWelcomeMessage(SocketCommandContext context, string message)
        {
            throw new NotImplementedException();
        }

        internal Task SetWelcomeChannel(SocketCommandContext context, IMessageChannel channel)
        {
            throw new NotImplementedException();
        }

        internal Task SetLeaveMessage(SocketCommandContext context, string message)
        {
            throw new NotImplementedException();
        }

        internal Task SetLeaveChannel(SocketCommandContext context, IMessageChannel channel)
        {
            throw new NotImplementedException();
        }

        internal Task RemoveLeave(SocketCommandContext context)
        {
            throw new NotImplementedException();
        }
    }
}