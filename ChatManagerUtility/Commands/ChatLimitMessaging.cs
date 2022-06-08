using ChatManagerUtility.Configs;
using ChatManagerUtility.Events;
using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatManagerUtility.Commands
{
    public delegate void ChatLimitEventHandler(ChatLimitEventArgs ev);

    [CommandHandler(typeof(ClientCommandHandler))]
    class ChatLimitMessaging : ICommand
    {
        public string Command { get; } = "ChatLimit";

        public string[] Aliases { get; } = { "cl", "chatl" };

        public string Description { get; } = "聊天限制";

        public static event ChatLimitEventHandler IncomingChatLimitMessage;

        /// <summary>
        /// Handles incoming messages from client console for individuals to subscribe/unsubscribe from streams (channels).
        /// </summary>
        /// <returns></returns>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if(arguments.Count == 0){
                response = "你必须提供一个参数, 包括: qb, fj, sl, td ";
                return false;
            }
            Player player = Player.Get(sender);
            if(Enum.TryParse(arguments.At(0).ToUpper(), out MessageType channel)){
                IncomingChatLimitMessage?.Invoke(new ChatLimitEventArgs(arguments.At(0), player, channel));
                response = "消息已被接受";
                return true;
            }
            response = $"更改订阅的频道指定不正确: {arguments.At(0)}. 选项包括: qb, fj, sl, td";
            return false;
           
        }
    }
}
