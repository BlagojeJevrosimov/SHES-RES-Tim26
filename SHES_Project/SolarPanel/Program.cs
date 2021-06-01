using Common;
using SolarPanels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolarPanel
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            double powerOutput = 0;
            double sunIntensity = 0;

            Thread server = new Thread(Server);
            server.Start();

            ChannelFactory<ISHESSolarPanel> channel = new ChannelFactory<ISHESSolarPanel>("ISHESSolarPanel");
            ISHESSolarPanel proxy = channel.CreateChannel();

            while (true)
            {
                sunIntensity = SolarPanelGUI.buffer;
                powerOutput = 0;
                foreach (var sp in SolarPanelSHES.solarPanels)
                {
                    powerOutput += (sp.MaxPower * sunIntensity);
                }
                proxy.SendData(powerOutput);
                Trace.TraceInformation("Sun intensity: " + sunIntensity);
                Trace.TraceInformation("Solar power output: " + powerOutput);

                Thread.Sleep(100);
            }


        }
        static void Server()
        {
            using (ServiceHost host = new ServiceHost(typeof(SolarPanelGUI)))
            {
                host.Open();
                while (true) ;
            }
        }
    }
}
