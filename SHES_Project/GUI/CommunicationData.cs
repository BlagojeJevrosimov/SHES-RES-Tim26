using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    [ExcludeFromCodeCoverage]
    public class CommunicationData
    {
        public static ISolarPanelGUI proxySP;
        public static IEVChargerGUI proxyEV;
        public static ISHESGUI proxySHES;
        public static IConsumerGUI proxyConsumer;
        public static IUtilityGUI proxyUtility;
    }
}
