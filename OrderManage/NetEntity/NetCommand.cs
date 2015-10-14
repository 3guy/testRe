using System;
using System.Collections.Generic;
using System.Text;

namespace NetEntity
{
    [Serializable()]
    public class NetCommand
    {
        public string gid;
        public string cmd;
        public object data;

        public NetCommand()
        { }

        public NetCommand(string cmd, object data)
        {
            gid = Guid.NewGuid().ToString("N");
            this.cmd = cmd;
            this.data = data;
        }

        public NetCommand(string gid, string cmd, object data)
        {
            this.gid = gid;
            this.cmd = cmd;
            this.data = data;
        }
    }
}
