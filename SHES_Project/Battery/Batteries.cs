using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enums;

namespace Battery
{
    public class Batteries : IBatterySHES
    {
        public static BatteryRezim bufferRezim = BatteryRezim.IDLE;
        public static List<Common.Battery> batteries = new List<Common.Battery>();

        public static void InitializeBatteries(int num, double[] maxPowers)
        {
            if (num > 0)
            {
                batteries = new List<Common.Battery>();
                bufferRezim = BatteryRezim.IDLE;
                //sta je kapacitet
                //da li to dokle je napunjena u intervalu od 0 do maxPower
                //ili je capacity maksimalni kapacitet baterije
                //a maxPower je sta onda?
                for (int i = 0; i < num; i++)
                    batteries.Add(new Common.Battery(i.ToString(), maxPowers[i], 0));
            }
        }

        public void SendRegime(BatteryRezim rezim)
        {
            bufferRezim = rezim;
        }
    }
}
