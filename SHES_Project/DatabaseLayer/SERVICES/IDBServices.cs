using Common;
using DatabaseLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.SERVICES
{
    [ServiceContract]
    public interface IDBServices
    {
        [OperationContract]
        List<SolarPanelsDTO> GetSolarPanelProduction(DateTime date);

        [OperationContract]
        List<BatteryDTO> GetBatteryProduction(string id,DateTime date);

        [OperationContract]
        List<UtilityDTO> GetUtilityProduction(DateTime date);

        [OperationContract]
        List<ConsumersDTO> GetConsumersProduction(DateTime date);

        [OperationContract]
        void SaveBatteries(IEnumerable<Battery> list,int time);

        [OperationContract]
        void SaveSolarPanels(IEnumerable<SolarPanel> list);

        [OperationContract]
        void SaveSolarPanelProduction(double power, int time);

        [OperationContract]
        void SaveConsumers(IEnumerable<Consumer> list,int time);

        [OperationContract]
        void SaveUtility(Utility utility,int time);

        [OperationContract]
        void SaveEVCharger(EVCharger evc);

        [OperationContract]
        void SaveTotalExpenditure(DateTime date, int total);

        [OperationContract]
        List<Battery> GetBatteries();

        [OperationContract]
        List<Consumer> GetConsumers();

        [OperationContract]
        EVCharger GetEVCharger();

        [OperationContract]
        List<SolarPanel> GetSolarPanels();

        [OperationContract]
        double GetCurrentPrice();

       // [OperationContract]
       // void GetEVCharger();

    }
}
