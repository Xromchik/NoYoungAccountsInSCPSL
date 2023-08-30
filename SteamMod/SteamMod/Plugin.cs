using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using HtmlAgilityPack;
using System;

namespace SteamMod
{
    class Sawa : Plugin<Config>
    {
        public static Sawa plugin;
        public override string Prefix => "SteamMod";
        public override string Name => "SteamMod";
        public override string Author => "Julik";
        public override System.Version Version { get; } = new System.Version(0, 0, 1);

        public override void OnEnabled()
        {
            plugin = this;
            Exiled.Events.Handlers.Player.Verified += SendingARequest;
            base.OnEnabled();
        }
        private void SendingARequest(VerifiedEventArgs ev)
        {
            string x = ev.Player.UserId;
            int x1 = x.Length - 6; // Удаляю приписку @steam
            x = x.Remove(x1);

            string steamIdValue = x;

            string url = $"https://steamid.io/lookup/{steamIdValue}";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            // Использую XPath для поиска нужного элемента
            HtmlNode infoNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"content\"]/dl/dd[6]");
            string infoText;
            if (infoNode != null)
            {
                infoText = infoNode.InnerText;
            }
            else
            {
                infoText = "Jan 1, 2009";
            }

            // Считаю разницу между датами 
            string dateInput = infoText;
            var parsedDate = DateTime.Parse(dateInput);

            DateTime now = DateTime.Now;

            TimeSpan ts = now.Subtract(parsedDate);
            double diff = ts.TotalDays;


            if (diff < plugin.Config.MinDay)
            {
                ev.Player.Kick(plugin.Config.KickTxt);
            }
        }

        public override void OnDisabled()
        {
            plugin = this;
            Exiled.Events.Handlers.Player.Verified -= SendingARequest;
            base.OnDisabled();
        }
    }
}
