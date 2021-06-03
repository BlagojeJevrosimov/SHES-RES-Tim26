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

        public ConsumersDTO(int time, double power)
        {
            Time = time;
            Power = power;
        }

        public int Time { get => time; set => time = value; }
        public double Power { get => power; set => power = value; }
    }
}
