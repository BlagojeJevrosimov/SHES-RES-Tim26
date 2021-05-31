using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DTO
{
   public class BatteryDTO : Battery
    {
        private int time;

        public BatteryDTO(double capacity, string id, double maxPower, Enums.BatteryRezim state,int time):base(capacity,id,maxPower,state)
        {
            Time = time;
        }

        public int Time { get => time; set => time = value; }
    }
}
