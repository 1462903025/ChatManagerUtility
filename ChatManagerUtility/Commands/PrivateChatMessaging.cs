using ChatManagerUtility.Events;
using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatManagerUtility
{
    //First we have to define a delegate that acts as a signature for the
    //function that is ultimately called when the event is triggered.
    //You will notice that the second parameter is of MyEventArgs type.
    //This object will contain information about the triggered event.
    public delegate void PrivateMsgEventHandler(PrivateMsgEventArgs ev);

    //https://stackoverflow.com/questions/623451/how-can-i-make-my-own-event-in-c
    [CommandHandler(typeof(ClientCommandHandler))]
    class PrivateMessaging : ICommand
    {
        public string Command { get; } = "PrivateMessaging";

        public string[] Aliases { get; } = { "p", "private", "sl", "私聊"};

        public string Description { get; } = "私人聊天";

        public static event PrivateMsgEventHandler IncomingPrivateMessage;

        /// <summary>
        /// Handles incoming messages from client console for private messages. 
        /// </summary>
        /// <returns></returns>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!ChatManagerUtilityMain.Instance.Config.MsgTypesAllowed.Contains(Configs.MessageType.PRIVATE))
            {
                response = "管理员已禁用此功能。联系他们以启用私人聊天.";
                return false;
            }

            if (arguments.Count == 0)
            {
                response = "您必须提供要发送的消息";
                return false;
            }

            try{
                Player player = Player.Get(sender);
                Player targetPlayer = Player.Get(arguments.At(0));
                if (player.Role.Type is RoleType.Spectator && targetPlayer.Role.Type != RoleType.Spectator)
                {
                    response = "在观众模式下，无法向非观众玩家发送私人消息.";
                    return false;
                }

                StringBuilder sb = new StringBuilder();
                for (int pos = 1; pos < arguments.Count; pos++)
                {
                    sb.Append(arguments.At(pos));
                    sb.Append(" ");
                }
                
                IncomingPrivateMessage?.Invoke(new PrivateMsgEventArgs($"[P][{player.Nickname}]:" + sb.ToString(), player, targetPlayer));
                response = "已接受私聊信息";
                return true;
            }
            catch (Exception ex){
                response = $"无法发送私聊信息，因为 {ex}";
            }
            return false;
        }
    }
}
