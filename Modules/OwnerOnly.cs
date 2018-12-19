using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordTutorialBot.Core.UserAccounts;
using System.Threading.Tasks;

namespace Owneronly
{
    [Name("Owner")]
    [RequireContext(ContextType.Guild)]


    public class ModeratorModule : ModuleBase<SocketCommandContext>
    {
        [Command("Warn")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task WarnUser(IGuildUser user)
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.NumberOfWarnings++;
            UserAccounts.SaveAccounts();

            if (userAccount.NumberOfWarnings >= 3)
            {
                await user.Guild.AddBanAsync(user, 5);
            }
            else if (userAccount.NumberOfWarnings == 2)
            {
                await user.KickAsync();
            }
            else if (userAccount.NumberOfWarnings == 1)
            {
                await ReplyAsync($"1st warning issued. 2nd warning = kick");
            }
        }
             [Command("VoteKick")]
             [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task C(IGuildUser user)
        {
            var userAccount = UserAccounts.GetAccount((SocketUser)user);
            userAccount.NumberOfWarnings++;
            UserAccounts.SaveAccounts();

            if (userAccount.NumberOfWarnings >= 3)
            {

            }
            else if (userAccount.NumberOfWarnings == 2)
            {

            }
            else if (userAccount.NumberOfWarnings == 1)
            {

            }

        }           
    }

    }


