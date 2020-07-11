using AJAXTools.Utility;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AJAXTools.Commands
{
    public class OtherCommands : BaseCommandModule
    {
        [Command("fee")]
        [Description("Calculates the money received after fees.")]
        public async Task Add(CommandContext ctx,
            [Description("Listing price.")] double price)
        {
            var paypal = (price * 0.971) - 0.3;
            paypal = Math.Round(paypal, 2);
            var ebay = price * 0.9;
            ebay = Math.Round(ebay, 2);
            var stockx = price * 0.905;
            stockx = Math.Round(stockx, 2);
            var goat = (price * 0.905) - 5;
            goat = Math.Round(goat, 2);
            var grailed = price * 0.94;
            grailed = Math.Round(grailed, 2);
            var klekt = price * 0.8;
            klekt = Math.Round(klekt, 2);

            var embed = new DiscordEmbedBuilder
            {
                Title = "**Fees calculated**",
                Color = DiscordColor.White,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "SneakerUtilityBot | Made by kalek#0001",
                    IconUrl = "https://cdn.discordapp.com/attachments/720298875159576616/731305267832160346/pp.png"
                }
            };
            embed.AddField("Paypal", $"${paypal}");
            embed.AddField("Ebay", $"${ebay}");
            embed.AddField("StockX", $"${stockx}");
            embed.AddField("Goat", $"${goat}");
            embed.AddField("Grailed", $"${grailed}");
            embed.AddField("Klekt", $"${klekt}");
            await ctx.Channel.SendMessageAsync(embed: embed.Build()).ConfigureAwait(false);
        }
       
        [Command("downloads")]
        [Description("Sends some useful bot download links.")]
        public async Task Add(CommandContext ctx)
        {
            var downloads = new DiscordEmbedBuilder
            {
                Title = "**Bot downloads:**",
                Color = DiscordColor.White,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "SneakerUtilityBot | Made by kalek#0001",
                    IconUrl = "https://cdn.discordapp.com/attachments/720298875159576616/731305267832160346/pp.png"
                }
            };
            downloads.AddField("_", "[Cyber](https://cybersole.io/download)\n[EVE](http://eve-robotics.com/release/EveAIO_setup.exe)\n[Dashe](http://updater.dashe.io/)\n[Ghost SNKRS](https://ghost.shoes/l/snkrs)\n[TKS](http://thekickstationapi.com/downloads/Installer.msi)\n[PD](https://shopify.projectdestroyer.com/download)\n[NSB](https://nsb.nyc3.digitaloceanspaces.com/NSB-win-latest.exe)\n[Splashforce](https://update.splashforce.io/)\n[Mek](https://mekpreme-auto-updater.sfo2.digitaloceanspaces.com/latest/MEKpreme%20Setup%200.4.99.exe)", inline: true);
            downloads.AddField("_", "[Kodai](https://kodai.io/download)\n[Phantom](https://ghost.shoes/l/phantom)\n[ANB](https://s3-us-west-2.amazonaws.com/aio-v2/AIO+Bot+-+V2+Setup.exe)\n[Tohru](https://tohru.io/download)\n[SoleAIO](https://drive.google.com/open?id=1GRpzE8Ofc2fY_ueNwvut4QjoKm4SdpGr)\n[HawkMesh](http://download.hawkmesh.com/)\n[Balko](https://s3.amazonaws.com/balkobot.com/Balkobot2-0/balkobot2-setup.exe)\n[Wrath](https://download.wrathbots.co/)\n[Velox](https://velox.nyc3.cdn.digitaloceanspaces.com/Velox%20Setup%204.4.2.exe)", inline: true);
            await ctx.Channel.SendMessageAsync(embed: downloads.Build()).ConfigureAwait(false);
        }
        
        [Command("nike")]
        [Description("Creates Nike SNKRS checkout links.")]
        public async Task Add(CommandContext ctx,
            [Description("Product URL")] string productLink)
        {
            var linkList = new List<string>();
            var source = NikeEL.DownloadHtml(productLink);
            var doc = new HtmlDocument();
            doc.LoadHtml(source.Result);
            var productName = NikeEL.InitialLinkParse(doc);
            var region = NikeEL.ParseRegion(productLink);
            var sizes = NikeEL.GetSizes(source.Result);
            if (region == "au" || region == "ca")
            {
                foreach (var size in sizes)
                {
                    linkList.Add(NikeEL.AuCaLinkCreation(doc, productLink, size));
                }
            }
            else
            {
                foreach (var size in sizes)
                {
                    linkList.Add(NikeEL.ATCLinkCreation(doc, productLink, size));
                }
            }

            var mainString = NikeEL.GenerateMainString(linkList, sizes, productName);

            var nikeEmbed = new DiscordEmbedBuilder
            {
                Title = $"**Generated links.**",
                Description = $"{mainString}",
                Color = DiscordColor.White,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "SneakerUtilityBot | Made by kalek#0001",
                    IconUrl =
                            "https://cdn.discordapp.com/attachments/720298875159576616/731305267832160346/pp.png"
                }
            };
            await ctx.Channel.SendMessageAsync(embed: nikeEmbed.Build()).ConfigureAwait(false);
        }
    }
}
