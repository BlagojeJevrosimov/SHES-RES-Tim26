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
        public List<DateTime> GetDates() {

            String Sql = "select datum from shes";
            List<DateTime> list = new List<DateTime>(); 
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = Sql;
                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string date = reader.GetString(0);
                            string[] temp = date.Split('/');
                            int day = int.Parse(temp[0]);
                            int month = int.Parse(temp[1]);
                            int year = int.Parse(temp[2]);
                            DateTime d = new DateTime(year,month,day);
                            list.Add(d);
                        }
                    }
                }
            }
            return list;

        }
    }
}
