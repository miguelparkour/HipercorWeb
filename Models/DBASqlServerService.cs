using HipercorWeb.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace HipercorWeb.Models
{
    public class DBASqlServerService : IDataBaseAccess

    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HipercorWeb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private byte[] HASHKEY = new byte[24] { 175, 17, 62, 27, 21, 3, 47, 163, 72, 9, 111, 102, 40, 200, 213, 117, 56, 84, 123, 60, 239, 106, 248, 133 };


        public async Task<Cliente> login(Cliente client)
        {
            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                _insert.CommandText = "select Nombre, Apellidos, Movil from dbo.Clientes where Email=@Email and Password=@Password";
                _insert.Parameters.AddWithValue("@Email", client.Email);
                _insert.Parameters.AddWithValue("@Password",StringCipher.Encrypt(client.Password,HASHKEY));

                SqlDataReader _reader =await _insert.ExecuteReaderAsync();

                while (_reader.Read())
                {
                    client.DatosPersonales = new DatosPersonales();
                    client.DatosPersonales.Nombre = _reader.GetString(0);
                    client.DatosPersonales.Apellidos = _reader.GetString(1);
                    client.DatosPersonales.Movil = _reader.GetString(2);
                    client.Password = null;
                }
            }
            catch (Exception)
            {
            
            }
            return client;
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
                _insert.CommandText = "insert into dbo.Clientes (Email,Password,Nombre,Apellidos,Movil,CuentaActiva) values (@Email,@Password,@Nombre,@Apellidos,@Movil,@CuentaActiva)";
                _insert.Parameters.AddWithValue("@Email", client.Email);
                _insert.Parameters.AddWithValue("@Password", StringCipher.Encrypt(client.Password, HASHKEY));
                _insert.Parameters.AddWithValue("@Nombre", client.DatosPersonales.Nombre);
                _insert.Parameters.AddWithValue("@Apellidos", client.DatosPersonales.Apellidos);
                _insert.Parameters.AddWithValue("@Movil", client.DatosPersonales.Movil);
                _insert.Parameters.AddWithValue("@CuentaActiva", false);

                if (await _insert.ExecuteNonQueryAsync() > 0)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return result;
        }



        /*public async Task<bool> login(Cliente client)
        {
            bool result = false;

            DataContext db = new DataContext(this.connectionString);

            Table<Cliente> Clientes = db.GetTable<Cliente>();
            //InvalidOperationException: El tipo 'HipercorWeb.Models.Cliente' no está asignado como Table.

            IEnumerable<Cliente> query = from c in Clientes where c.UserName == "migue" select c;

            foreach (var item in query)
            {
                result = true;
            }

            return result;
        }*/
    }
}



