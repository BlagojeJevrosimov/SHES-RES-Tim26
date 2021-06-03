using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ConsumerSHES : IConsumerSHES
    {
        public static List<Common.Consumer> consumersList = new List<Common.Consumer>();

        public void InitializeConsumers(List<Common.Consumer> consumers)
        {
            if (consumers == null)
                throw new ArgumentNullException("Null prosledjen u Consumera");

            consumersList = consumers;
            int i = 0;
            ConsumerGUI.rezimBuffer = new Enums.ConsumerRezim[consumers.Count];
            foreach (var c in consumers)
            {
                ConsumerGUI.total += c.EnergyConsumption;
                ConsumerGUI.rezimBuffer[i++] = c.Rezim;
            }
            ConsumerGUI.changed = true;
        }
    }
}
