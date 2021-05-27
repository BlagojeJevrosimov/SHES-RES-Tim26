using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SolarPanel
{
    public class SolarPanelSHES : ISolarPanelSHES
    {
        public static List<Common.SolarPanel> solarPanels = new List<Common.SolarPanel>();
        public void InitializeSolarPanels(int num, double[] maxPowers)
        {
           solarPanels = new List<Common.SolarPanel>(num);
            for (int i = 0; i < num; i++)
            {
                solarPanels.Add(new Common.SolarPanel() { Id = i.ToString(), MaxPower = maxPowers[i] });
            }
        }
    }
}
