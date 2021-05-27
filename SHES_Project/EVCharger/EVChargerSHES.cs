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
    public class EVChargerSHES : IEVChargerSHES
    {
        public static Enums.BatteryRezim rezimBuffer; 
        public static Common.EVCharger EVCharger;

        public void InitializeEVCharger(Common.EVCharger evc)
        {
            rezimBuffer = Enums.BatteryRezim.IDLE;
            EVCharger = evc;
        }

        public void SendRegime(Enums.BatteryRezim rezim)
        {
            rezimBuffer = rezim;
        }
    }
}
