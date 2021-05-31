using DatabaseLayer.DAO;
using DatabaseLayer.DAO.Implementacije;
using DatabaseLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.SERVICES
{
    public class DBServices : IDBServices
    {
        public void GetBatteryProduction(string id, DateTime date)
        {
            IBatteries batteries = new Batteries();
            List<BatteryDTO> list = (List<BatteryDTO>)batteries.FindAllById(id);
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            long ticks = date.Ticks - centuryBegin.Ticks;
            long secondsStart = ticks / 10000000;
            long secondsEnd = secondsStart + 86400;
            foreach (BatteryDTO b in list) {

                if (b.Time > secondsStart && b.Time <= secondsEnd) {

                    //ubaci u listu povratnih povratnih
                }

            }
            
        }

        public void GetConsumersProduction(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void GetSolarPanelProduction(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void GetUtilityProduction(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
