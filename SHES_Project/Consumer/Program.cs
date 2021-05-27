using Common;
using SHES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //zameniti ovaj broj nakon inicijalizacije sistema sa stvarnim brojem consumera
            

            Thread serverGUI = new Thread(ServerGUI);
            serverGUI.Start();


        }

        public static void ServerGUI()
        {
            using (ServiceHost host = new ServiceHost(typeof(ConsumerGUI)))
            {
                host.Open();
                while (true) ;
            }
        }
    }
}
