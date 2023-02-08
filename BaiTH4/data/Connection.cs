using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BaiTH4.data
{
    internal class Connection
    {
        private SqlConnection sqlConnection;
        private static string string_connect = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\SOURCE\\C#\\BaiTH4\\BaiTH4\\Database1.mdf;Integrated Security=True";


        private SqlDataAdapter sqlDataAdapter;//chen du lieu vao bang data table va data set
        private SqlCommand sqlCommand;//thuc thi cau lenh truy van
        public DataTable table(string query) // lay bang du lieu
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(string_connect))
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            return dataTable;
        }

        //thuc thi cac cau lenh truy van

        public void Excute(string query)
        {
            using(SqlConnection sqlConnection = new SqlConnection(string_connect))
            {

                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

    }
}
