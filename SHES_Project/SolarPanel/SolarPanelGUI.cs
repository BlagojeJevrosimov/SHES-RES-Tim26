using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SolarPanels
{
    [ServiceBehavior(ConcurrencyMode=ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class SolarPanelGUI : ISolarPanelGUI
    {
        public static double buffer = 0;

        public void ChangeSunIntensity(double sunIntensity)
        {
            if (sunIntensity >= 0 && sunIntensity <= 1)
                buffer = sunIntensity;
            else throw new ArgumentOutOfRangeException("Intenzitet sunca mora da bude izmedju 0 i 1");
        }

        
    }
}
