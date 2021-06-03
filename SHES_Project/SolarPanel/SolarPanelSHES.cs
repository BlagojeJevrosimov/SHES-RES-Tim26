using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SolarPanels
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class SolarPanelSHES : ISolarPanelSHES
    {
        public static List<Common.SolarPanel> solarPanels = new List<Common.SolarPanel>();
        public void InitializeSolarPanels(int num, double[] maxPowers)
        {
            if (num < 0)
                throw new ArgumentOutOfRangeException("Broj solarnih panela ne sme biti negativan!");
            else if(num != maxPowers.Count())
            {
                throw new ArgumentOutOfRangeException("Pogresan broj solarnih panela/njihovih snaga!");
            }
            else
            {
                bool flag = true;
                foreach(double br in maxPowers)
                {
                    if(br < 0)
                    {
                        flag = false;
                        throw new ArgumentOutOfRangeException("Snage ne smeju biti negativne!");
                    }
                }
                if (flag)
                {
                    solarPanels = new List<Common.SolarPanel>(num);
                    for (int i = 0; i < num; i++)
                    {
                        solarPanels.Add(new Common.SolarPanel() { Id = i.ToString(), MaxPower = maxPowers[i] });
                    }
                }
                
            }
           
        }
    }
}
