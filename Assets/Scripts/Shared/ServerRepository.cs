using System.Collections.Generic;
using Zenject;

namespace Shared
{
    public class ServerRepository : IServerRepository
    {
        private List<ServerData> _servers = new()
        {
            new ServerData()
            {
                IP = "127.0.0.1",
                Port = 7798
            }
        };

        public ServerData GetServerInfo()
        {
            return _servers[0];
        }
    }
}