using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static Common.Enums;

namespace SHES
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,InstanceContextMode = InstanceContextMode.Single)]
    public class SHESBattery : ISHESBattery
    {
        public static Dictionary<string, double> bufferCapacities = new Dictionary<string, double>();
        public static Dictionary<string, Enums.BatteryRezim> bufferRezimi = new Dictionary<string, BatteryRezim>();


        public void SendData(string id, double sentCapacity, BatteryRezim sentRegime)
        {
            if (id == null)
                throw new ArgumentNullException("Id ne moze biti null!");
            else if (sentCapacity < 0)
                throw new ArgumentOutOfRangeException("Kapacitet ne moze biti negativan!");
            else
            {
                bufferRezimi[id] = sentRegime;
                bufferCapacities[id] = sentCapacity;
            }
        }
    }
}
