using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class CommunicationData
    {
        public static ISolarPanelGUI proxySP;
        public static IEVChargerGUI proxyEV;
        public static ISHESGUI proxySHES;
    }
}
