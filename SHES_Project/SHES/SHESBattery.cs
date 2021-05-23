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
        public static Rezim bufferRegime = Rezim.Idle;


        public void SendData(double sentCapacity, Rezim sentRegime)
        {
            bufferCapacity = sentCapacity;
            bufferRegime = sentRegime;
        }
    }
}
