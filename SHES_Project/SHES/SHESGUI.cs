﻿using Common;
using Common.DTO;
using DatabaseLayer.SERVICES;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class SHESGUI : ISHESGUI
    {
        public static List<DateTime> dates = new List<DateTime>();
        public static bool init = false;
        public static bool startSystem = false;
        public static int brojPanelaBuffer;
        public static double[] snagePanelaBuffer;
        public static int brojBaterijaBuffer;
        public static double[] snageBaterijaBuffer;
        public static double[] kapacitetiBaterijaBuffer;
        public static double snagaEVCBuffer;
        public static double cenaUtilityBuffer;
        public static int brojPotrosacaBuffer;
        public static double[] snagePotrosacaBuffer;

        [ExcludeFromCodeCoverage]
        public List<BatteryDTO> GetBatteryData(DateTime date, string id)
        {
            ChannelFactory<IDBServices> channel = new ChannelFactory<IDBServices>("IDBServices");
            IDBServices proxy = channel.CreateChannel();

            return proxy.GetBatteryProduction(id,date);
        }

        [ExcludeFromCodeCoverage]
        public List<ConsumersDTO> GetConsumersData(DateTime date)
        {
            ChannelFactory<IDBServices> channel = new ChannelFactory<IDBServices>("IDBServices");
            IDBServices proxy = channel.CreateChannel();

            return proxy.GetConsumersProduction(date);
        }

        [ExcludeFromCodeCoverage]
        public List<DateTime> GetDates()
        {
            ChannelFactory<IDBServices> channel = new ChannelFactory<IDBServices>("IDBServices");
            IDBServices proxy = channel.CreateChannel();
            foreach (DateTime d in proxy.GetDates())
            {
                if (!dates.Contains(d)) dates.Add(d);

            }
            return dates;
        }

        [ExcludeFromCodeCoverage]
        public List<SolarPanelsDTO> GetSolarPanelsData(DateTime date)
        {
            ChannelFactory<IDBServices> channelFactory = new ChannelFactory<IDBServices>("IDBServices");
            IDBServices proxy = channelFactory.CreateChannel();
            return proxy.GetSolarPanelProduction(date);
        }

        [ExcludeFromCodeCoverage]
        public List<UtilityDTO> GetUtilityData(DateTime date)
        {
            ChannelFactory<IDBServices> channelFactory = new ChannelFactory<IDBServices>("IDBServices");
            IDBServices proxy = channelFactory.CreateChannel();
            return proxy.GetUtilityProduction(date);
        }

        public void Initialize(int brojPanela, double[] snagePanela, int brojBateija, 
            double[] snageBaterija, double[] kapacitetiBaterija, double snagaEVC, 
            double cenaUtility, int brojPotrosaca, double[] snagePotrosaca,bool init1, bool start)
        {
            if(brojPanela < 0)
            {
                throw new ArgumentOutOfRangeException("Broj panela je manji od 0!");
            }
            else if (snagePanela.Count() != brojPanela)
            {
                throw new ArgumentOutOfRangeException("Pogresno unete snage panela!");
            }
            else if(brojBateija < 0)
            {
                throw new ArgumentOutOfRangeException("Broj baterija je manji od 0!");
            }
            else if (snageBaterija.Count() != brojBateija)
            {
                throw new ArgumentOutOfRangeException("Pogresno unete snage baterija");
            }
            else if (kapacitetiBaterija.Count() != brojBateija)
            {
                throw new ArgumentOutOfRangeException("Pogresno uneti kapaciteti baterija!");
            }
            else if (snagaEVC < 0)
            {
                throw new ArgumentOutOfRangeException("Snaga EVC-a manja od 0!");
            }
            else if (cenaUtility < 0)
            {
                throw new ArgumentOutOfRangeException("Cena elektrodistribucije ne moze biti negativna!");
            }
            else if (brojPotrosaca < 0)
            {
                throw new ArgumentOutOfRangeException("Broj potrosaca ne moze biti negativan!");
            }
            else if (snagePotrosaca.Count() != brojPotrosaca)
            {
                throw new ArgumentOutOfRangeException("Pogresno unete snage potrosaca!");
            }
            else
            {
                bool flag = true;
                foreach(double br in snagePanela)
                {
                    if (br < 0)
                    {
                        flag = false;
                        throw new ArgumentOutOfRangeException("Snaga panela ne sme biti negativna!");
                    }
                }

                foreach(double br in snageBaterija)
                {
                    if(br < 0)
                    {
                        flag = false;
                        throw new ArgumentOutOfRangeException("Snaga baterije ne sme biti negativna!");
                    }
                }

                foreach (double br in kapacitetiBaterija)
                {
                    if (br < 0)
                    {
                        flag = false;
                        throw new ArgumentOutOfRangeException("Kapacitet baterije ne sme biti negativna!");
                    }
                }

                foreach (double br in snagePotrosaca)
                {
                    if (br < 0)
                    {
                        flag = false;
                        throw new ArgumentOutOfRangeException("Snaga potrosaca ne sme biti negativna!");
                    }
                }

                if (flag == true)
                {
                    brojPanelaBuffer = brojPanela;
                    snagePanelaBuffer = snagePanela;
                    brojBaterijaBuffer = brojBateija;
                    snageBaterijaBuffer = snageBaterija;
                    kapacitetiBaterijaBuffer = kapacitetiBaterija;
                    snagaEVCBuffer = snagaEVC;
                    cenaUtilityBuffer = cenaUtility;
                    brojPotrosacaBuffer = brojPotrosaca;
                    snagePotrosacaBuffer = snagePotrosaca;
                    init = init1;
                    startSystem = start;
                }
            }
            
            Trace.TraceInformation("System initialized");
        }
        
    }
}
