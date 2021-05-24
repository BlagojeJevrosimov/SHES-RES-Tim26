using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class UtilitySHES : IUtilitySHES
    {
        public static Common.Utility utility;
        public void initializeUtility(Common.Utility util)
        {
            utility = util;
        }



        public double sendRequestforEnergy(double amount)
        {
            utility.Power = amount;
            if (amount <= 0)
            {
                return utility.Price;
            }
            else {

                return utility.Price;
            }
        }
    }
}
