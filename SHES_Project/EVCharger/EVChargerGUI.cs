using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVCharger
{
    public class EVChargerGUI : IEVChargerGUI
    {
        public static Enums.BatteryRezim rezimBuffer;

        public void SendRegime(Enums.BatteryRezim rezim)
        {
            rezimBuffer = rezim;
        }
    }
}
