using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class UtilityDTO
    {
        private double power;
        private int time;

        public UtilityDTO(double power, int time)
        {
            Power = power;
            Time = time;
        }

        public double Power { get => power; set => power = value; }
        public int Time { get => time; set => time = value; }
    }
}
