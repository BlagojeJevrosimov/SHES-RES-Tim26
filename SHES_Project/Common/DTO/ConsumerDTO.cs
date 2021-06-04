using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class ConsumersDTO
    {
        private int time;
        private double power;

        //OVAJ OBRISATI
        public ConsumersDTO(int time, double power)
        {
            Time = time;
            Power = power;
        }

        public int Time { get => time; set => time = value; }
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
