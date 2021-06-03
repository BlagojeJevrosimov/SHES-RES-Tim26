using Common;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DAO
{
    public interface IUtility : ICRUDDao<Utility,string>
    {
        double GetCurrentPrice();
        IEnumerable<UtilityDTO> FindAll(int start, int end);
        void SaveProduction(Utility utility, int time);
    }
}
