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
        public static List<Consumer> consumers;
        public void sendEnergyConsumption(double energyConsumption,List<Consumer> c)
        {
            if (energyConsumption >= 0)
                energyConsumptioneBuffer = energyConsumption;
            else
                throw new ArgumentOutOfRangeException("Potrosnja ne moze biti negativna vrednost!");

            if (c != null)
            {
                consumers = c;
            }
            else
                throw new ArgumentNullException("Nije poslata lista conusmera iz komponente");
        }

 
    }
}
