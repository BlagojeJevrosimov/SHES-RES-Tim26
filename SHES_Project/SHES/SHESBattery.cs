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
        public static double bufferCapacity;
        public static BatteryRezim bufferRegime = BatteryRezim.IDLE;


        public void SendData(double sentCapacity, BatteryRezim sentRegime)
        {
            bufferCapacity = sentCapacity;
            bufferRegime = sentRegime;
        }
    }
}
