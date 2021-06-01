using Common;
using DatabaseLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DAO
{
    public interface ISolarPanels : ICRUDDao<SolarPanel,string>
    {
        IEnumerable<SolarPanelsDTO> FindAll(int start,int end);
        void SaveProduction(double power, int time);
    }
}
