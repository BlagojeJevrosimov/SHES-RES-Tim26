using Common;
using SolarPanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolarPanel
{
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

            SolarPanelGUI.InitializeSolarPanels(3, new double[] { 50, 100, 200 });

            while (true)
            {
                sunIntensity = SolarPanelGUI.buffer;
                powerOutput = 0;
                foreach (var sp in SolarPanelGUI.solarPanels)
                {
                    powerOutput += (sp.MaxPower * sunIntensity);
                }
                proxy.SendData(powerOutput);
                Thread.Sleep(1000);
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
