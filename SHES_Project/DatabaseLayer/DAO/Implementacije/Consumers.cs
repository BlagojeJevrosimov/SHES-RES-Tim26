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
    public class Consumers : IConsumers
    {
        public int Count()
        {
            string query = "select count(*) from consumers";

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

        public void Delete(Consumer entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            string query = "delete from consumers";

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
            string query = "select * from consumers where idc=:idc";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "idc", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idc", id);
                return command.ExecuteScalar() != null;
            }
        }

        public bool ExistsById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Consumer> FindAll()
        {
            string query = "select power, idc, state from consumers";
            List<Consumer> consumerList = new List<Consumer>();

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
                            Consumer cons = new Consumer(reader.GetDouble(0), reader.GetString(1),
                                 (Enums.ConsumerRezim)Enum.Parse(typeof(Enums.ConsumerRezim), reader.GetString(3)));
                            consumerList.Add(cons);
                        }
                    }
                }
            }
            return consumerList;
        }

        public IEnumerable<Consumer> FindAllById(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Consumer FindById(string id)
        {
            string query = "select power,idc,state from consumers where idc = :idc";
           Consumer consumer= null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idc", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idc", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            consumer = new Consumer(reader.GetDouble(0), reader.GetString(1),
                                (Enums.ConsumerRezim)Enum.Parse(typeof(Enums.ConsumerRezim), reader.GetString(3)));
                        }
                    }
                }

            }

            return consumer;
        }

        public void Save(Consumer entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                Save(entity, connection);
            }
        }

        public void SaveAll(IEnumerable<Consumer> entities)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                foreach (Consumer entity in entities)
                {
                    Save(entity, connection);
                }

                transaction.Commit();
            }
        }
        public void Save(Consumer entity, IDbConnection connection)
        {
            String insertSql = "insert into consumers (power,state,idc) values (:power, :state, :idc)";
            String updateSql = "update consumers set  power = :power, state = :state where idc =:idc";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(entity.Id, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "power", DbType.Double);
                ParameterUtil.AddParameter(command, "state", DbType.String);
                ParameterUtil.AddParameter(command, "idc", DbType.String);
                ParameterUtil.SetParameterValue(command, "idc", entity.Id);
                ParameterUtil.SetParameterValue(command, "state", entity.Rezim);
                ParameterUtil.SetParameterValue(command, "power", entity.EnergyConsumption);
                command.ExecuteNonQuery();
            }
        }
    }
}
