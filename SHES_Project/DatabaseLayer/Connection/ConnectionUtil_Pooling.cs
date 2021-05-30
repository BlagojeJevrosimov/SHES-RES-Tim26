using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess.Client;

namespace SHES_Project.DatabaseLayer
{
    public class ConnectionUtil_Pooling : IDisposable
    {
        private static IDbConnection instance = null;

        public static IDbConnection GetConnection()
        {
            if (instance == null || instance.State == System.Data.ConnectionState.Closed)
            {
                OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder();
                ocsb.DataSource = "//localhost:1521/xe";
                ocsb.UserID = "blagoje";
                ocsb.Password = "ftn";
                ocsb.Pooling = true;
                ocsb.MinPoolSize = 1;
                ocsb.MaxPoolSize = 10;
                ocsb.IncrPoolSize = 3;
                ocsb.ConnectionLifeTime = 5;
                ocsb.ConnectionTimeout = 30;
                instance = new OracleConnection(ocsb.ConnectionString);

            }
            return instance;
        }

        public void Dispose()
        {
            if (instance != null)
            {
                instance.Close();
                instance.Dispose();
            }

        }
    }
}
