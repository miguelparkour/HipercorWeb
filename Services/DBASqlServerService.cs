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
        #region Propiedades
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HipercorWeb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        #endregion

        #region Métodos



        #region Select
        public async Task<bool> RecuperarCuenta(string email)
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
                _insert.CommandText = "select Nombre from dbo.Clientes where Email=@Email";
                _insert.Parameters.AddWithValue("@Email", email);

                SqlDataReader _reader = await _insert.ExecuteReaderAsync();
        result = await _reader.ReadAsync();
        _insert.Connection.Close();
            }
            catch (Exception)
            {

            }
            return result;
        }
        public async Task<Cliente> CargarDirecciones(Cliente cliente)
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
                    dir.Provincia.nm = getNm(_reader.GetString(1), "provincia");
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
                _insert.CommandText = "select Nombre, Apellidos, Movil, Fijo from dbo.Clientes where Email=@Email and Password=@Password";
                _insert.Parameters.AddWithValue("@Email", client.Email);
                _insert.Parameters.AddWithValue("@Password", StringCipher.EncryptOneWay(client.Password));

                SqlDataReader _reader = await _insert.ExecuteReaderAsync();

                while (await _reader.ReadAsync())
                {
                    client.DatosPersonales = new DatosPersonales();
                    client.DatosPersonales.Nombre = _reader.GetString(0);
                    client.DatosPersonales.Apellidos = _reader.GetString(1);
                    client.DatosPersonales.Movil = _reader.GetString(2);
                    if (_reader.GetString(3) != null) client.DatosPersonales.Fijo = _reader.GetString(3);

                    client.Password = null;

                    return client;
                }
                _insert.Connection.Close();
            }
            catch (Exception)
            {

            }
            return null;
        }
        public async Task<bool> CheckEmail(string email)
        {
            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                _insert.CommandText = "select Nombre from dbo.Clientes where Email=@Email";
                _insert.Parameters.AddWithValue("@Email", email);

                SqlDataReader _reader = await _insert.ExecuteReaderAsync();

                while (await _reader.ReadAsync())
                {
                    _insert.Connection.Close();
                    return true;
                }
                _insert.Connection.Close();
            }
            catch (Exception)
            {

            }
            return false;

        }
        public async Task<bool> CheckConfirmEmail(string email)
        {
            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                _insert.CommandText = "select CuentaActiva from dbo.Clientes where Email=@Email";
                _insert.Parameters.AddWithValue("@Email", email);

                SqlDataReader _reader = await _insert.ExecuteReaderAsync();

                while (await _reader.ReadAsync())
                {
                    bool boleano = _reader.GetBoolean(0);
                    _insert.Connection.Close();
                    return !boleano;
                    //Devuelve true cuando es false para que entre en el if de cuentaNoActiva
                }
                _insert.Connection.Close();
            }
            catch (Exception)
            {

            }
            return false;

        }
        #endregion


        #region Insert
        public async Task<Cliente> AddDireccion(Cliente cliente, Direccion dir)
        {
            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                _insert.CommandText = "insert into dbo.Direcciones (Email,CP,IdProv,IdMun,Calle) values (@Email,@CP,@IdProv,@IdMun,@Calle)";
                _insert.Parameters.AddWithValue("@Email", cliente.Email);
                _insert.Parameters.AddWithValue("@CP", dir.CodigoPostal);
                _insert.Parameters.AddWithValue("@IdProv", dir.Provincia.id);
                _insert.Parameters.AddWithValue("@IdMun", dir.Municipio.id);
                _insert.Parameters.AddWithValue("@Calle", dir.Calle);

                if (await _insert.ExecuteNonQueryAsync() > 0)
                {
                    dir.Provincia.nm = this.getNm(dir.Provincia.id, "provincia");
                    dir.Municipio.nm = this.getNm(dir.Municipio.id, "municipio");
                    cliente.Direcciones.Add(dir);
                    return cliente;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
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
        #endregion


        #region Update
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
        public async Task<bool> EditarPassword(string email, string password)
        {
            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                _insert.CommandText = "update dbo.Clientes set Password = @Password where Email = @Email";
                _insert.Parameters.AddWithValue("@Email", email);
                _insert.Parameters.AddWithValue("@Password", StringCipher.EncryptOneWay(password));

                if (await _insert.ExecuteNonQueryAsync() > 0)
                {
                    return true;
                }
                _insert.Connection.Close();

            }
            catch (Exception)
            {

            }
            return false;
        }
        public async Task<Cliente> EditarPersonal(Cliente cliente)
        {
            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                _insert.CommandText = "update dbo.Clientes set Nombre = @Nombre, Apellidos = @Apellidos , Movil = @Movil , Fijo = @Fijo where Email = @Email";
                _insert.Parameters.AddWithValue("@Email", cliente.Email);
                _insert.Parameters.AddWithValue("@Nombre", cliente.DatosPersonales.Nombre);
                _insert.Parameters.AddWithValue("@Apellidos", cliente.DatosPersonales.Apellidos);
                _insert.Parameters.AddWithValue("@Movil", cliente.DatosPersonales.Movil);
                if (cliente.DatosPersonales.Fijo == null)
                    _insert.Parameters.AddWithValue("@Fijo", "");
                else
                    _insert.Parameters.AddWithValue("@Fijo", cliente.DatosPersonales.Fijo);

                if (await _insert.ExecuteNonQueryAsync() > 0)
                {
                    return cliente;
                }
                _insert.Connection.Close();

            }
            catch (Exception)
            {

            }
            return null;
        }
        public async Task<Cliente> EditarDireccion(Cliente cliente, Direccion dir, int indice)
        {
            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                _insert.CommandText = "update dbo.Direcciones set CP = @CP, IdProv = @IdProv ,IdMun = @IdMun ,Calle = @Calle where Email=@Email and IdMun = @OldIdMun and Calle = @OldCalle";
                _insert.Parameters.AddWithValue("@Email", cliente.Email);
                _insert.Parameters.AddWithValue("@OldIdMun", cliente.Direcciones[indice].Municipio.id);
                _insert.Parameters.AddWithValue("@OldCalle", cliente.Direcciones[indice].Calle);
                _insert.Parameters.AddWithValue("@CP", dir.CodigoPostal);
                _insert.Parameters.AddWithValue("@IdProv", dir.Provincia.id);
                _insert.Parameters.AddWithValue("@IdMun", dir.Municipio.id);
                _insert.Parameters.AddWithValue("@Calle", dir.Calle);

                if (await _insert.ExecuteNonQueryAsync() > 0)
                {
                    dir.Provincia.nm = this.getNm(dir.Provincia.id, "provincia");
                    dir.Municipio.nm = this.getNm(dir.Municipio.id, "municipio");
                    cliente.Direcciones[indice] = dir;
                    return cliente;
                }
                _insert.Connection.Close();
            }
            catch (Exception)
            {

            }
            return null;
        }
        #endregion


        #region Delete
        public async Task<Cliente> BorrarDireccion(Cliente cliente, int id)
        {
            try
            {
                SqlConnection _miConexion = new SqlConnection();
                _miConexion.ConnectionString = this.connectionString;
                _miConexion.Open();

                SqlCommand _insert = new SqlCommand();
                _insert.Connection = _miConexion;
                _insert.CommandType = CommandType.Text;
                _insert.CommandText = "delete from dbo.Direcciones where Email = @Email and IdMun = @IdMun and Calle = @Calle";
                _insert.Parameters.AddWithValue("@Email", cliente.Email);
                _insert.Parameters.AddWithValue("@IdMun", cliente.Direcciones[id].Municipio.id);
                _insert.Parameters.AddWithValue("@Calle", cliente.Direcciones[id].Calle);
                if (await _insert.ExecuteNonQueryAsync() > 0)
                {
                    cliente.Direcciones.RemoveAt(id);
                    return cliente;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        #endregion


        #region Métodos privados
        private string getNm(string id, string option)
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
                        _insert.Parameters.AddWithValue("@id", id);
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
        #endregion



        #endregion
    }
    
}



