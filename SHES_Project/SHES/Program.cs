using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Common.Enums;

namespace SHES
{
    class Program
    {
        static void Main(string[] args)
        {
            //Vrednosti potrebne za pravilan rad aplikacije: 
            double solarPanelsOutput = 0;
            double batteryCapacity = 0;
            double consumerEnergyConsumption = 0;
            BatteryRezim rezim = BatteryRezim.IDLE;

            //Iniciajizacija Servera:
            Thread solarPanelServer = new Thread(SolarPanelServerThread);
            solarPanelServer.Start();

            Thread batteryServer = new Thread(BatteryServerThread);
            batteryServer.Start();

            Thread consumerServer = new Thread(ConsumerServerThread);
            consumerServer.Start();

            Thread GUIServer = new Thread(GUIServerThread);
            GUIServer.Start();

            //Otvaravnje kanala:
            ChannelFactory<IBatterySHES> batteryChannel = new ChannelFactory<IBatterySHES>("IBatterySHES");
            IBatterySHES batteryProxy = batteryChannel.CreateChannel();

            ChannelFactory<IUtilitySHES> utilityChannel = new ChannelFactory<IUtilitySHES>("IUtilitySHES");
            IUtilitySHES utilityProxy = utilityChannel.CreateChannel();

            ChannelFactory<IEVChargerSHES> evchargerChannel = new ChannelFactory<IEVChargerSHES>("IEVChargerSHES");
            IEVChargerSHES evchargerProxy = evchargerChannel.CreateChannel();
            
            while (true)
            {
                //Preuzimanje vrednosti iz baffera:
                solarPanelsOutput = SHESSolarPanel.bufferPowerOutput;
                batteryCapacity = SHESBattery.bufferCapacity;
                rezim = SHESBattery.bufferRegime;
                consumerEnergyConsumption = SHESConsumer.energyConsumptioneBuffer;
                //preuzeti bafere sa GUIja

                Thread.Sleep(3000);
            }

        }

        static void SolarPanelServerThread()
        {
            using (ServiceHost host = new ServiceHost(typeof(SHESSolarPanel)))
            {
                host.Open();
                while (true) ;
            }
        }

        static void BatteryServerThread()
        {
            using (ServiceHost host = new ServiceHost(typeof(SHESBattery)))
            {
                host.Open();
                while (true) ;
            }
        }

        static void ConsumerServerThread() {
            using (ServiceHost host = new ServiceHost(typeof(SHESConsumer))) {

                host.Open();
                while (true) ;
            }
        }

        static void GUIServerThread()
        {
            using (ServiceHost host = new ServiceHost(typeof(SHESGUI)))
            {

                host.Open();
                while (true) ;
            }
        }
    }

}
   

