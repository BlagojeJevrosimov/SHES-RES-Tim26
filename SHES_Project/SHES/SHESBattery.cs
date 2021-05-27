using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enums;

namespace SHES
{
    public class SHESBattery : ISHESBattery
    {
        public static Dictionary<string, double> bufferCapacities;
        public static Dictionary<string, Enums.BatteryRezim> bufferRezimi;


        public void SendData(string id, double sentCapacity, BatteryRezim sentRegime)
        {
            bufferRezimi[id] = sentRegime;
            bufferCapacities[id] = sentCapacity;
        }
    }
}
