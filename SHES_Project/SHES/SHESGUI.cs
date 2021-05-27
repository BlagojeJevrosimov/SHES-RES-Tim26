using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class SHESGUI : ISHESGUI
    {
        public static int brojPanelaBuffer;
        public static double[] snagePanelaBuffer;
        public static int brojBaterijaBuffer;
        public static double[] snageBaterijaBuffer;
        public static double[] kapacitetiBaterijaBuffer;
        public static double snagaEVCBuffer;
        public static double cenaUtilityBuffer;
        public static int brojPotrosacaBuffer;
        public static double[] snagePotrosacaBuffer;

        public void Initialize(int brojPanela, double[] snagePanela, int brojBateija, 
            double[] snageBaterija, double[] kapacitetiBaterija, double snagaEVC, 
            double cenaUtility, int brojPotrosaca, double[] snagePotrosaca)
        {
            brojPanelaBuffer = brojPanela;
            snagePanelaBuffer = snagePanela;
            brojBaterijaBuffer = brojBateija;
            snageBaterijaBuffer = snageBaterija;
            kapacitetiBaterijaBuffer = kapacitetiBaterija;
            snagaEVCBuffer = snagaEVC;
            cenaUtilityBuffer = cenaUtility;
            brojPotrosacaBuffer = brojPotrosaca;
            snagePotrosacaBuffer = snagePotrosaca;

            Trace.TraceInformation("System initialized");
        }
        
    }
}
