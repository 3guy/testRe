using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OrderManage.UI
{
    class ControlInvoke<CONTROL, DATA> : IInvokeItem
    {
        public ControlInvoke(CONTROL control, DATA data, Action<CONTROL, DATA> action)
        {
            mControl = control;
            mData = data;
            mAction = action;
        }
        private CONTROL mControl;
        private DATA mData;
        private Action<CONTROL, DATA> mAction;
        public void Execute()
        {
            mAction(mControl, mData);
            Application.DoEvents();
        }
    }
}
