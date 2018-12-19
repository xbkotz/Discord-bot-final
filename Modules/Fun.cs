using Discord;
using Discord.Commands;
using System.Linq;
using System;
using DiscordTutorialBot;
using System.Threading.Tasks;
using Discord.Rest;
using System.Net;
using Newtonsoft.Json;
using NReco;
using Discord.WebSocket;
using NReco.ImageGenerator;
using System.IO;

namespace ConsoleApp1.commands
{
    public class Fun : ModuleBase<SocketCommandContext>
    {
        private IServiceProvider _services;

            

        [Command("HTMLI")]
        public async Task GeneratedImage(string color = "red", SocketUser user = null)
        {
            if (user == null) user = Context.User as SocketGuildUser;
            string css = "<style>\n    h1{\n        color: " + color + ";\n    }\n</style>\n";
            string html = String.Format("<h1>Who's Thicc? {0}!<h1>", user.Username);
            var converter = new HtmlToImageConverter
            {
                Width = 340,
                Height = 110
            };
            var jpgBytes = converter.GenerateImage(css + html, NReco.ImageGenerator.ImageFormat.Jpeg);
            await Context.Channel.SendFileAsync(new MemoryStream(jpgBytes), "hello.jpg");

        }

        [Command("person")]
        public async Task GetRandomPerson()
        {
            string json = "";
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString("https://randomuser.me/api/?gender=female&nat=US");
            }
            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);
            string firstName = dataObject.results[0].name.first.ToString();
            string lastName = dataObject.results[0].name.last.ToString();
            string avatarURL = dataObject.results[0].picture.large.ToString();
            var embed = new EmbedBuilder();
            embed.WithThumbnailUrl(avatarURL);
            embed.WithTitle("Generated Person");
            embed.AddInlineField("First Name", firstName);
            embed.AddInlineField("Last Name", lastName);
            embed.WithColor(Color.Teal);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }

        [Command("howscam")]
        public async Task dice(SocketUser user = null)
        {
            var embed = new EmbedBuilder();
            Random Rand = new Random();
            int I = Rand.Next(0, 100);
            embed.WithTitle("Scam Percentage");
            embed.AddField($"{user.Username}",$"is {I}% scam");
            embed.WithColor(Color.Teal);
            await Context.Channel.SendMessageAsync("", embed: embed);
        }
                  
        [Command("Role")]
        public async Task Role2(IGuildUser user)
        {
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == "");
            await (user as IGuildUser).AddRoleAsync(role);
        }

        //sends dm  await user.SendMessageAsync(""); 
        [Command("SendCute")]
        public async Task ping(IGuildUser user,SocketUser User = null)
        {
            string[] cute = {"you're a cutie", "Your presence makes the world more beautiful.","I like to see you smiling, and only smiling.", "You have the smile I can die for.", "I like to see you smiling, and only smiling.", "I never like sorrow on your face." };
            Random r = new Random();
            int index = r.Next(cute.Length - 1);
            await (user.SendMessageAsync(cute[index]));
        }

        [Command("katz")]
        public async Task p1ing(SocketUser User = null)
        {
            string[] katz = { "https://imgur.com/r/cats/BNzCgCu", "https://imgur.com/r/cats/czlKnRC", "https://imgur.com/r/cats/mDNKXPd", "https://imgur.com/r/cats/DcBU1b9", "https://imgur.com/r/cats/3RzwL1A" };
            Random r = new Random();
            int index = r.Next(katz.Length - 1);
            await (ReplyAsync(katz[index]));
        }

        [Command("pic")]
        public async Task pic([Remainder]SocketUser user = null)
        {
            Random r = new Random();
            var builder = new EmbedBuilder();
            builder.WithTitle($"Choice for {Context.User.Username}");
            builder.WithImageUrl($"{Context.User.GetAvatarUrl()}");         
            builder.WithColor(Color.Teal);
            await Context.Channel.SendMessageAsync("", false, builder);
        }
      
        [Command("boom")]
        public async Task boom([Remainder]IGuildUser user = null)
        {       
           var l = await Context.Channel.SendMessageAsync(":bomb:----:fire:");
            const int delay = 4090;
            await Task.Delay(delay);       
            await l.ModifyAsync(m => { m.Content = ":bomb:---:fire:"; });
            await Task.Delay(delay);
            await l.ModifyAsync(m => { m.Content = ":bomb:--:fire:"; });
            await Task.Delay(delay);
            await l.ModifyAsync(m => { m.Content = ":bomb:-:fire:"; });
            await Task.Delay(delay);
            await l.ModifyAsync(m => { m.Content = ":bomb::fire:"; });
            await Task.Delay(delay);
            await l.ModifyAsync(m => { m.Content = ":boom:"; });
            await Task.Delay(delay);
            await l.DeleteAsync();
        }

        [Command("Mute")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Mute(IGuildUser user)
        {
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == "Muted");
            await ReplyAsync($"{user.Mention} was muted");
            await (user as IGuildUser).AddRoleAsync(role);
        }

         
        [Command("unmute")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task unmute(IGuildUser user)
        {
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == "Homies");
            await ReplyAsync($"{user.Mention} was unmuted");
            await (user as IGuildUser).AddRoleAsync(role);
        }

        [Command("Owner")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Rbotdev(IGuildUser user)
        {
            try
            {
                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == "Owner");
                await ReplyAsync($"{user.Mention} was given role **Owner**");
                await (user as IGuildUser).AddRoleAsync(role);
            }
            catch
            {
                await ReplyAsync("Role **Owner** not found");
            }
        }

        [Command("Remove")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Remover(IGuildUser user)
        {
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == "");
            await ReplyAsync($"{user.Mention} had their roles removed.");
            await (user as IGuildUser).RemoveRoleAsync(role);
        }

        
    }
    }
