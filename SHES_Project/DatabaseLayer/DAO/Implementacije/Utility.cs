﻿using Common;
using Oracle.ManagedDataAccess.Client;
using SHES_Project.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DAO.Implementacije
{
    public class Utility : IUtility
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(Common.Utility entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsById(string id,IDbConnection connection)
        {
            string query = "select * from utility where idu=:idu";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "idu", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idu", id);
                return command.ExecuteScalar() != null;
            }
        }

        public bool ExistsById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Common.Utility> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Common.Utility> FindAllById(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Common.Utility FindById(string id)
        {
            throw new NotImplementedException();
        }

        public double GetCurrentPrice()
        {
            string query = "select price from utility";
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection()) {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand()) {

                    command.CommandText = query;
                    command.Prepare();

                    return Convert.ToDouble(command.ExecuteScalar());
                }


            }
        }

        public void Save(Common.Utility entity)
        {
            
                using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
                {
                    String insertSql = "insert into utility (price,idu) values (:price, :idu)";
                    String updateSql = "update utility set price= :price where idu =:idu";
                    connection.Open();
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = ExistsById("1", connection) ? updateSql : insertSql;
                        ParameterUtil.AddParameter(command, "price", DbType.Double);
                        ParameterUtil.AddParameter(command, "idu", DbType.String);
                        ParameterUtil.SetParameterValue(command, "idu", "1");
                        ParameterUtil.SetParameterValue(command, "price", entity.Price);
                        command.ExecuteNonQuery();
                    }
                }
            
        }

        public void SaveAll(IEnumerable<Common.Utility> entities)
        {
            throw new NotImplementedException();
        }
    }
}
