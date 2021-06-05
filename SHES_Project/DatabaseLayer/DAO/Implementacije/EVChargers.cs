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
    public class EVChargers : IEVChargers
    {
        public int Count()
        {

            string query = "select count(*) from evcharger";

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

        public void Delete(EVCharger entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            string query = "delete from evcharger";

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
            string query = "select * from evcharger where idevc=:idevc";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "idevc", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idevc", id);
                return command.ExecuteScalar() != null;
            }
        }

        public bool ExistsById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EVCharger> FindAll()
        {
            string query = "select capacity, idevc, power, state, charge, connected from evcharger";
            List<EVCharger> eVChargers= new List<EVCharger>();

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
                            EVCharger evc = new EVCharger(reader.GetDouble(0), reader.GetString(1), reader.GetDouble(2),
                                (Enums.BatteryRezim)Enum.Parse(typeof(Enums.BatteryRezim), reader.GetString(3)), bool.Parse(reader.GetString(4)),
                                bool.Parse(reader.GetString(5)));
                                 
                            eVChargers.Add(evc);
                        }
                    }
                }
            }
            return eVChargers;
        }

        public IEnumerable<EVCharger> FindAllById(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public EVCharger FindById(string id)
        {
            string query = "select capacity, idevc, power, state, charge, connected from evcharger where idevc = :idevc";
            EVCharger evc = new EVCharger();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idevc", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idevc", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            evc = new EVCharger(reader.GetDouble(0), reader.GetString(1),reader.GetDouble(2), (Enums.BatteryRezim)Enum.Parse(typeof(Enums.BatteryRezim), reader.GetString(3)), bool.Parse(reader.GetString(4)),
                                bool.Parse(reader.GetString(5)));
                        }
                    }
                }

            }

            return evc;
        }

        public void Save(EVCharger entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                Save(entity, connection);
            }
        }

        public void SaveAll(IEnumerable<EVCharger> entities)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                foreach (EVCharger entity in entities)
                {
                    Save(entity, connection);
                }

                transaction.Commit();
            }
        }
        public void Save(EVCharger entity, IDbConnection connection)
        {
            String insertSql = "insert into evcharger (capacity,power,state,charge,connected,idevc) " +
                "values (:c , :p,:s, :charge,:connected, :idevc)";
            String updateSql = "update evcharger set capacity = :c, power = :p, state =:s" +
                "charge =:charge, connected=:connected where idevc =:idevc";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(entity.Id, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "c", DbType.Double);
                ParameterUtil.AddParameter(command, "p", DbType.Double);
                ParameterUtil.AddParameter(command, "s", DbType.String);
                ParameterUtil.AddParameter(command, "charge", DbType.String);
                ParameterUtil.AddParameter(command, "connected", DbType.String);
                ParameterUtil.AddParameter(command, "idevc", DbType.String);
                ParameterUtil.SetParameterValue(command, "idevc", entity.Id);
                ParameterUtil.SetParameterValue(command, "connected", entity.Connected);
                ParameterUtil.SetParameterValue(command, "charge", entity.Charge);
                ParameterUtil.SetParameterValue(command, "s", entity.State);
                ParameterUtil.SetParameterValue(command, "p", entity.MaxPower);
                ParameterUtil.SetParameterValue(command, "c", entity.Capacity);
                command.ExecuteNonQuery();
            }
        }
    }
}
