using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class UtilitySHES : IUtilitySHES
    {
        public static Common.Utility utility;

        public double getPrice()
        {
            return utility.Price;
        }

        public void initializeUtility(Common.Utility util)
        {
            if (util == null) throw new ArgumentNullException("Null prosledjen u eletrodistribuciju");
            if (util.Price < 0) throw new ArgumentOutOfRangeException("Cena struje ne moze biti manja od nula");
            utility = util;
        }



        public void sendRequestforEnergy(double amount)
        {
            utility.Power = amount;
          
        }
    }
}
