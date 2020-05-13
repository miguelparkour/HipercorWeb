using HipercorWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HipercorWeb.Interfaces
{
    public interface IDataBaseAccess
    {
        #region Tablas del cliente
        Task<bool> signup(Cliente client);
        Task<Cliente> login(Cliente client);

        Task<bool> ConfirmEmail(string email);
        Task<Cliente> EditarPersonal(Cliente cliente);
        #endregion
        Task<Cliente> CargarDirecciones(Cliente cliente);
        Task<Cliente> BorrarDireccion(string municipio, string calle, Cliente cliente);
        Task<Cliente> AddDireccion(Cliente cliente, Direccion dir);
    }
}
