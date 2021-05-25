using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarPanels
{
    public class SolarPanelGUI : ISolarPanelGUI
    {
        public static double buffer = 0;

        public static List<Common.SolarPanel> solarPanels = new List<Common.SolarPanel>();

        public void ChangeSunIntensity(double sunIntensity)
        {
            if (sunIntensity >= 0 && sunIntensity <= 1)
                buffer = sunIntensity;
            else throw new ArgumentOutOfRangeException("Intenzitet sunca mora da bude izmedju 0 i 1");
        }

        public static void InitializeSolarPanels(int num, double[] maxPowers)
        {
            solarPanels = new List<Common.SolarPanel>(num);
            for (int i = 0; i < num; i++)
            {
                solarPanels.Add(new Common.SolarPanel() { Id = i.ToString(), MaxPower = maxPowers[i] });
            }
        }
    }
}
