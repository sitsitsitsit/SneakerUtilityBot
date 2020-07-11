using AJAXTools.Utility;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AJAXTools.Commands
{
    public class EbayCommand : BaseCommandModule
    {
        [Command("view")]
        [Description("Adds the desired amount of views to an ebay URL.")]
        public async Task Add(CommandContext ctx,
            [Description("Product URL.")] string product,
            [Description("Amount of views.")] int views)
        {
            // Prevent Ebay ban
            if (views > 100)
            {
                await ctx.Channel.SendMessageAsync("You may not add more than 100 views at one time. :no_entry:").ConfigureAwait(false);
                return;
            }
            else
            {
                var sending = new DiscordEmbedBuilder
                {
                    Title = $"**Adding {views} views...**",
                    Color = DiscordColor.White,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "SneakerUtilityBot | Made by kalek#0001",
                        IconUrl = "https://cdn.discordapp.com/attachments/720298875159576616/731305267832160346/pp.png"
                    }
                };

                // 4 Clients running asynchronously to increase speed, can be adjusted
                await ctx.Channel.SendMessageAsync(embed: sending.Build()).ConfigureAwait(false);
                var client = await CreateProxyClient(Bot._proxies[0]);
                var client2 = await CreateProxyClient(Bot._proxies[1]);
                var client3 = await CreateProxyClient(Bot._proxies[2]);
                var client4 = await CreateProxyClient(Bot._proxies[3]);

                var view = views - (views % 3);
                List<Task> tasks = new List<Task>();
                for (int i = 0; i < (view / 3); i++)
                {
                    tasks.Add(Task.Run(() => client.GetAsync(product)));
                    tasks.Add(Task.Run(() => client2.GetAsync(product)));
                    tasks.Add(Task.Run(() => client3.GetAsync(product)));
                    tasks.Add(Task.Run(() => client4.GetAsync(product)));
                }
                // Execute tasks in parallel
                await Task.WhenAll(tasks);

                var sent = new DiscordEmbedBuilder
                {
                    Title = $"**Added {views} views.**",
                    Color = DiscordColor.White,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "SneakerUtilityBot | Made by kalek#0001",
                        IconUrl = "https://cdn.discordapp.com/attachments/720298875159576616/731305267832160346/pp.png"
                    }
                };
                await ctx.Channel.SendMessageAsync(embed: sent.Build()).ConfigureAwait(false);
            }
        }
        public static async Task<HttpClient> CreateProxyClient(Proxy proxy)
        {
            var webProxy = new WebProxy
            {
                Address = new Uri($"http://{proxy.ProxyIP}:{proxy.ProxyPort}"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,

                // *** These creds are given to the proxy server, not the web server ***
                Credentials = new NetworkCredential(
                    userName: proxy.ProxyUser,
                    password: proxy.ProxyPass)
            };
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = webProxy,
                PreAuthenticate = true,
                UseDefaultCredentials = false,
            };

            // Finally, create the HTTP client object
            var client = new HttpClient(handler: httpClientHandler, disposeHandler: false);
            return client;
        }
    }
}
