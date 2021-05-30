using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using SHES_Project.DatabaseLayer;

namespace DatabaseLayer.DAO.Implementacije
{
    public class Batteries : IBatteries
    {
        public int Count()
        {
            string query = "select count(*) from batteries";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(Battery entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            string query = "delete from batteries";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsById(string id, IDbConnection connection)
        {
           
                string query = "select * from batteries where idb=:idb";

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idb", DbType.String);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "idb", id);
                    return command.ExecuteScalar() != null;
                }
            
        }

        public IEnumerable<Battery> FindAll()
        {
            
                string query = "select capacity, idb, power, state from batteries";
                List<Battery> batteryList = new List<Battery>();

                using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
                {
                    connection.Open();
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.Prepare();

                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Battery scene = new Battery(reader.GetDouble(0), reader.GetString(1),
                                    reader.GetDouble(2), (Enums.BatteryRezim)Enum.Parse(typeof(Enums.BatteryRezim), reader.GetString(3)));
                                batteryList.Add(scene);
                            }
                        }
                    }
                }
            return batteryList;
            
        }

        public IEnumerable<Battery> FindAllById(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Battery FindById(string id)
        {
            string query = "select capacity ,idb, power, state from batteries where idb = :idb";
            Battery battery = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idb", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idb", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            battery = new Battery(reader.GetDouble(0), reader.GetString(1),
                                reader.GetDouble(2), (Enums.BatteryRezim)Enum.Parse(typeof(Enums.BatteryRezim),reader.GetString(3)));
                        }
                    }
                }

            }

            return battery;
        }

        public void Save(Battery entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                Save(entity, connection);
            }
        }

        public void SaveAll(IEnumerable<Battery> entities)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                foreach (Battery entity in entities)
                {
                    Save(entity, connection);
                }

                transaction.Commit();
            }
        }
        public void Save(Battery entity, IDbConnection connection)
        {
            String insertSql = "insert into batteries (capacity,power,state,idb) values (:capacity, :power, :state, :idb)";
            String updateSql = "update batteries set capacity=:capacity, power = :power, state = :state where idb =:idb";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(entity.Id,connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "capacity", DbType.Double);
                ParameterUtil.AddParameter(command, "power", DbType.Double);
                ParameterUtil.AddParameter(command, "state", DbType.String);
                ParameterUtil.AddParameter(command, "idb", DbType.String);
                ParameterUtil.SetParameterValue(command, "idb", entity.Id);
                ParameterUtil.SetParameterValue(command, "state", entity.State);
                ParameterUtil.SetParameterValue(command, "power", entity.MaxPower);
                ParameterUtil.SetParameterValue(command, "capacity", entity.Capacity);              
                command.ExecuteNonQuery();
            }
        }

        public bool ExistsById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
