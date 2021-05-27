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
    public class EVChargerGUI : IEVChargerGUI
    {
        //trebalo bi samo da prosledis dva boola i jedan statechanged da napravis u kome ce se slati shesu ta dva boola
        // taj deo cu ja da napravim, ti samo dva buffera za boolove i da ih prosledis
        public static Enums.BatteryRezim rezimBuffer;
        public static bool plugBuffer;

        public void SendRegime(bool plug, Enums.BatteryRezim rezim)
        {
            rezimBuffer = rezim;
            plugBuffer = plug;
        }
    }
}
