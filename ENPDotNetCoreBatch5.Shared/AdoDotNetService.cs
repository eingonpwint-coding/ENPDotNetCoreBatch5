using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENPDotNetCoreBatch5.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;
        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Query(string query, params SqlParameterModel[] sqlParameters) 
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            SqlCommand sqlCommand = new SqlCommand(query, con);

            if(sqlParameters is not null)
            {
               foreach(var sqlParameter in sqlParameters)
                {
                    sqlCommand.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;
        }

        public int Execute(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            SqlCommand sqlCommand = new SqlCommand(query, con);

            if (sqlParameters is not null)
            {
                foreach (var sqlParameter in sqlParameters)
                {
                    sqlCommand.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
                }
            }

            var result = sqlCommand.ExecuteNonQuery();

            con.Close();

            return result;
        }

    }

    public class SqlParameterModel
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public SqlParameterModel() { }
        public SqlParameterModel(string name, object value)
        {
            Name = name;
            Value = value;
        }

    }
}
