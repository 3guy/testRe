using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PlaceOrder.Util
{
    class Win32API
    {
        [DllImport("user32", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hwnd, int wMsg, int wParam, ref int lParam);

        [DllImport("user32", EntryPoint = "RegisterWindowMessage")]
        public static extern int RegisterWindowMessage(string lpString);

        [DllImport("OLEACC.DLL", EntryPoint = "ObjectFromLresult")]
        public static extern int ObjectFromLresult(
            int lResult,
            ref System.Guid riid,
            int wParam,
            [MarshalAs(UnmanagedType.Interface), System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out]ref System.Object ppvObject
            //注意这个函数ObjectFromLresult的声明
        );
    }

}