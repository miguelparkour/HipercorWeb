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
         Task<bool> CheckEmail(string email);
         Task<bool> CheckConfirmEmail(string email);
        Task<bool> EditarPassword(string email, string password);
        Task<bool> RecuperarCuenta(string email);
        Task<bool> ConfirmEmail(string email);
        Task<Cliente> EditarPersonal(Cliente cliente);
        #endregion
        Task<Cliente> CargarDirecciones(Cliente cliente);
        Task<Cliente> BorrarDireccion(Cliente cliente, int id);
        Task<Cliente> EditarDireccion(Cliente cliente, Direccion dir, int indice);
        Task<Cliente> AddDireccion(Cliente cliente, Direccion dir);
    }
}
