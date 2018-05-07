using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Contracts;
using System.IO;

namespace HelloWorld
{
    public class Server
    {
        private ServiceHost serviceHost;

        public Server(int portNumber)
        {
            Port = portNumber;
            Start();
        }

        public static int Port { get; set; }

        public void Start()
        {            
            serviceHost = new ServiceHost(typeof(Container));
            NetTcpBinding binding = new NetTcpBinding();
            serviceHost.AddServiceEndpoint(typeof(IContainer), binding, new Uri(string.Format("net.tcp://localhost:{0}/Container", Port)));

            serviceHost.Open();
            Console.WriteLine("Server (PORT: {0}) ready and waiting for requests...", Port);
        }

        public void Stop()
        {
            serviceHost.Close();
            Console.WriteLine("Server has been closed.");
        }
    }
}
