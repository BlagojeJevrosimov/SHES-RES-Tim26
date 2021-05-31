using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.SERVICES
{
    public interface IDBServices
    {
        void GetSolarPanelProduction(DateTime date);
        void GetBatteryProduction(string id,DateTime date);
        void GetUtilityProduction(DateTime date);
        void GetConsumersProduction(DateTime date);
    }
}
