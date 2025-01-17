﻿using Common;
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
        public static bool initialized = false;
        public void InitializeEVCharger(Common.EVCharger evc)
        {
            if (evc != null)
            {
                rezimBuffer = Enums.BatteryRezim.PRAZNJENJE;
                EVCharger = evc;
                initialized = true;
            }
            else
            {
                throw new ArgumentNullException("EVCharger ne sme biti null!");
            }

        }

        public void SendRegime(Enums.BatteryRezim rezim)
        {
            rezimBuffer = rezim;
        }
    }
}
