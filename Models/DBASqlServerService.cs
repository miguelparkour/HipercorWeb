using HipercorWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HipercorWeb.Models
{
    public class DBASqlServerService : IDataBaseAccess

    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HipercorWeb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task<bool> login(Cliente client)
        {

            bool result = false;

            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                _insert.CommandText = "select * from dbo.Clientes where Name=@User and Password=@User";
                //_insert.Parameters.Add("@User", SqlDbType.VarChar);
                _insert.Parameters.AddWithValue("@User", client.UserName);
                _insert.Parameters.AddWithValue("@Password", client.Password);

                SqlDataReader _reader =await _insert.ExecuteReaderAsync();

                result = _reader.HasRows;
            }
            catch (Exception e)
            {

            }
            return result;
        }

        public async Task<bool> signup(Cliente client)
        {

            bool result = false;

            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                _insert.CommandText = "insert into dbo.Clientes values (@User,@Password)";
                _insert.Parameters.AddWithValue("@User", client.userName);
                _insert.Parameters.AddWithValue("@Password", client.Password);

                if (await _insert.ExecuteNonQueryAsync() > 0)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}
