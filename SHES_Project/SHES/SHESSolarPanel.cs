using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SHESSolarPanel : ISHESSolarPanel

    {
        public static double bufferPowerOutput;

        public void SendData(double PowerOutput)
        {
            bufferPowerOutput = PowerOutput;
        }
    }
}
