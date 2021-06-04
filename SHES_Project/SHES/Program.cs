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

            //Vrednosti potrebne za pravilan rad aplikacije: 
            double solarPanelsOutput = 0;
            double consumerEnergyConsumption = 0;
            int brojac = 150;
            //Racunanje pocetnog vremena
            DateTime centuryBegin = new DateTime(2020, 1, 1);
            DateTime currentDate = DateTime.Now.Date;


            long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            int vreme = (int)Math.Floor(elapsedSpan.TotalSeconds);

            double cenaDana = 0;
            bool systemInitialized = false;
            while (true)
            {
               if(!systemInitialized){ 
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
                    utility.Price = 0.139;
                    utility.Power = 0;
                    proxyBaza.SaveUtility(utility, 0);

                    batteryProxy.InitializeBatteries(batteries);
                    utilityProxy.initializeUtility(utility);
                    evchargerProxy.InitializeEVCharger(evc);
                    consumerProxy.InitializeConsumers(consumers);
                    double[] p = new double[solarPanels.Count];
                    int j = 0;
                    foreach (var sp in solarPanels)
                    {
                        p[j] = sp.MaxPower;
                        j++;

                    }
                    solarProxy.InitializeSolarPanels(solarPanels.Count(), p);
                    systemInitialized = true;
                }
                else
                {
                    solarPanels = proxyBaza.GetSolarPanels();
                    batteries = proxyBaza.GetBatteries();
                    consumers = proxyBaza.GetConsumers();
                    evc = proxyBaza.GetEVCharger();
                    utility = new Utility(proxyBaza.GetCurrentPrice());
                    utility.Power = 0;

                    batteryProxy.InitializeBatteries(batteries);
                    utilityProxy.initializeUtility(utility);
                    evchargerProxy.InitializeEVCharger(evc);
                    consumerProxy.InitializeConsumers(consumers);
                    double[] p = new double[solarPanels.Count];
                    int j = 0;
                    foreach (var sp in solarPanels)
                    {
                        p[j] = sp.MaxPower;
                        j++;

                    }
                    solarProxy.InitializeSolarPanels(solarPanels.Count(), p);
                    systemInitialized = true;
                }
               }
                //Preuzimanje vrednosti iz baffera:
                solarPanelsOutput = SHESSolarPanel.bufferPowerOutput;

                consumerEnergyConsumption = SHESConsumer.energyConsumptioneBuffer;
                consumers = SHESConsumer.consumers;
                
                foreach (var b in batteries)
                {
                    b.State = SHESBattery.bufferRezimi[b.Id];
                    b.Capacity = SHESBattery.bufferCapacities[b.Id];
                }

                evc.Charge = SHESEVCharger.rezimBuffer;
                evc.Connected = SHESEVCharger.plugBuffer;

                utility.Price = utilityProxy.getPrice();

                double avgCena = 0.139;

                //Algoritam:
                double potrosnja = consumerEnergyConsumption;

                potrosnja -= solarPanelsOutput;

                if (evc.Connected && evc.Charge && evc.Capacity < 1)
                {
                    potrosnja += evc.MaxPower;
                    evc.State = Enums.BatteryRezim.PUNJENJE;
                    evchargerProxy.SendRegime(Enums.BatteryRezim.PUNJENJE);
                }

                if (brojac >= 36 && brojac <= 72)
                {
                    foreach (var b in batteries)
                    {
                        if (b.Capacity < 1)
                        {
                            potrosnja += b.MaxPower;
                            b.State = Enums.BatteryRezim.PUNJENJE;
                            batteryProxy.SendRegime(b.Id, Enums.BatteryRezim.PUNJENJE);
                        }
                    }
                    if (evc.Connected)
                    {
                        if (evc.Capacity < 1)
                        {
                            potrosnja += evc.MaxPower;
                            evc.State = Enums.BatteryRezim.PUNJENJE;
                            evchargerProxy.SendRegime(Enums.BatteryRezim.PUNJENJE);
                        }
                    }

                }
                else if (brojac >= 168 && brojac <= 204)
                {

                    foreach (var b in batteries)
                    {
                        if (b.Capacity > 0)
                        {
                            potrosnja -= b.MaxPower;
                            b.State = Enums.BatteryRezim.PRAZNJENJE;
                            batteryProxy.SendRegime(b.Id, Enums.BatteryRezim.PRAZNJENJE);
                        }
                    }

                }
                else
                {

                    if (utility.Price <= avgCena)
                    {

                        foreach (var b in batteries)
                        {

                            if (b.Capacity <= 1)
                            {
                                potrosnja += b.MaxPower;
                                b.State = Enums.BatteryRezim.PUNJENJE;
                                batteryProxy.SendRegime(b.Id, Enums.BatteryRezim.PUNJENJE);
                            }
                        }

                    }
                    else
                    {

                        foreach (var b in batteries)
                        {
                            if (b.Capacity > 0)
                            {
                                potrosnja -= b.MaxPower;
                                b.State = Enums.BatteryRezim.PRAZNJENJE;
                                batteryProxy.SendRegime(b.Id, Enums.BatteryRezim.PRAZNJENJE);
                            }
                        }

                    }

                }
                utilityProxy.sendRequestforEnergy(potrosnja);
                utility.Power = potrosnja;

                cenaDana += (potrosnja * utility.Price);

                    proxyBaza.SaveSolarPanelProduction(solarPanelsOutput,vreme);
                    proxyBaza.SaveConsumers(consumers,vreme);
                    proxyBaza.SaveBatteries(batteries, vreme);
                    proxyBaza.SaveUtility(utility, vreme);
                
                if (brojac == 288)
                {

                    
                    proxyBaza.SaveTotalExpenditure(currentDate,(int)cenaDana);
                    brojac = 0;
                    cenaDana = 0;
                    currentDate.AddDays(1);
                }
                

                    brojac++;

                
                vreme += 300;
                Thread.Sleep(1000);

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
   

