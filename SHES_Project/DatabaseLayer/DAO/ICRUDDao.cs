using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DAO
{
    public interface ICRUDDao<T, ID>
    {
        int Count();

        void Delete(T entity);

        void DeleteAll();

        void DeleteById(ID id);

        bool ExistsById(ID id);

        IEnumerable<T> FindAll();

        IEnumerable<T> FindAllById(IEnumerable<ID> ids);

        T FindById(ID id);

        void Save(T entity);

        void SaveAll(IEnumerable<T> entities);
    }
}
