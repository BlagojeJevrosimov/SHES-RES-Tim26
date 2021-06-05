using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Common.DTO
{
    [DataContract]
    public class BatteryDTO : Battery
    {  
        private int time;

        public BatteryDTO(double capacity, string id, double maxPower, Enums.BatteryRezim state, int time) : base(capacity, id, maxPower, state)
        {
            Time = time;
        }
        [DataMember]
        public int Time { get => time; set => time = value; }

        public DateTime TimeAsDT
        {
            get { return ToDateTime(Time); }
        }

        private DateTime ToDateTime(int time)
        {
            DateTime ret;
            DateTime centuryBegin = new DateTime(2020, 1, 1);
            
            //time je u sekundama, ovim dobijamo ukupan broj otkucaja
            var elapsedSpan = TimeSpan.TicksPerSecond * time;
            ret = new DateTime(centuryBegin.Ticks + elapsedSpan);

            return ret;
        }

    }
}
