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


                 int vreme = 14;
                double solarniPaneli = 100;
                double potrosaci = 200;
                double avgCena = 0.139;
                double cena = 0.5;
                List<Common.Battery> baterije = new List<Battery>() {
                new Common.Battery(0.3,"b1",50,Enums.BatteryRezim.IDLE),
                new Common.Battery(0.5,"b2",25,Enums.BatteryRezim.IDLE)
            };
                Common.EVCharger ev = new EVCharger(0.7,"evc",50,Enums.BatteryRezim.IDLE);
                ev.Charge = false;
                ev.Connected = true;



                //Algoritam:
                double potrosnja = potrosaci;
                potrosnja -= solarniPaneli;

                if (ev.Connected && ev.Charge && ev.Capacity < 1)
                {
                    potrosnja += ev.MaxPower;
                    //evproxy.ZadajRezim("punjenje");
                }

                if (vreme >= 3 && vreme <= 6)
                {
                    foreach (var b in baterije)
                    {
                        if (b.Capacity < 1)
                        {
                            potrosnja += b.MaxPower;
                            //baterijaProxy.ZadajRezim("Punjenje");
                        }
                    }
                    if (ev.Connected)
                    {
                        if (ev.Capacity < 1)
                        {
                            potrosnja += ev.MaxPower;
                            //evproxy.ZadajRezim("punjenje");
                        }
                    }

                }
                else if (vreme >= 14 && vreme <= 17)
                {

                    foreach (var b in baterije)
                    {
                        if (b.Capacity > 0)
                        {
                            potrosnja -= b.MaxPower;
                            //baterijaProxy.ZadajRezim("Praznjenje");
                        }
                    }

                }
                else
                {

                    if (cena <= avgCena)
                    {

                        foreach (var b in baterije)
                        {

                            if (b.Capacity <= 1)
                            {
                                potrosnja += b.MaxPower;
                                //baterijaProxy.ZadajRezim("Punjenje");
                            }
                        }

                    }
                    else
                    {

                        foreach (var b in baterije)
                        {
                            if (b.Capacity > 0)
                            {
                                potrosnja -= b.MaxPower;
                                //baterijaProxy.ZadajRezim("Praznjenje");
                            }
                        }

                    }

                }
                Console.WriteLine(potrosnja);
                Console.ReadKey();

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
   

