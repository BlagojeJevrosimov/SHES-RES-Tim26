﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    public class SHESConsumer : ISHESConsumer
    {
        public static double energyConsumptioneBuffer;
        public void sendEnergyConsumption(double energyConsumption)
        {
            energyConsumptioneBuffer = energyConsumption;
        }

 
    }
}