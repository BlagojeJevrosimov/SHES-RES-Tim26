using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,InstanceContextMode =InstanceContextMode.Single)]
    public class ConsumerGUI : IConsumerGUI
    {
        public static Enums.ConsumerRezim[] rezimBuffer = new Enums.ConsumerRezim[ConsumerSHES.consumersList.Count];

        public void ChangeConsumerState(int id, Enums.ConsumerRezim rezim)
        {
            if(id < rezimBuffer.Count() && id >= 0)
            {
                rezimBuffer[id] = rezim;
                Trace.TraceInformation("Sent to Consumer: id-" + id + ", state-" + rezim.ToString());                
                //odmah se posalje shesu informacija ukupne snage consumera
                
                double total = 0;
                for (int i = 0; i < ConsumerSHES.consumersList.Count(); i++)
                {
                    if (rezimBuffer[i] == Enums.ConsumerRezim.ON)
                        total += ConsumerSHES.consumersList[i].EnergyConsumption;
                }

                SendTotal(total);
            }
            else
            {
                Trace.TraceError("Consumer id doesn't exist!");
            }
        }

        private void SendTotal(double total)
        {
            ChannelFactory<ISHESConsumer> channel = new ChannelFactory<ISHESConsumer>("ISHESConsumer");
            ISHESConsumer proxy = channel.CreateChannel();
            proxy.sendEnergyConsumption(total);
        }
    }
}
