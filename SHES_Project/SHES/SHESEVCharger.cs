using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{   [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,InstanceContextMode = InstanceContextMode.Single)]
    public class SHESEVCharger : ISHESEVCharger
    {
        public static bool rezimBuffer;
        public static bool plugBuffer;

        public void SendRegime(bool plugIn, bool charge)
        {
            rezimBuffer = charge;
            plugBuffer = plugIn;
        }
    }
}
