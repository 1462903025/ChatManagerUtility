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
    public delegate void TeamMsgEventHandler(TeamMsgEventArgs ev);

    //https://stackoverflow.com/questions/623451/how-can-i-make-my-own-event-in-c
    [CommandHandler(typeof(ClientCommandHandler))]
    class TeamMessaging : ICommand
    {
        public string Command { get; } = "TeamMessaging";

        public string[] Aliases { get; } = { "c", "team", "td", "团队"};

        public string Description { get; } = "团队聊天";

        public static event TeamMsgEventHandler IncomingTeamMessage;

        /// <summary>
        /// Handles incoming messages from client console for Team messages. 
        /// </summary>
        /// <returns></returns>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (!ChatManagerUtilityMain.Instance.Config.MsgTypesAllowed.Contains(Configs.MessageType.TEAM))
            {
                response = "管理员已禁用此功能。联系他们以启用团队聊天.";
                return false;
            }

            if (arguments.Count == 0)
            {
                response = "您必须提供要发送的消息";
                return false;
            }

            try
            {
                Player player = Player.Get(sender);
                if (player.Role.Type is RoleType.Spectator)
                {
                    response = "在观众模式下无法发送本地消息.";
                    return false;
                }
                String nameToShow = player.Nickname.Length < 6 ? player.Nickname : player.Nickname.Substring(0, (player.Nickname.Length / 3) + 1);
                IncomingTeamMessage?.Invoke(new TeamMsgEventArgs($"[团队][{nameToShow}]:" + String.Join(" ", arguments.ToList()), player));
                response = "已处理团队聊天.";
                return true;
            }
            catch (Exception ex){
                response = $"无法发送团队聊天，因为 {ex}";
            }
            return false;
        }
    }
}
