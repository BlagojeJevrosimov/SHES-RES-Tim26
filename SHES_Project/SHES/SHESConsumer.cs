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
    public class SHESConsumer : ISHESConsumer
    {
        public static double energyConsumptioneBuffer;
        public void sendEnergyConsumption(double energyConsumption)
        {
            if (energyConsumption >= 0)
                energyConsumptioneBuffer = energyConsumption;
            else
                throw new ArgumentOutOfRangeException("Potrosnja ne moze biti negativna vrednost!");
        }

 
    }
}
