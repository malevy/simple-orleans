using System;
using System.Net;
using Orleans;
using Orleans.Runtime.Host;

namespace simple_server
{
    class Program
    {
        private static SiloHost _host;

        static void Main(string[] args)
        {
            var hostDomain = AppDomain.CreateDomain("simple-_host", null, new AppDomainSetup
            {
                AppDomainInitializer = InitSilo,
                AppDomainInitializerArguments = args
            });

            GrainClient.Initialize("ClientConfig.xml");

            // make call here

            Console.WriteLine("\nPress enter to stop");
            Console.ReadLine();

            hostDomain.DoCallBack(ShutdownSilo);

        }

        private static void ShutdownSilo()
        {
            if (null == _host) return;
            _host.StopOrleansSilo();
            _host.Dispose();
            _host = null;
        }

        private static void InitSilo(string[] args)
        {
            _host = new SiloHost(Dns.GetHostName())
            {
                ConfigFileName = "OrleansConfiguration.xml"
            };
            _host.LoadOrleansConfig();
            _host.InitializeOrleansSilo();
            if (_host.StartOrleansSilo())
            {
                Console.WriteLine($"Started host {_host.Name} as a {_host.Type} node");
            }
            else
            {
                Console.WriteLine($"Failed to start host {_host.Name}");
            }
        }
    }
}
