using Common;
using DatabaseLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DAO
{
    public interface IConsumers : ICRUDDao<Consumer,string>
    {
        IEnumerable<ConsumersDTO> FindAll(int start, int end);
        void SaveAll(IEnumerable<Consumer> entities, int time);
        void Save(Consumer entity, int time);
        List<Tuple<string, int>> GetIdsForInit();
        Consumer FindById(string id, int time);
    }
}
