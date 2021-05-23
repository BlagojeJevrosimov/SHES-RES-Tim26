﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Common.Enums;

namespace BatteryComponent
{
    class Program
    {
        static void Main(string[] args)
        {
            double capacities = 0;
            Rezim rezimRada = Rezim.Idle;
            //preuzeti vrednosti iz bafera iz baterije i dodeliti pravim poljima
            Thread server = new Thread(Server);
            server.Start();

            ChannelFactory<ISHESBattery> channel = new ChannelFactory<ISHESBattery>("ISHESBattery");
            ISHESBattery proxy = channel.CreateChannel();

            while (true)
            {
                capacities = 0;
                rezimRada = Batteries.bufferRezim;

                foreach (Common.Battery battery in Batteries.batteries)
                {
                    capacities += battery.Capacity;

                    if (rezimRada == Rezim.Punjenje && battery.Capacity <= battery.MaxPower - 1)
                    {
                        battery.Capacity++;
                    }
                    else if (rezimRada == Rezim.Praznjenje && battery.Capacity >= 1)
                    {
                        battery.Capacity--;
                    }
                    proxy.SendData(capacities, rezimRada);
                }
                Thread.Sleep(1000);
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
