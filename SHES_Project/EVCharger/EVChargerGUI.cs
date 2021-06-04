using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EVCharger
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class EVChargerGUI : IEVChargerGUI
    {
        public static Enums.BatteryRezim rezimBuffer;
        public static bool plugBuffer = true;

        public void SendRegime(bool plug, Enums.BatteryRezim rezim)
        {
            rezimBuffer = rezim;
            plugBuffer = plug;
        }
    }
}
