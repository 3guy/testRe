using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OrderManage.Util
{
    public class DeskTopCapture
    {
        public static Image Capture()
        {
            ////获得桌面窗口的上下文
            //IntPtr desktopWindow = GetDesktopWindow();
            //IntPtr desktopDC = GetDC(desktopWindow);
            ////得到image的GDI句柄
            //IntPtr desktopBitmap = GetCurrentObject(desktopDC, OBJ_BITMAP);
            ////用句柄创建一个.NET图形对象
            //Bitmap desktopImage = Image.FromHbitmap(desktopBitmap);
            ////释放设备上下文
            ////ReleaseDC(desktopDC);
            //return desktopImage;

            Image myImg = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics g = Graphics.FromImage(myImg);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
            myImg.Save(DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return myImg;
        }
    }
}
