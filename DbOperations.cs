using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopproject
{
    class DbOperation
    {
        SqlConnection connection;
        SqlCommand sqlCmd;
        SqlDataAdapter sqlDa;
        string cnnStr;
        public DbOperation()
        {
            cnnStr = "Data Source=DESKTOP-OM5PRLQ\\SQLEXPRESS;Initial Catalog=oopproje;Integrated Security=true;";
        }
        public DbOperation(string connStr)
        {
            cnnStr = connStr;
        }
        public DataTable SelectTable(string cmdStr)
        {
            connection = new SqlConnection(cnnStr);
            sqlCmd = new SqlCommand(cmdStr, connection);
            sqlDa = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            try
            {
                sqlDa.Fill(dt);
            }
            catch
            {

            }
            return dt;
        }

        public int runCommand(string cmdStr)
        {
            int numberOfRows = 0;
            connection = new SqlConnection(cnnStr);
            sqlCmd = new SqlCommand(cmdStr, connection);
            try
            {
                connection.Open();
                numberOfRows = sqlCmd.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                numberOfRows = -1;
                connection.Close();
            }
            return numberOfRows;

        }
    }
}
