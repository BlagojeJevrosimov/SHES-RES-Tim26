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
            Thread SHESServer = new Thread(ShesServer);
            SHESServer.Start();

            Thread GUIServer = new Thread(GuiServer);
            GUIServer.Start();
            
        }

        private static void ShesServer()
        {
            using (ServiceHost host = new ServiceHost(typeof(UtilitySHES)))
            {

                host.Open();

                while (true) ;
            }
        }

        private static void GuiServer()
        {
            using (ServiceHost host = new ServiceHost(typeof(UtilityGUI)))
            {

                host.Open();

                while (true) ;
            }
        }
    }
}
