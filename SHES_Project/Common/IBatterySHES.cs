using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static Common.Enums;

namespace Common
{
    [ServiceContract]
    public interface IBatterySHES
    {
        [OperationContract]
        void InitializeBatteries(List<Common.Battery> batteriesRecieved);

        [OperationContract]
        void SendRegime(string id,BatteryRezim rezim);
    }
}
