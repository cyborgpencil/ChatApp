using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppServer.ViewModels
{
    public enum ServerCommands
    {
        WRITE_USERS_TO_DISC,
        DELETE_USERS_FROM_FILE
    }



    public class ServerCommandManager
    {
        public ServerCommands ServerComm { get; set; }

        public ServerCommandManager()
        {
            ServerComm = new ServerCommands();

        }
    }
}
