using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamMod
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;
        [Description("The number of days you need to have on your account to enter the server (int)")]
        public int MinDay { get; set; } = 7;
        [Description("Text displayed when a player is kicked from the server (string)")]
        public string KickTxt { get; set; } = "Sorry, your account is too few days old, you can not play on this server";
    }
}
