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
    public class SolarPanels : ISolarPanels
    {
        public int Count()
        {
            string query = "select count(*) from solarpanels";

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

        public void Delete(SolarPanel entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            string query = "delete from solarpanels";

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

        public bool ExistsById(string id,IDbConnection connection)
        {
            string query = "select * from solarpanels where idsp=:idsp";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "idsp", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idsp", id);
                return command.ExecuteScalar() != null;
            }
        }

        public IEnumerable<SolarPanel> FindAll()
        {
            string query = "select  idsp, power from solarpanels";
            List<SolarPanel> solarPanelList= new List<SolarPanel>();

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
                            SolarPanel sp = new SolarPanel(reader.GetString(0),
                                 reader.GetDouble(1));
                            solarPanelList.Add(sp);
                        }
                    }
                }
            }
            return solarPanelList;
        }

        public IEnumerable<SolarPanel> FindAllById(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public SolarPanel FindById(string id)
        {
            string query = "select idsp, power from solarpanels where idsp = :idsp";
            SolarPanel sp = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idsp", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idsp", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sp = new SolarPanel(reader.GetString(0), reader.GetDouble(1));
                        }
                    }
                }

            }

            return sp;
        }

        public void Save(SolarPanel entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                Save(entity, connection);
            }
        }

        public void SaveAll(IEnumerable<SolarPanel> entities)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                foreach (SolarPanel entity in entities)
                {
                    Save(entity, connection);
                }

                transaction.Commit();
            }
        }
        public void Save(SolarPanel entity, IDbConnection connection)
        {
            String insertSql = "insert into solarpanels(power,idsp) values (:power, :idsp)";
            String updateSql = "update consumers set  power = :power where idsp =:idsp";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(entity.Id, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "power", DbType.Double);
                ParameterUtil.AddParameter(command, "idsp", DbType.String);
                ParameterUtil.SetParameterValue(command, "idsp", entity.Id);
                ParameterUtil.SetParameterValue(command, "power", entity.MaxPower);
                command.ExecuteNonQuery();
            }
        }

        public bool ExistsById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
