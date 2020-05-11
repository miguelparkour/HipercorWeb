using HipercorWeb.Interfaces;
using HipercorWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HipercorWeb.Services
{
    public class DBASqlServerService : IDataBaseAccess

    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HipercorWeb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task<bool> ConfirmEmail(string email)
        {
            bool result = false;
            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _update = new SqlCommand();
                _update.Connection = _miConexion;
                _update.CommandType = CommandType.Text;
                _update.CommandText = "update dbo.Clientes set CuentaActiva=@CuentaActiva where Email=@Email";
                _update.Parameters.AddWithValue("@Email", email);
                _update.Parameters.AddWithValue("@CuentaActiva", true);

                if (await _update.ExecuteNonQueryAsync() > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

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
                _insert.Parameters.AddWithValue("@Password",StringCipher.EncryptOneWay(client.Password));

                SqlDataReader _reader =await _insert.ExecuteReaderAsync();

                while (_reader.Read())
                {
                    client.DatosPersonales = new DatosPersonales();
                    client.DatosPersonales.Nombre = _reader.GetString(0);
                    client.DatosPersonales.Apellidos = _reader.GetString(1);
                    client.DatosPersonales.Movil = _reader.GetString(2);
                    client.Password = null;
                }
                _insert.Connection.Close();
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
                _insert.Parameters.AddWithValue("@Password", StringCipher.EncryptOneWay(client.Password));
                _insert.Parameters.AddWithValue("@Nombre", client.DatosPersonales.Nombre);
                _insert.Parameters.AddWithValue("@Apellidos", client.DatosPersonales.Apellidos);
                _insert.Parameters.AddWithValue("@Movil", client.DatosPersonales.Movil);
                _insert.Parameters.AddWithValue("@CuentaActiva", false);

                if (await _insert.ExecuteNonQueryAsync() > 0)
                {
                    _insert.Parameters.Clear();
                    _insert.CommandText = "insert into dbo.Direcciones (Email,CP,IdProv,IdMun,Calle) values(@Email,@CP,@IdProv,@IdMun,@Calle)";
                    _insert.Parameters.AddWithValue("@Email", client.Email);
                    _insert.Parameters.AddWithValue("@CP", client.Direcciones[0].CodigoPostal);
                    _insert.Parameters.AddWithValue("@IdProv", client.Direcciones[0].Provincia.id);
                    _insert.Parameters.AddWithValue("@IdMun", client.Direcciones[0].Municipio.id);
                    _insert.Parameters.AddWithValue("@Calle", client.Direcciones[0].Calle);
                    if (await _insert.ExecuteNonQueryAsync() > 0)
                    {
                        result = true;
                        _insert.Connection.Close();
                    }
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        public async Task<Cliente> CargarDirecciones (Cliente cliente)
        {
            try
            { 
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                _insert.CommandText = "select CP,IdProv,IdMun,Calle from dbo.Direcciones where Email=@Email";
                _insert.Parameters.AddWithValue("@Email", cliente.Email);
                SqlDataReader _reader = _insert.ExecuteReader();

                cliente.Direcciones = new List<Direccion>();
                while (await _reader.ReadAsync())
                {
                    //cliente.Direcciones[i] = new Direccion();
                    Direccion dir = new Direccion();
                    dir.Provincia = new Provincia();
                    dir.Municipio = new Municipio();
                    dir.CodigoPostal = _reader.GetString(0);
                    dir.Provincia.id = _reader.GetString(1);
                    dir.Provincia.nm = getNm(_reader.GetString(1),"provincia");
                    dir.Municipio.id = _reader.GetString(2);
                    dir.Municipio.nm = getNm(_reader.GetString(2), "municipio");
                    dir.Calle = _reader.GetString(3);

                    cliente.Direcciones.Add(dir);
                }
                _insert.Connection.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return cliente;
        }
        private string getNm (string id, string option)
        {
            string result = null;
            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                switch (option)
                {
                    case "provincia":
                        _insert.CommandText = "select nm from dbo.provincias where id=@id";
                        _insert.Parameters.AddWithValue("@id",id);
                        SqlDataReader _reader = _insert.ExecuteReader();
                        _reader.Read();
                        result = _reader.GetString(0);
                        break;
                    case "municipio":
                        _insert.CommandText = "select nm from dbo.municipios where id=@id";
                        _insert.Parameters.AddWithValue("@id", id);
                        SqlDataReader _reader2 = _insert.ExecuteReader();
                        _reader2.Read();
                        result = _reader2.GetString(0);
                        break;
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}



