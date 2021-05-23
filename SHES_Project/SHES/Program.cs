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
            double solarPanelsOutput = 0;
            double batteryCapacity = 0;
            Rezim rezim = Rezim.Idle;

            Thread solarPanelServer = new Thread(SolarPanelServerThread);
            solarPanelServer.Start();

            Thread batteryServer = new Thread(BatteryServerThread);
            batteryServer.Start();

            ChannelFactory<IBatterySHES> batteryChannel = new ChannelFactory<IBatterySHES>("IBatterySHES");
            IBatterySHES batteryProxy = batteryChannel.CreateChannel();

            //ove vrednosti se prosledjuju od UI-a
            int num = 1;

            double[] power = { 100 };
            batteryProxy.InitializeBatteries(num, power);
            
            while (true)
            {
               
                solarPanelsOutput = SHESSolarPanel.bufferPowerOutput;

                Console.WriteLine(solarPanelsOutput);

                //ovde ide logika kada se prazne i pune baterije
                batteryProxy.SendRegime(Rezim.Punjenje);

                batteryCapacity = BatteryServer.bufferCapacity;
                rezim = BatteryServer.bufferRegime;

                Console.WriteLine("Kapacitet baterija: " + batteryCapacity);

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
            using (ServiceHost host = new ServiceHost(typeof(BatteryServer)))
            {
                host.Open();
                while (true) ;
            }
        }
    }

}
   

