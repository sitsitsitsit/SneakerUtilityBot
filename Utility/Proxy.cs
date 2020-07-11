using System;
using System.Collections.Generic;
using System.Text;

namespace AJAXTools.Utility
{
    public class Proxy
    {
        public string ProxyIP { get; set; }
        public int ProxyPort { get; set; }
        public string ProxyUser { get; set; }
        public string ProxyPass { get; set; }
        public Proxy(string proxyIp, int proxyPort, string proxyUser, string proxyPass)
        {
            ProxyIP = proxyIp;
            ProxyPort = proxyPort;
            ProxyUser = proxyUser;
            ProxyPass = proxyPass;
        }

        public static Proxy ParseProxy(string line)
        {
            var parsedProxy = line.Split(':');

            string proxyIP = parsedProxy[0];
            int proxyPort = Int32.Parse(parsedProxy[1]);
            string proxyUser = parsedProxy[2];
            string proxyPass = parsedProxy[3];

            var currentProxy = new Proxy(proxyIP, proxyPort, proxyUser, proxyPass);
            return currentProxy;
        }
    }
}
