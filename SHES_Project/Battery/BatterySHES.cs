using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static Common.Enums;

namespace Battery
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]

    public class BatterySHES : IBatterySHES
    {
        public static Dictionary<string, Enums.BatteryRezim> bufferRezim = new Dictionary<string, BatteryRezim>();
        public static List<Common.Battery> batteries = new List<Common.Battery>();

        public void InitializeBatteries(int num, double[] maxPowers)
        {
            if (num != maxPowers.Length) throw new ArgumentOutOfRangeException("Broj baterija i njihovih snaga se ne poklapaju.");

            if (num > 0)
            {
                batteries = new List<Common.Battery>();

                for (int i = 0; i < num; i++)
                {
                    bufferRezim[i.ToString()] = Enums.BatteryRezim.IDLE;
                    batteries.Add(new Common.Battery(0, i.ToString(), maxPowers[i], Enums.BatteryRezim.IDLE));
                }
            }
            else throw new ArgumentOutOfRangeException("Broj baterija ne moze biti negativan.");
            
        }

        public void SendRegime(string id,BatteryRezim rezim)
        {
            if (id == null)
                throw new ArgumentNullException("Null prosledjen u bateriju");
            bufferRezim[id] = rezim;
        }
    }
}
