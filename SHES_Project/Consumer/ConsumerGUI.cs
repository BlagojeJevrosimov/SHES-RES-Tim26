using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class ConsumerGUI : IConsumerGUI
    {
        //zameniti ovaj broj sa stvarnim brojem consumera nakon inicijalizacije
        public static Enums.ConsumerRezim[] rezimBuffer = new Enums.ConsumerRezim[5];

        public void ChangeConsumerState(int id, Enums.ConsumerRezim rezim)
        {
            if(id < rezimBuffer.Count() && id >= 0)
            {
                rezimBuffer[id] = rezim;
                Trace.TraceInformation("Sent to Consumer: id-" + id + ", state-" + rezim.ToString());
            }
            else
            {
                Trace.TraceError("Consumer id doesn't exist!");
            }
        }
    }
}
