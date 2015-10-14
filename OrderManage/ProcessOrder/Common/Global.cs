using ProcessOrder.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProcessOrder.Common
{
    internal class Global
    {
        internal static string ID;
        internal static QuickForm quickForm;
        internal static NotifyIcon notifyIcon;

        internal static Hashtable netData = new Hashtable();
        internal static List<JiaoYiMaoHelper> jymhlist = new List<JiaoYiMaoHelper>();
        internal static string[] user;
    }
}
