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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BatterySHES : IBatterySHES
    {
        public static Dictionary<string, Enums.BatteryRezim> bufferRezim = new Dictionary<string, BatteryRezim>();
        public static List<Common.Battery> batteries = new List<Common.Battery>();
        public static bool initialized = false;

        public void InitializeBatteries(List<Common.Battery> batteriesRecieved)
        {
            //if (num != batteriesRecieved.Count)
            //    throw new ArgumentOutOfRangeException("Broj baterija i njihovih snaga se ne poklapaju.");

            if (batteriesRecieved != null)
            {
                int num = batteriesRecieved.Count();

                batteries = batteriesRecieved;

                for (int i = 0; i < num; i++)
                {
                    bufferRezim[batteriesRecieved[i].Id] = Enums.BatteryRezim.PUNJENJE;
                }
            }
            else throw new ArgumentNullException("Baterije su null!");
            initialized = true;
        }

        public void SendRegime(string id, BatteryRezim rezim)
        {
            if (id == null)
                throw new ArgumentNullException("Id ne moze biti null!");

            if (!bufferRezim.Keys.Contains(id))
                throw new ArgumentException("Nepostojeci id!");
            else
                bufferRezim[id] = rezim;
        }
    }
}
