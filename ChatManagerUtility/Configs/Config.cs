using ChatManagerUtility.Configs;
using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatManagerUtility
{
    public class Config : IConfig
    {
        /// <inheritdoc />
        [Description("是否启用或禁用插件")]
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc />
        [Description("是否启用/禁用调试")]
        public bool IsDebugEnabled { get; set; } = false;

        /// <summary>
        /// In essence, how long the thread will sleep before trying to look for new messages from internal queue.
        /// </summary>
        [Description("在每次更新中休眠多长时间才能显示更多消息（以秒为单位）.")]
        public float SleepTime { get; set; } = 3f;

        /// <summary>
        /// How many characters per line are allowed. 
        /// </summary>
        [Description("每行要显示的字符数")]
        public int CharacterLimit { get; set; } = 64;

        /// <summary>
        /// Amount of messages to show both to console, and hint.
        /// </summary>
        [Description("要显示的行数")]
        public int DisplayLimit { get; set; } = 15;

        /// <summary>
        /// How long messages live for both in console, and hint.
        /// </summary>
        [Description("显示消息的时间")]
        public float DisplayTimeLimit { get; set; } = 3f;

        /// <summary>
        /// What part of the screent to place the text (Always on bottom)
        /// </summary>
        [Description("放置文本的位置（始终位于底部）")]
        public LocationEnum TextPlacement { get; set; } = LocationEnum.Left;

        /// <summary>
        /// Gets or sets what types of end round outputs should be shown.
        /// </summary>
        [Description("聊天颜色实例")]
        public ChatColors AssociatedChatColors { get; set; } = new ChatColors();

        /// <summary>
        /// Text size, based on http://digitalnativestudios.com/textmeshpro/docs/rich-text/#color
        /// </summary>
        [Description("要显示的文本大小")]
        public string SizeOfHintText { get; set; } = "<size=50%>";

        /// <summary>
        /// Whether to allow certain chat's to be enabled/disabled.
        /// </summary>

        [Description("是否允许消息类型，如果未指定，则将忽略该类型，并拒绝针对该类型的命令.")]
        public HashSet<MessageType> MsgTypesAllowed { get; set; } = new HashSet<MessageType>() { MessageType.GLOBAL, MessageType.LOCAL, MessageType.PRIVATE, MessageType.TEAM };

        /// <summary>
        /// Disables or enables sending to client console
        /// </summary>
        [Description("是否将消息发送到控制台")]
        public bool SendToConsole { get; set; } = false;

        /// <summary>
        /// Disables or enables sending to client hint system
        /// </summary>
        [Description("是否将消息发送到提示系统")]
        public bool SendToHintSystem { get; set; } = true;

        /// <summary>
        /// Thet type of colors to use for the hint system, console does not accept the same as far as I can tell.
        /// </summary>
        [Description("聊天室颜色")]
        public class ChatColors {

            /// <summary>
            /// Use hex for color type
            /// </summary>
            [Description("全局聊天颜色-使用十六进制指定颜色.")]
            public string GlobalChatColor { get; set; } = "<color=#85C7F2> ";

            /// <summary>
            /// Use hex for color type
            /// </summary>
            [Description("附近聊天颜色-使用十六进制指定颜色.")]
            public string LocalChatColor { get; set; } = "<color=#85C7F2> ";

            /// <summary>
            /// Use hex for color type
            /// </summary>
            [Description("私人聊天颜色-使用十六进制指定颜色.")]
            public string PrivateChatColor { get; set; } = "<color=#ADD7F6> ";

            /// <summary>
            /// Use hex for color type
            /// </summary>
            [Description("团队聊天颜色-使用十六进制指定颜色.")]
            public string TeamChatColor { get; set; } = "<color=#3B28CC> ";



            /// <summary>
            /// Parses the color type
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            internal string ParseColor(MessageType type){
                switch(type){
                    case MessageType.GLOBAL:
                        return GlobalChatColor;
                    case MessageType.LOCAL:
                        return LocalChatColor;
                    case MessageType.PRIVATE:
                        return PrivateChatColor;
                    case MessageType.TEAM:
                        return TeamChatColor;
                    default:
                        return "<color=#4C4C4C> ";
                }
            }
        }
    }
}
