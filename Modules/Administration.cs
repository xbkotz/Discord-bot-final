using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using DiscordTutorialBot.Core.UserAccounts;
using bot;
using System.Reflection;
using Discord.Commands;
using System.Drawing;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Linq;
using bot.Modules;
using System.Diagnostics;

namespace bot.Modules

{
    [Name("Admin")]
    [RequireContext(ContextType.Guild)]
    public class ModeratorModule : ModuleBase<SocketCommandContext>
    {
        private object BaseCommands;
        private string prefix;

        public Task SentryService { get; private set; }

        [Command("kick")]
        [Summary("Kick the specified user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task Kick(IGuildUser user, string reason = "")
        {
            await user.KickAsync(reason);
            var m = await ReplyAsync($"{user.Mention} Was kicked");
            const int delay = 3700;
            await Task.Delay(delay);
            await m.DeleteAsync();          
        }

        [Command("Ban")]
        [Summary("Ban the specified user.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Ban(IGuildUser user, string reason = "")
        {
            var m = await ReplyAsync($"{user.Mention} Was banned");
            const int delay = 3700;
            await Task.Delay(delay);
            await m.DeleteAsync();
            await user.Guild.AddBanAsync(user, 0, reason);
        }


        [Command("purge", RunMode = RunMode.Async)]
        [Summary("Deletes the specified amount of messages.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task PurgeChat(uint amount)
        {           
            var messages = await Context.Channel.GetMessagesAsync((int)amount + 1).Flatten();
            await Context.Channel.DeleteMessagesAsync(messages);          
            var m = await ReplyAsync("Message(s) deleted.");           
            const int delay = 3700;
            await Task.Delay(delay);
            await m.DeleteAsync();          
        }

        [Command("Fun")]
        [Summary("Send all commands")]
        public async Task Help(SocketUser user = null)
        {
            DateTime today = DateTime.Today;
            DateTime now = DateTime.Now;
            if (user == null) user = Context.User as SocketGuildUser;
            var builder = new EmbedBuilder();
            builder.WithTitle("Fun Commands");
            builder.WithDescription("Prefix for bot is ``??``");
            builder.AddInlineField("Howscam", "Shows users scam %");
            builder.AddInlineField("Join", "Joins voice channel");
            builder.AddInlineField("SendCute", "Sends cute things to tagged user");
            builder.AddInlineField("katz", "Sends cute cat pics");
            builder.AddInlineField("Nick", "Change users nickname");
            builder.AddInlineField("Boom", "Make things go boom! \n credits to hulky");
            builder.WithFooter($"Requested by {user.Username} at {now}");
            builder.WithColor(Color.Blue);
            await Context.Channel.SendMessageAsync("", false, builder);
        }

      
        [Command("help")]
        [Summary("Send all commands")]
        public async Task Halp(SocketUser user = null)
        {
            DateTime now = DateTime.Now;
            if (user == null) user = Context.User as SocketGuildUser;
            var builder = new EmbedBuilder();
            builder.WithTitle("Help Commands");
            builder.WithDescription("Prefix for bot is ??");
            builder.AddInlineField("Mute", "mutes user tagged");
            builder.AddInlineField("Kick", "kicks the user tagged");
            builder.AddInlineField("Ban", "Bans the user tagged");
            builder.AddInlineField("Purge", "Mass delete messages");
            builder.AddInlineField("Owner", "Gives role named 'Owner'");
            builder.AddInlineField("Admin", "Gives role named 'Admin'");
            builder.AddInlineField("Role", "Gives user entered role");
            builder.AddInlineField("loot", "Gives big loot role");
            builder.AddInlineField("Remove", "Strips them of their roles");
            builder.AddInlineField("Warn", "Warns user tagged");
            builder.AddInlineField("Fun", "Shows list of fun commands");
            builder.WithFooter($"Requested by {user.Username}  {DateTime.Today}");
            builder.WithColor(Color.Blue);
            await Context.Channel.SendMessageAsync("", false, builder);
        }


        [Command("nick")]
        public async Task nickv2(IGuildUser user, [Remainder]string name)
        {
            await user.ModifyAsync(x => x.Nickname = name);
            var m = await ReplyAsync($"{user.Username} I changed your name to **{user.Mention}**");
            const int delay = 3700;
            await Task.Delay(delay);
            await m.DeleteAsync();
        }
              
        [Command("loot")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task loot(IGuildUser user)
        {
            var m = await ReplyAsync($"Role named **big loot** was given to {user.Mention}");
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == "big loot");
            await (user as IGuildUser).AddRoleAsync(role);
            const int delay = 3700;
            await Task.Delay(delay);
            await m.DeleteAsync();
        }
     
        [Command("ServerInfo")]
        public async Task serverinfo(IGuildUser user = null)
        {
            var builder = new EmbedBuilder();
            builder.WithTitle($"Server information for {Context.Guild.Name}");
            builder.AddInlineField("Server Name", $"{Context.Guild.Name}");
            builder.WithThumbnailUrl($"{Context.Guild.IconUrl}");
            builder.AddInlineField("Owner", $"{Context.Guild.Owner}");
            builder.AddInlineField("Region", $"{Context.Guild.VoiceRegionId}");
            builder.AddInlineField("Verification Level", $"{Context.Guild.VerificationLevel}");
            builder.AddInlineField("Member Count", $"{Context.Guild.MemberCount}");
            builder.AddInlineField("Highest Role", $"{Context.Guild.Roles.OrderByDescending(role => role.Position).FirstOrDefault()}");
            builder.AddInlineField("Number of TextChannels", $"{Context.Guild.TextChannels.Count()}");
            builder.AddInlineField("Number of VoiceChannels", $"{Context.Guild.VoiceChannels.Count()}");
            builder.AddInlineField("Number of Roles", $"{Context.Guild.Roles.Count()}");
            builder.AddInlineField("Server ID:", $"{Context.Guild.Id}");
            builder.WithFooter($"Created At: {Context.Guild.CreatedAt:HH:mm:ss pm, dddd, dd MMMM yyyy}");
            builder.WithColor(Color.Teal);           
            await Context.Channel.SendMessageAsync("", embed: builder);
        }

        [Command("admin")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Admin(IGuildUser user)
        {
            var m = await ReplyAsync($"Role named **Admin** was given to {user.Mention}");
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == "Admin");
            await (user as IGuildUser).AddRoleAsync(role);
            const int delay = 3700;
            await Task.Delay(delay);
            await m.DeleteAsync();
        }
    }
}

    


    








