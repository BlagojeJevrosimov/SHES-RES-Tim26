using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{   [ServiceContract]
    public interface IUtilitySHES
    {
        [OperationContract]
        void sendRequestforEnergy(double amount);
        [OperationContract]
        void initializeUtility(Common.Utility util);
        [OperationContract]
        double getPrice();
    }
}
