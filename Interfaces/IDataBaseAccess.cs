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


        #region Tablas de direcciones
        Task<Cliente> CargarDirecciones(Cliente cliente);
        Task<Cliente> BorrarDireccion(Cliente cliente, int id);
        Task<Cliente> EditarDireccion(Cliente cliente, Direccion dir, int indice);
        Task<Cliente> AddDireccion(Cliente cliente, Direccion dir);

        #endregion


        #region Tablas de productos
        Task<List<Producto>> CargarProductos();
        Task<Producto> CargarProductos(string id);
        Task<Cliente> CargarPedidos(Cliente cliente);
        Task<bool> HacerPedido(Pedido pedido, Cliente cliente, int index);
        #endregion
    }
}
