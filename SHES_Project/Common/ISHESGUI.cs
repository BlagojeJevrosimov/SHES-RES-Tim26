using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface ISHESGUI
    {
        [OperationContract]
        void Initialize(int brojPanela, double[] snagePanela, 
            int brojBateija, double[] snageBaterija, double[] kapacitetiBaterija, double snagaEVC, 
            double cenaUtility, int brojPotrosaca, double[] snagePotrosaca);
    }
}
