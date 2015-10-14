using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace OrderManage.Util
{
    internal class ImageHelper
    {
        //图片 转为    base64编码的文本
        internal static string ImgToBase64String(Bitmap bmp)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
                return strbaser64;
                // MessageBox.Show("转换成功!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //base64编码的文本 转为    图片
        internal static Bitmap Base64StringToImage(string code)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(code);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                //bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);
                ms.Close();
                return bmp;
                //MessageBox.Show("转换成功！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
