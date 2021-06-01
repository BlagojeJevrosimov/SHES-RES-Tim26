using SHES_Project.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DAO.Implementacije
{
    public class SHES : ISHES
    {
        public void Save(string date, double total)
        {

            String insertSql = "insert into shes (total,datum) values (:total, :datum)";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = insertSql;
                    ParameterUtil.AddParameter(command, "total", DbType.Double);
                    ParameterUtil.AddParameter(command, "datum", DbType.String);
                    ParameterUtil.SetParameterValue(command, "datum", date);
                    ParameterUtil.SetParameterValue(command, "total", total);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
