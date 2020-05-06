using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HipercorWeb.Interfaces
{
    public interface ISendEmail
    {
        void Send(string to, string subject, string body);
    }
}
