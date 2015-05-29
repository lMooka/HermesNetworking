using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HermesNetworking.Networking.Connection
{
    public interface IMyConnector
    {
        void Connect(string ip, int port);
    }
}
