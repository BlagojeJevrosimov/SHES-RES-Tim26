using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SHES
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread batteryServer = new Thread(BatteryServerThread);
            batteryServer.Start();

            ChannelFactory<IBatterySHES> batteryChannel = new ChannelFactory<IBatterySHES>("BatterySHES");
            IBatterySHES batteryProxy = batteryChannel.CreateChannel();

            //ove vrednosti se prosledjuju od UI-a
            int num = 1;
            double[] power = { 100 };
            batteryProxy.InitializeBatteries(num, power);

            //ovde ide logika kada se prazne i pune baterije
            //batteryProxy.SendRegime(Rezim.punjenje);

        }

        static void BatteryServerThread()
        {
            using (ServiceHost host = new ServiceHost(typeof(BatteryServer)))
            {
                host.Open();
                while (true)
                {
                    BatteryServer.capacity = BatteryServer.bufferCapacity;
                    BatteryServer.rezim = BatteryServer.bufferRegime;
                    //da li treba da spava???
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
