using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Common.Enums;

namespace Battery
{
    class Program
    {
        static void Main(string[] args)
        {
            double capacities = 0;
            //rezim baterije
            BatteryRezim rezimRada = BatteryRezim.IDLE;

            Thread server = new Thread(Server);
            server.Start();

            ChannelFactory<ISHESBattery> channel = new ChannelFactory<ISHESBattery>("ISHESBattery");
            ISHESBattery proxy = channel.CreateChannel();

            int num = 2;
            double[] power = { 100, 200 };
            Batteries.InitializeBatteries(num, power);

            int counter = 0;
            while (true)
            {
                capacities = 0;
                rezimRada = Batteries.bufferRezim;

                foreach (Common.Battery battery in Batteries.batteries)
                {
                    capacities += battery.Capacity;

                    if (rezimRada == BatteryRezim.PUNJENJE && battery.Capacity <= battery.MaxPower - 1 && counter == 60)
                    {
                        battery.Capacity++;
                        counter = 0;
                    }
                    else if (rezimRada == BatteryRezim.PRAZNJENJE && battery.Capacity >= 1 && counter == 60)
                    {
                        battery.Capacity--;
                        counter = 0;
                    }
                    proxy.SendData(capacities, rezimRada);
                }
                counter++;
                Thread.Sleep(3000);
            }
        }

        static void Server()
        {
            using (ServiceHost host = new ServiceHost(typeof(Batteries)))
            {
                host.Open();
                while (true) ;
            }
        }
    }
}
