# SneakerUtilityBot
A lightweight Discord bot including some sneaker related utilities. I am planning on updating this bot with new commands over time, please feel free to contribute!
# Prerequisites
* [.NET Core 3.1 or higher](https://dotnet.microsoft.com/download/dotnet-core/current/runtime)
* [DSharpPlus](https://github.com/DSharpPlus/DSharpPlus)
* [HTMLAgilityPack](https://html-agility-pack.net/)

# Current Commands
* `!fee {Listing Price}` - Calculates the payout you would receive after selling fees on popular platforms. (Paypal, Ebay, StockX, Goat, Grailed, Klekt)
* `!downloads` - Provides a list of download links for popular bots.
* `!view {Ebay URL} {Amount of views}` - Adds the desired amount of views to an Ebay listing. Capped at 100 views to  prevent suspension of Ebay accounts.
* `!nike {Nike launch URL}` - Scrapes available sizes and generates checkout redirect links for Nike SNKRS upcoming launches (Singapore, Malaysia, Thailand, Philippines, Indonesia, Taiwan, Canada, Australia).

# Usage
After installing all the prerequisistes, simply compile the code and run the .exe file. Insert your bot token in `Bot.cs` and invite the bot to your server. 
[Here](https://discordpy.readthedocs.io/en/latest/discord.html) is a guide outlining how to create a Discord bot. 4 proxies are needed to use the `!view` command, insert these in `proxies.txt`.

# Discord
Please contact me at `kalek#0001` on Discord, I would love to have a chat! Join [this](https://discord.gg/z8F4Xa9) Discord server for a supportive sneaker development community!
