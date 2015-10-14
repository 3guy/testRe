using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProcessOrder.UI
{
    public class Dispatch
    {
        static Dispatch()
        {
            Instance = new Dispatch();
            //ImportDataLog.WriteLog("创建了Dispatch对象！");
        }
        public void Add<CONTROL, DATA>(CONTROL control, DATA data, Action<CONTROL, DATA> action)
        {
            ControlInvoke<CONTROL, DATA> item = new ControlInvoke<CONTROL, DATA>(control, data, action);
            Add(item);
        }
        private Queue mQueues = Queue.Synchronized(new Queue());
        public static Dispatch Instance
        {
            get;
            set;
        }
        public void Add(IInvokeItem item)
        {
            mQueues.Enqueue(item);
        }
        public void Execite()
        {
            //lock (this)
            //{
                while (mQueues.Count > 0)
                {
                    ((IInvokeItem)mQueues.Dequeue()).Execute();
                    //Thread.Sleep(100);
                }
            //}
        }
    }
}
