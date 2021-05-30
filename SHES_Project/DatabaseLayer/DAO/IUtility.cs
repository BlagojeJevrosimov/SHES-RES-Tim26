using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DAO
{
    public interface IUtility 
    {
        int GetCurrentPrice();

        void SavePrice(double cena);
    }
}
