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
        void InitializeBatteries(int num, double[] maxPowers);

        [OperationContract]
        void SendRegime(Rezim rezim);
    }
}
