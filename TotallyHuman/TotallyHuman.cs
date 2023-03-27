using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotallyHuman.Attributes;
using static TotallyHuman.Utils.Utils;

namespace TotallyHuman
{
    public class TotallyHuman : Bot
    {
        public TotallyHuman(string token) : base(token) { }


        [Command("d20")]
        public async Task CommandD20(SocketMessage message)
        {
            await message.Channel.SendMessageAsync(RandomRange(1, 20).ToString());
        }
    }
}
