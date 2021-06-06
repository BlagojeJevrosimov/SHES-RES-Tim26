using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    [DataContract]
    public class ConsumersDTO
    {
        private int time;
        private double power;

        public ConsumersDTO() { }

        public ConsumersDTO(int time, double power)
        {
            if (time < 0)
                throw new ArgumentOutOfRangeException("Vreme ne sme biti negativno!");
            else
            {
                Time = time;
                Power = power;
            }
        }
        [DataMember]
        public int Time { get => time; set => time = value; }
        [DataMember]
        public double Power { get => power; set => power = value; }

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
