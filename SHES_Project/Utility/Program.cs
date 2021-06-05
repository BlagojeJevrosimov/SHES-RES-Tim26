using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            
            ServiceHost host = new ServiceHost(typeof(UtilitySHES));
            host.Open();

            ServiceHost host2 = new ServiceHost(typeof(UtilityGUI));
            host2.Open();

            while (true) {

                UtilitySHES.utility.Price = UtilityGUI.bufferPrice;
                Thread.Sleep(500);
            }
        }

    }
}
