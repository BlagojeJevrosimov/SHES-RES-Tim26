using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class UtilityGUI : IUtilityGUI
    {
        public static double bufferPrice = 0;

        public void SendPrice(double price)
        {
            if (price >= 0)
                bufferPrice = price;
            else
                throw new ArgumentOutOfRangeException("Cena elektrodistribucije je negativna!");
        }
    }
}
