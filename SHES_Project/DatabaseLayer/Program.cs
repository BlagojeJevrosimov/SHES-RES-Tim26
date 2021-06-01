using Common;
using DatabaseLayer.DAO;
using DatabaseLayer.DAO.Implementacije;
using DatabaseLayer.DTO;
using DatabaseLayer.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(DBServices))) {
                
                host.Open();
                while (true) ;
               
            }
        }
    }
}
