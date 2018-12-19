using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextBot;
using TextBot.Discord;


namespace TextBot.Discord.Modules
{
    [Name("Texting")]
    public class TextingModule : ModuleBase<SocketCommandContext>
    {
        private CommandService _service;
        private static Regex _add = new Regex(@"\s*?(?<name>\S.*?)\s*?\|\s*?(?<contact>\d{10}@.+)");
        private static Regex _number = new Regex(@"\d{10}@.+");

        public TextingModule(CommandService service)
        {
            _service = service;
        }

        [Command("add")]
        [Summary("Adds a person to your contact list. Usage: [contact name]|[contact_number_as_email]")]
        public async Task AddCmd(string contact)
        {
            await AddCmd(contact);
        }

        [Command("add")]
        [Summary("Adds a person to your contact list. Usage: [contact name]|[contact_number_as_email]")]
        public async Task AddCmd(string contact, [Remainder] string remainder)
        {
            contact += " " + remainder;
            await AddCmd(contact);
        }
    }
}


        