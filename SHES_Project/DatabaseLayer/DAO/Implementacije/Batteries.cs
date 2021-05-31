﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DatabaseLayer.DTO;
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

        public void Delete(BatteryDTO entity)
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

        public IEnumerable<BatteryDTO> FindAll()
        {
            
                string query = "select capacity, idb, power, state, time from batteries";
                List<BatteryDTO> batteryList = new List<BatteryDTO>();

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
                                BatteryDTO b = new BatteryDTO(reader.GetDouble(0), reader.GetString(1),
                                    reader.GetDouble(2), (Enums.BatteryRezim)Enum.Parse(typeof(Enums.BatteryRezim), reader.GetString(3)),reader.GetInt32(4));
                                batteryList.Add(b);
                            }
                        }
                    }
                }
            return batteryList;
            
        }

        public IEnumerable<BatteryDTO> FindAllById(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<BatteryDTO> FindAllById(string id)
        {
            string query = "select capacity, idb, power, state, time from batteries where idb =:idb";
            List<BatteryDTO> batteryList = new List<BatteryDTO>();

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
                        while (reader.Read())
                        {
                            BatteryDTO b = new BatteryDTO(reader.GetDouble(0), reader.GetString(1),
                                reader.GetDouble(2), (Enums.BatteryRezim)Enum.Parse(typeof(Enums.BatteryRezim), reader.GetString(3)), reader.GetInt32(4));
                            batteryList.Add(b);
                        }
                    }
                }
            }
            return batteryList;
        }
        public BatteryDTO FindById(string id)
        {
            string query = "select capacity ,idb, power, state, time from batteries where idb = :idb";
            BatteryDTO battery = null;

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
                            battery = new BatteryDTO(reader.GetDouble(0), reader.GetString(1),
                                reader.GetDouble(2), (Enums.BatteryRezim)Enum.Parse(typeof(Enums.BatteryRezim),reader.GetString(3)),reader.GetInt32(4));
                        }
                    }
                }

            }

            return battery;
        }

        public void Save(BatteryDTO entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                Save(entity, connection);
            }
        }

        public void SaveAll(IEnumerable<BatteryDTO> entities)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                foreach (BatteryDTO entity in entities)
                {
                    Save(entity, connection);
                }

                transaction.Commit();
            }
        }
        public void Save(BatteryDTO entity, IDbConnection connection)
        {
            String insertSql = "insert into batteries (capacity,power,state,time,idb) values (:capacity, :power, :state,:time, :idb)";
           // String updateSql = "update batteries set capacity=:capacity, power = :power, state = :state, time=:time where idb =:idb";

            using (IDbCommand command = connection.CreateCommand())
            {
                // command.CommandText = ExistsById(entity.Id,connection) ? updateSql : insertSql;
                command.CommandText = insertSql;
                ParameterUtil.AddParameter(command, "capacity", DbType.Double);
                ParameterUtil.AddParameter(command, "power", DbType.Double);
                ParameterUtil.AddParameter(command, "state", DbType.String);
                ParameterUtil.AddParameter(command, "time", DbType.Int32);
                ParameterUtil.AddParameter(command, "idb", DbType.String);
                ParameterUtil.SetParameterValue(command, "idb", entity.Id);
                ParameterUtil.SetParameterValue(command, "time", entity.Time);
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
