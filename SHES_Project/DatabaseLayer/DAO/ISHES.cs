using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DAO.Implementacije
{
    public interface ISHES
    {
        void Save(string date, double total);
        List<DateTime> GetDates();
    }
}
