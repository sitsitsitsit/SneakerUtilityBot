using System;

namespace AJAXTools
{
    class Program
    {
        // Asynchronous main
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
