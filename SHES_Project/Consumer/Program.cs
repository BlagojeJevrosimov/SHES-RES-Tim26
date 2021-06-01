using Common;
using SHES;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {

            Thread serverGUI = new Thread(ServerGUI);
            serverGUI.Start();

            Thread serverSHES = new Thread(ServerSHES);
            serverSHES.Start();
        }

        public static void ServerGUI()
        {
            using (ServiceHost host = new ServiceHost(typeof(ConsumerGUI)))
            {
                host.Open();
                while (true) ;
            }
        }

        public static void ServerSHES()
        {
            using (ServiceHost host = new ServiceHost(typeof(ConsumerSHES)))
            {
                host.Open();
                while (true) ;
            }
        }
    }
}
