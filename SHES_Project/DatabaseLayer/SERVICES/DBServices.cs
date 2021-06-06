using Common;
using Common.DTO;
using DatabaseLayer.DAO;
using DatabaseLayer.DAO.Implementacije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.SERVICES
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DBServices : IDBServices
    {
       
        public List<Battery> GetBatteries()
        {
            IBatteries batteries = new Batteries();
            List<Tuple<string,int>> ids = batteries.GetIdsForInit();
            List<Battery> list = new List<Battery>();
            foreach (var s in ids) {
                Battery b = batteries.FindById(s.Item1,s.Item2);
                list.Add(b);
            }
            return list;
        }

        public List<BatteryDTO> GetBatteryProduction(string id, DateTime date)
        {
            IBatteries batteries = new Batteries();
            DateTime centuryBegin = new DateTime(2020, 1, 1);

            long ticks = date.Ticks - centuryBegin.Ticks;
            long secondsStart = ticks / 10000000;
            long secondsEnd = secondsStart + 86400;
            List<BatteryDTO> list = (List<BatteryDTO>)batteries.FindAllById(id, (int)secondsStart, (int)secondsEnd); ;
            return list;
        }
        public List<Consumer> GetConsumers()
        {
            IConsumers consumers = new Consumers();
            List<Consumer> list = new List<Consumer>();
            List<Tuple<string, int>> ids = consumers.GetIdsForInit();
            foreach (var s in ids) {
                Consumer c = consumers.FindById(s.Item1,s.Item2);
                list.Add(c);

            }

            return list;
        }

        public List<ConsumersDTO> GetConsumersProduction(DateTime date)
        {
            IConsumers consumers = new Consumers();
            DateTime centuryBegin = new DateTime(2020, 1, 1);

            long ticks = date.Ticks - centuryBegin.Ticks;
            long secondsStart = ticks / 10000000;
            long secondsEnd = secondsStart + 86400;
            return (List<ConsumersDTO>)consumers.FindAll((int)secondsStart, (int)secondsEnd);
        }

        public double GetCurrentPrice()
        {
            IUtility utility = new DAO.Implementacije.Utility();
            return utility.GetCurrentPrice();
        }

        public EVCharger GetEVCharger()
        {
            IEVChargers evc = new EVChargers();
            EVCharger e = evc.FindById("1");
            return e;
        }

        public List<SolarPanelsDTO> GetSolarPanelProduction(DateTime date)
        {
            ISolarPanels solar = new SolarPanels();
            DateTime centuryBegin = new DateTime(2020, 1, 1);

            long ticks = date.Ticks - centuryBegin.Ticks;
            long secondsStart = ticks / 10000000;
            long secondsEnd = secondsStart + 86400;
            return (List<SolarPanelsDTO>)solar.FindAll((int)secondsStart, (int)secondsEnd);
        }

        public List<SolarPanel> GetSolarPanels()
        {
            ISolarPanels solar = new SolarPanels();
            return (List<SolarPanel>)solar.FindAll();
        }

        public List<UtilityDTO> GetUtilityProduction(DateTime date)
        {
            IUtility utility = new DAO.Implementacije.Utility();
            DateTime centuryBegin = new DateTime(2020, 1, 1);

            long ticks = date.Ticks - centuryBegin.Ticks;
            long secondsStart = ticks / 10000000;
            long secondsEnd = secondsStart + 86400;
            return (List<UtilityDTO>)utility.FindAll((int)secondsStart, (int)secondsEnd);
        }

        public void SaveBatteries(IEnumerable<Battery> list, int time)
        {
            IBatteries batteries = new Batteries();
            List<BatteryDTO> list2 = new List<BatteryDTO>();
            foreach (var b in list) {
                list2.Add( new BatteryDTO(b.Capacity,b.Id,b.MaxPower,b.State,time));

            }
            batteries.SaveAll(list2);
        }

        public void SaveConsumers(IEnumerable<Consumer> list, int time)
        {
            IConsumers consumers = new Consumers();
            

            consumers.SaveAll(list,time);
        }


        public void SaveEVCharger(EVCharger evc)
        {
            IEVChargers evcs = new EVChargers();
            evcs.Save(evc);
        }

        public void SaveSolarPanelProduction(double power, int time)
        {
            ISolarPanels solar = new SolarPanels();
            solar.SaveProduction(power,time);
        }

        public void SaveSolarPanels(IEnumerable<SolarPanel> list)
        {
            ISolarPanels solar = new SolarPanels();
            solar.SaveAll(list);
        }

        public void SaveTotalExpenditure(DateTime date, int total)
        {
            ISHES shes = new DAO.Implementacije.SHES();
            shes.Save(date.ToString("dd/MM/yyyy"), total);
        }

        public void SaveUtility(Common.Utility utility,int time)
        {
            IUtility util = new DAO.Implementacije.Utility();
            util.Save(utility);
            util.SaveProduction(utility,time);

        }
        public List<DateTime> GetDates() {

            ISHES shes = new DAO.Implementacije.SHES();

            return shes.GetDates();
        }
    }
}
