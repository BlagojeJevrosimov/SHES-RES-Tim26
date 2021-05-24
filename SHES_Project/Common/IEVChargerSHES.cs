using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
   public interface IEVChargerSHES
    {
        [OperationContract]
        void InitializeEVCharger(Common.EVCharger evc);

        [OperationContract]
        void SendRegime(Enums.Rezim rezim);
    }
}
