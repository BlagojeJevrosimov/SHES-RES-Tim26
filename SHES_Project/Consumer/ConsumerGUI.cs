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

namespace Consumer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,InstanceContextMode =InstanceContextMode.Single)]
    public class ConsumerGUI : IConsumerGUI
    {
        public static Enums.ConsumerRezim[] rezimBuffer = new Enums.ConsumerRezim[ConsumerSHES.consumersList.Count];
        private double total;

        public void ChangeConsumerState(int id, Enums.ConsumerRezim rezim)
        {
            if(id >= 0)
            {
                rezimBuffer = new Enums.ConsumerRezim[ConsumerSHES.consumersList.Count];
            }
            else
            {
                throw new ArgumentOutOfRangeException("Negativan id!");
            }

            if (id < rezimBuffer.Count())
            {
                rezimBuffer[id] = rezim;
                Trace.TraceInformation("Sent to Consumer: id-" + id + ", state-" + rezim.ToString());                
                //odmah se posalje shesu informacija ukupne snage consumera
                
                total = 0;
                for (int i = 0; i < ConsumerSHES.consumersList.Count(); i++)
                {
                    if (rezimBuffer[i] == Enums.ConsumerRezim.ON)
                        total += ConsumerSHES.consumersList[i].EnergyConsumption;
                }

                Thread t = new Thread(SendTotal);
                t.Start();

            }
            else
            {
                throw new ArgumentOutOfRangeException("Nepostojeci id!");
            }
        }

        [ExcludeFromCodeCoverage]    
        private void SendTotal()
        {
            ChannelFactory<ISHESConsumer> channel = new ChannelFactory<ISHESConsumer>("ISHESConsumer");
            ISHESConsumer proxy = channel.CreateChannel();
            proxy.sendEnergyConsumption(total);
        }

        public double ReturnTotal()
        {
            return total;
        }
    }
}
