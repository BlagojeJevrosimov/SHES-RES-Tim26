using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EVCharger
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            Enums.BatteryRezim rezimSHES = Enums.BatteryRezim.PRAZNJENJE;
            Enums.BatteryRezim rezimGUI = Enums.BatteryRezim.PRAZNJENJE;
            bool plug = false;

            ServiceHost host = new ServiceHost(typeof(EVChargerSHES));
            host.Open();
            ServiceHost host2 = new ServiceHost(typeof(EVChargerGUI));
            host2.Open();

            ChannelFactory<ISHESEVCharger> channelSHES = new ChannelFactory<ISHESEVCharger>("ISHESEVCharger");
            ISHESEVCharger proxySHES = channelSHES.CreateChannel();

            while (true)
                {
                if (EVChargerSHES.initialized)
                {
                    rezimSHES = EVChargerSHES.rezimBuffer;
                    rezimGUI = EVChargerGUI.rezimBuffer;

                    if (plug != EVChargerGUI.plugBuffer || rezimSHES != EVChargerGUI.rezimBuffer)
                    {
                        plug = EVChargerGUI.plugBuffer;
                        if (rezimGUI == Enums.BatteryRezim.PRAZNJENJE)
                            proxySHES.SendRegime(plug, false);
                        else if (rezimGUI == Enums.BatteryRezim.PUNJENJE)
                            proxySHES.SendRegime(plug, true);
                    }

                    Thread.Sleep(300);
                }
            }        
        }
    }
}
