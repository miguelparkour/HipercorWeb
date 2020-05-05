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
        Task<Boolean> signup(Cliente client);
        Task<Cliente> login(Cliente client);

        #endregion
    }
}
