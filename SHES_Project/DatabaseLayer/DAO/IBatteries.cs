using Common;
using DatabaseLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DAO
{
    public interface IBatteries: ICRUDDao<BatteryDTO,string>
    {
        IEnumerable<BatteryDTO> FindAllById(string id);
    }
}
