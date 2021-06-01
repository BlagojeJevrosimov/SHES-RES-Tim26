using Common;
using DatabaseLayer.SERVICES;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Common.Enums;

namespace SHES
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
    {
            //Inicijalizacija:
            ChannelFactory<IDBServices> kanalBaza = new ChannelFactory<IDBServices>("IDBServices");
            IDBServices proxyBaza = kanalBaza.CreateChannel();

            List<SolarPanel> solarPanels = new List<SolarPanel>();
            List<Battery> batteries = new List<Battery>();
            List<Consumer> consumers = new List<Consumer>();
            EVCharger evc = new EVCharger();
            Utility utility = new Utility();

            int brojPanela = SHESGUI.brojPanelaBuffer;
            double[] snagePanela = SHESGUI.snagePanelaBuffer;
            int brojBaterija = SHESGUI.brojBaterijaBuffer;
            double[] snageBaterija = SHESGUI.snageBaterijaBuffer;
            double[] kapacitetiBaterija = SHESGUI.kapacitetiBaterijaBuffer;
            double snagaEVC = SHESGUI.snagaEVCBuffer;
            double cenaUtility = SHESGUI.cenaUtilityBuffer;
            int brojPotrosaca = SHESGUI.brojPotrosacaBuffer;
            double[] snagePotrosaca = SHESGUI.snagePotrosacaBuffer;

            if (SHESGUI.init)
            {
                //SolarPanels:
                solarPanels = new List<SolarPanel>();
                for (int i = 0; i < brojPanela; i++)
                {
                    solarPanels.Add(new SolarPanel(i.ToString(), snagePanela[i]));
                }
                proxyBaza.SaveSolarPanels(solarPanels);

                //Batteries:
                batteries = new List<Battery>();
                for (int i = 0; i < brojBaterija; i++)
                {
                    batteries.Add(new Battery(kapacitetiBaterija[i], i.ToString(), snageBaterija[i], Enums.BatteryRezim.PUNJENJE));
                }
                proxyBaza.SaveBatteries(batteries, 0);

                //Consumers:
                consumers = new List<Consumer>();
                for (int i = 0; i < brojPotrosaca; i++)
                {
                    consumers.Add(new Consumer(snagePotrosaca[i], i.ToString(), Enums.ConsumerRezim.ON));
                }
                proxyBaza.SaveConsumers(consumers, 0);

                //EVCharger;
                evc = new EVCharger(0, "1", snagaEVC, BatteryRezim.PUNJENJE, false, false);
                proxyBaza.SaveEVCharger(evc);

                //Utility:
                utility.Price = 0.33;
                utility.Power = 0;
                proxyBaza.SaveUtility(utility,0);
            }
            else {
                solarPanels = proxyBaza.GetSolarPanels();
                batteries = proxyBaza.GetBatteries();
                consumers = proxyBaza.GetConsumers();
                evc = proxyBaza.GetEVCharger();
                utility = new Utility(proxyBaza.GetCurrentPrice());
                utility.Power = 0;
            }

            //Iniciajizacija Servera:
            Thread shesSolarPanel = new Thread(SolarPanelServerThread);
            shesSolarPanel.Start();

            Thread shesBattery = new Thread(BatteryServerThread);
            shesBattery.Start();

            Thread shesConsumer = new Thread(ConsumerServerThread);
            shesConsumer.Start();

            Thread shesGUI = new Thread(GUIServerThread);
            shesGUI.Start();

            Thread shesEVC = new Thread(EVCServerThread);
            shesEVC.Start();

            //Otvaravnje kanala:
            ChannelFactory<IBatterySHES> batteryChannel = new ChannelFactory<IBatterySHES>("IBatterySHES");
            IBatterySHES batteryProxy = batteryChannel.CreateChannel();

            ChannelFactory<IUtilitySHES> utilityChannel = new ChannelFactory<IUtilitySHES>("IUtilitySHES");
            IUtilitySHES utilityProxy = utilityChannel.CreateChannel();

            ChannelFactory<IEVChargerSHES> evchargerChannel = new ChannelFactory<IEVChargerSHES>("IEVChargerSHES");
            IEVChargerSHES evchargerProxy = evchargerChannel.CreateChannel();

            ChannelFactory<IConsumerSHES> consumerChannel = new ChannelFactory<IConsumerSHES>("IConsumerSHES");
            IConsumerSHES consumerProxy = consumerChannel.CreateChannel();

            ChannelFactory<ISolarPanelSHES> solarChannel = new ChannelFactory<ISolarPanelSHES>("ISolarPanelSHES");
            ISolarPanelSHES solarProxy = solarChannel.CreateChannel();

            solarProxy.InitializeSolarPanels(brojPanela,snagePanela);
            batteryProxy.InitializeBatteries(batteries);
            consumerProxy.InitializeConsumers(consumers);
            evchargerProxy.InitializeEVCharger(evc);
            utilityProxy.initializeUtility(utility);

            //Vrednosti potrebne za pravilan rad aplikacije: 
            double solarPanelsOutput = 0;
            double consumerEnergyConsumption = 0;
            Dictionary<string, Enums.BatteryRezim> rezimi;
            Dictionary<string, double> capacities;
            double consumerEC = 0;
            bool charge = false;
            bool connected = false;
            int brojac = 0;
            //Racunanje pocetnog vremena
            DateTime centuryBegin = new DateTime(2020, 1, 1);
            DateTime currentDate = DateTime.Now;
            long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            int vreme = (int)Math.Floor(elapsedSpan.TotalSeconds); 

            while (true)
            {
                //Preuzimanje vrednosti iz baffera:
                solarPanelsOutput = SHESSolarPanel.bufferPowerOutput;
                consumerEnergyConsumption = SHESConsumer.energyConsumptioneBuffer;
                capacities = SHESBattery.bufferCapacities;
                rezimi = SHESBattery.bufferRezimi;
                consumerEC = SHESConsumer.energyConsumptioneBuffer;
                charge = SHESEVCharger.rezimBuffer;
                connected = SHESEVCharger.plugBuffer;
                //preuzeti bafere sa GUIja




                double avgCena = 0.139;
                double cena = 0.5;

                //ove podatke dobaviti sa GUI-ja
                List<Common.Battery> baterije = new List<Battery>() {
                new Common.Battery(0.3,"1",50,Enums.BatteryRezim.PRAZNJENJE),
                new Common.Battery(0.5,"2",25,Enums.BatteryRezim.PRAZNJENJE)
                };



                //ove podatke dobaviti sa GUI-ja
                Common.EVCharger ev = new EVCharger(0.7,"evc",50,Enums.BatteryRezim.PRAZNJENJE,false,true);



                //ove podatke dobaviti sa GUI-ja
                Utility util = new Utility(2000, 500);


                //Algoritam:
                double potrosnja = consumerEnergyConsumption;

                potrosnja -= solarPanelsOutput;

                if (ev.Connected && ev.Charge && ev.Capacity < 1)
                {
                    potrosnja += ev.MaxPower;
                    evchargerProxy.SendRegime(Enums.BatteryRezim.PUNJENJE);
                }

                if (vreme >= 3 && vreme <= 6)
                {
                    foreach (var b in baterije)
                    {
                        if (b.Capacity < 1)
                        {
                            potrosnja += b.MaxPower;
                            batteryProxy.SendRegime(b.Id, Enums.BatteryRezim.PUNJENJE);
                        }
                    }
                    if (ev.Connected)
                    {
                        if (ev.Capacity < 1)
                        {
                            potrosnja += ev.MaxPower;
                            evchargerProxy.SendRegime(Enums.BatteryRezim.PUNJENJE);
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
                            batteryProxy.SendRegime(b.Id, Enums.BatteryRezim.PRAZNJENJE);
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
                                batteryProxy.SendRegime(b.Id, Enums.BatteryRezim.PUNJENJE);
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
                                batteryProxy.SendRegime(b.Id, Enums.BatteryRezim.PRAZNJENJE);
                            }
                        }

                    }

                }
                //Sacuvaj u bazu sve

                if (brojac >= 5)
                {
                    brojac = 0;
                    //Sacuvaj u bazu sve
                }
                else {
                    brojac++;
                }
                vreme += 60;
                Thread.Sleep(100);

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

        static void EVCServerThread()
        {
            using (ServiceHost host = new ServiceHost(typeof(SHESEVCharger)))
            {
                host.Open();
                while (true) ;
            }
        }
    }

}
   

