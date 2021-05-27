using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class ConsumerSHES : IConsumerSHES
    {
        public static List<Common.Consumer> consumersList;
        public void InitializeConsumers(List<Common.Consumer> consumers)
        {
            if (consumers == null)
                throw new ArgumentNullException("Null prosledjen u Consumera");

            consumersList = consumers;
        }
    }
}
