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

            Thread serverGUI = new Thread(ServerGUI);
            serverGUI.Start();
            Thread serverSHES = new Thread(ServerSHES);
            serverSHES.Start();

            ChannelFactory<ISHESSolarPanel> channel = new ChannelFactory<ISHESSolarPanel>("ISHESSolarPanel");
            ISHESSolarPanel proxy = channel.CreateChannel();

            while (true)
            {
                if (SolarPanelSHES.initialized)
                {
                    sunIntensity = SolarPanelGUI.buffer;
                    powerOutput = 0;
                    foreach (var sp in SolarPanelSHES.solarPanels)
                    {
                        powerOutput += (sp.MaxPower * sunIntensity);
                    }
                    try { proxy.SendData(powerOutput); }
                    catch (Exception e)
                    {
                        Trace.TraceInformation("Waiting for server to load up.");
                        break;
                    }
                    //Trace.TraceInformation("Sun intensity: " + sunIntensity);
                    //Trace.TraceInformation("Solar power output: " + powerOutput);

                    Thread.Sleep(300);
                }
            }


        }
        static void ServerGUI()
        {
            using (ServiceHost host = new ServiceHost(typeof(SolarPanelGUI)))
            {
                host.Open();
                while (true) ;
            }
        }
        static void ServerSHES()
        {
            using (ServiceHost host = new ServiceHost(typeof(SolarPanelSHES)))
            {
                host.Open();
                while (true) ;
            }
        }
    }
}
