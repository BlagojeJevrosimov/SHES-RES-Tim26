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
        //trebalo bi samo da prosledis dva boola i jedan statechanged da napravis u kome ce se slati shesu ta dva boola
        // taj deo cu ja da napravim, ti samo dva buffera za boolove i da ih prosledis
        public static Enums.BatteryRezim rezimBuffer;

        public void SendRegime(Enums.BatteryRezim rezim)
        {
            rezimBuffer = rezim;
        }
    }
}
