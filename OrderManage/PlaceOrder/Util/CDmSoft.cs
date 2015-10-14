using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.InteropServices;

namespace PlaceOrder.Util
{
    /// <summary>
    /// 大漠插件C#免注册调用类
    /// 作者：清风抚断云
    /// QQ：274838061
    /// 本模块必须包含dmc.dll 实现不用注册dm.dll 到系统可以动态调用
    /// 最新修改：2013-8-23
    /// 修复vs2012 调试自动退出问题
    /// </summary>
    class CDmSoft : IDisposable
    {
        const string DMCNAME = "dmc.dll";
        #region import DLL 函数
        [DllImport(DMCNAME,CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr CreateDM(string dmpath);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FreeDM();

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Ver(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetPath(IntPtr dm, string path);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Ocr(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindStr(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetResultCount(IntPtr dm, string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetResultPos(IntPtr dm, string str, int index, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int StrStr(IntPtr dm, string s, string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SendCommand(IntPtr dm,string cmd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int UseDict(IntPtr dm, int index);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetBasePath(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetDictPwd(IntPtr dm, string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr OcrInFile(IntPtr dm, int x1, int y1, int x2, int y2, string pic_name, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Capture(IntPtr dm, int x1, int y1, int x2, int y2, string file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int KeyPress(IntPtr dm, int vk);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int KeyDown(IntPtr dm, int vk);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int KeyUp(IntPtr dm, int vk);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int LeftClick(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int RightClick(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int MiddleClick(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int LeftDoubleClick(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int LeftDown(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int LeftUp(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int RightDown(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int RightUp(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int MoveTo(IntPtr dm,int x,int  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int MoveR(IntPtr dm,int rx,int  ry);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetColor(IntPtr dm,int x,int  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetColorBGR(IntPtr dm,int x,int  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr RGB2BGR(IntPtr dm,string rgb_color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr BGR2RGB(IntPtr dm,string bgr_color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int UnBindWindow(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CmpColor(IntPtr dm,int x,int  y,string  color,double  sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ClientToScreen(IntPtr dm,int hwnd,ref object  x,ref object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ScreenToClient(IntPtr dm,int hwnd,ref object  x,ref object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ShowScrMsg(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  msg,string color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetMinRowGap(IntPtr dm,int row_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetMinColGap(IntPtr dm,int col_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindColor(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  color,double sim,int  dir,out object  x,out object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindColorEx(IntPtr dm,int x1,int  y1,int  x2,int  y2,string color,double  sim,int  dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetWordLineHeight(IntPtr dm,int line_height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetWordGap(IntPtr dm,int word_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetRowGapNoDict(IntPtr dm,int row_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetColGapNoDict(IntPtr dm,int col_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetWordLineHeightNoDict(IntPtr dm,int line_height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetWordGapNoDict(IntPtr dm,int word_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetWordResultCount(IntPtr dm,string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetWordResultPos(IntPtr dm,string str,int  index,out object  x,out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetWordResultStr(IntPtr dm,string str,int  index);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetWords(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  color,double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetWordsNoDict(IntPtr dm,int x1,int  y1,int  x2,int  y2,string color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetShowErrorMsg(IntPtr dm,int show);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetClientSize(IntPtr dm,int hwnd,out object  width,out object  height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int MoveWindow(IntPtr dm,int hwnd,int  x,int  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetColorHSV(IntPtr dm,int x,int  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetAveRGB(IntPtr dm,int x1,int  y1,int  x2,int  y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetAveHSV(IntPtr dm,int x1,int  y1,int  x2,int  y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetForegroundWindow(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetForegroundFocus(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetMousePointWindow(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetPointWindow(IntPtr dm,int x,int  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr EnumWindow(IntPtr dm,int parent,string  title,string  class_name,int filter);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetWindowState(IntPtr dm,int hwnd,int  flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetWindow(IntPtr dm,int hwnd,int  flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetSpecialWindow(IntPtr dm,int flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetWindowText(IntPtr dm,int hwnd,string  text);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetWindowSize(IntPtr dm,int hwnd,int  width,int  height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetWindowRect(IntPtr dm,int hwnd,out object  x1,out object  y1,out object  x2,out object  y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetWindowTitle(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetWindowClass(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetWindowState(IntPtr dm,int hwnd,int  flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CreateFoobarRect(IntPtr dm,int hwnd,int  x,int  y,int  w,int  h);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CreateFoobarRoundRect(IntPtr dm,int hwnd,int  x,int  y,int  w,int  h,int  rw,int  rh);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CreateFoobarEllipse(IntPtr dm,int hwnd,int  x,int  y,int  w,int  h);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CreateFoobarCustom(IntPtr dm,int hwnd,int  x,int  y,string  pic,string  trans_color,double  sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarFillRect(IntPtr dm,int hwnd,int  x1,int  y1,int  x2,int  y2,string  color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarDrawText(IntPtr dm,int hwnd,int  x,int  y,int  w,int  h,string  text,string  color,int  align);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarDrawPic(IntPtr dm,int hwnd,int  x,int  y,string  pic,string  trans_color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarUpdate(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarLock(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarUnlock(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarSetFont(IntPtr dm,int hwnd,string  font_name,int  size,int  flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarTextRect(IntPtr dm,int hwnd,int  x,int  y,int  w,int  h);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarPrintText(IntPtr dm,int hwnd,string  text,string  color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarClearText(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarTextLineGap(IntPtr dm,int hwnd,int  gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Play(IntPtr dm,string file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FaqCapture(IntPtr dm,int x1,int  y1,int  x2,int  y2,int  quality,int delay,int  time);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FaqRelease(IntPtr dm,int handle);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FaqSend(IntPtr dm,string server,int  handle,int  request_type,int  time_out);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Beep(IntPtr dm,int fre,int  delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarClose(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int MoveDD(IntPtr dm,int dx,int  dy);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FaqGetSize(IntPtr dm,int handle);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int LoadPic(IntPtr dm,string pic_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FreePic(IntPtr dm,string pic_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetScreenData(IntPtr dm,int x1,int  y1,int  x2,int  y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FreeScreenData(IntPtr dm,int handle);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WheelUp(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WheelDown(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetMouseDelay(IntPtr dm,string type_,int  delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetKeypadDelay(IntPtr dm,string type_,int  delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetEnv(IntPtr dm,int index,string  name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetEnv(IntPtr dm,int index,string  name,string  value);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SendString(IntPtr dm,int hwnd,string  str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DelEnv(IntPtr dm,int index,string  name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetPath(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetDict(IntPtr dm,int index,string  dict_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindPic(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  pic_name,string  delta_color,double  sim,int  dir,out object  x,out object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindPicEx(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  pic_name,string  delta_color,double  sim,int  dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetClientSize(IntPtr dm,int hwnd,int  width,int  height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ReadInt(IntPtr dm,int hwnd,string  addr,int  type_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ReadFloat(IntPtr dm,int hwnd,string  addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ReadDouble(IntPtr dm,int hwnd,string  addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindInt(IntPtr dm,int hwnd,string  addr_range,int  int_value_min,int int_value_max,int  type_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindFloat(IntPtr dm,int hwnd,string  addr_range,Single  float_value_min,Single  float_value_max);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindDouble(IntPtr dm,int hwnd,string  addr_range,double  double_value_min,double  double_value_max);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindString(IntPtr dm,int hwnd,string  addr_range,string  string_value,int  type_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetModuleBaseAddr(IntPtr dm,int hwnd,string  module_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr MoveToEx(IntPtr dm,int x,int  y,int  w,int  h);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr MatchPicName(IntPtr dm,string pic_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int AddDict(IntPtr dm,int index,string  dict_info);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnterCri(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int LeaveCri(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteInt(IntPtr dm,int hwnd,string  addr,int  type_,int  v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteFloat(IntPtr dm,int hwnd,string  addr,Single  v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteDouble(IntPtr dm,int hwnd,string  addr,double  v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteString(IntPtr dm,int hwnd,string  addr,int  type_,string  v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int AsmAdd(IntPtr dm,string asm_ins);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int AsmClear(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int AsmCall(IntPtr dm,int hwnd,int  mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindMultiColor(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  first_color,string  offset_color,double  sim,int  dir,out object  x,out object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindMultiColorEx(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  first_color,string  offset_color,double  sim,int  dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr AsmCode(IntPtr dm,int base_addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Assemble(IntPtr dm,string asm_code,int  base_addr,int  is_upper);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetWindowTransparent(IntPtr dm,int hwnd,int  v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr ReadData(IntPtr dm,int hwnd,string  addr,int  len);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteData(IntPtr dm,int hwnd,string  addr,string  data);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindData(IntPtr dm,int hwnd,string  addr_range,string  data);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetPicPwd(IntPtr dm,string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Log(IntPtr dm,string info);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindStrE(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindColorE(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  color,double  sim,int  dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindPicE(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  pic_name,string  delta_color,double  sim,int  dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindMultiColorE(IntPtr dm,int x1,int  y1,int  x2,int  y2,string first_color,string  offset_color,double sim,int  dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetExactOcr(IntPtr dm,int exact_ocr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr ReadString(IntPtr dm,int hwnd,string  addr,int  type_,int  len);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarTextPrintDir(IntPtr dm,int hwnd,int  dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr OcrEx(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  color,double  sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetDisplayInput(IntPtr dm,string mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetTime(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetScreenWidth(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetScreenHeight(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int BindWindowEx(IntPtr dm,int hwnd,string  display,string  mouse,string  keypad,string  internal_desc,int  mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetDiskSerial(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr Md5(IntPtr dm,string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetMac(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ActiveInputMethod(IntPtr dm,int hwnd,string  id);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CheckInputMethod(IntPtr dm,int hwnd,string  id);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindInputMethod(IntPtr dm,string id);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetCursorPos(IntPtr dm,out object x,out object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int BindWindow(IntPtr dm,int hwnd,string  display,string  mouse,string  keypad,int  mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindWindow(IntPtr dm,string class_name,string  title_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetScreenDepth(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetScreen(IntPtr dm,int width,int  height,int  depth);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ExitOs(IntPtr dm,int type_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetDir(IntPtr dm,int type_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetOsType(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindWindowEx(IntPtr dm,int parent,string  class_name,string  title_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetExportDict(IntPtr dm,int index,string  dict_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCursorShape(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DownCpu(IntPtr dm,int rate);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCursorSpot(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SendString2(IntPtr dm,int hwnd,string  str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FaqPost(IntPtr dm,string server,int  handle,int  request_type,int  time_out);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FaqFetch(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FetchWord(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  color,string  word);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CaptureJpg(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  file_,int  quality);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindStrWithFont(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim,string   font_name,int  font_size,int  flag,out object  x,out object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindStrWithFontE(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim,string  font_name,int  font_size,int  flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindStrWithFontEx(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim,string  font_name,int  font_size,int  flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetDictInfo(IntPtr dm,string str,string  font_name,int  font_size,int  flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SaveDict(IntPtr dm,int index,string  file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetWindowProcessId(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetWindowProcessPath(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int LockInput(IntPtr dm,int lock1);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetPicSize(IntPtr dm,string pic_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetID(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CapturePng(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CaptureGif(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  file_,int  delay,int  time);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ImageToBmp(IntPtr dm,string pic_name,string  bmp_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindStrFast(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim,out object  x,out object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindStrFastEx(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindStrFastE(IntPtr dm,int x1,int  y1,int  x2,int  y2,string str,string  color,double  sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableDisplayDebug(IntPtr dm,int enable_debug);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CapturePre(IntPtr dm,string file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int RegEx(IntPtr dm,string code,string  Ver,string  ip);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetMachineCode(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetClipboard(IntPtr dm,string data);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetClipboard(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetNowDict(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Is64Bit(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetColorNum(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  color,double  sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr EnumWindowByProcess(IntPtr dm,string process_name,string  title,string  class_name,int  filter);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetDictCount(IntPtr dm,int index);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetLastError(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetNetTime(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableGetColorByCapture(IntPtr dm,int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CheckUAC(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetUAC(IntPtr dm,int uac);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DisableFontSmooth(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CheckFontSmooth(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetDisplayAcceler(IntPtr dm,int level);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindWindowByProcess(IntPtr dm,string process_name,string  class_name,string  title_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindWindowByProcessId(IntPtr dm,int process_id,string  class_name,string  title_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr ReadIni(IntPtr dm,string section,string  key,string  file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteIni(IntPtr dm,string section,string  key,string  v,string  file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int RunApp(IntPtr dm,string path,int  mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int delay(IntPtr dm,int mis);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindWindowSuper(IntPtr dm,string spec1,int  flag1,int  type1,string  spec2,int  flag2,int  type2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr ExcludePos(IntPtr dm,string all_pos,int  type_,int  x1,int  y1,int  x2,int  y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindNearestPos(IntPtr dm,string all_pos,int  type_,int  x,int  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr SortPosDistance(IntPtr dm,string all_pos,int  type_,int  x,int  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindPicMem(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  pic_info,string  delta_color,double  sim,int  dir,out object  x,out object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindPicMemEx(IntPtr dm,int x1,int  y1,int  x2,int  y2,string pic_info,string  delta_color,double  sim,int  dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindPicMemE(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  pic_info,string  delta_color,double  sim,int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr AppendPicAddr(IntPtr dm,string pic_info,int  addr,int  size);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteFile(IntPtr dm,string file_,string  content);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Stop(IntPtr dm,int id);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetDictMem(IntPtr dm,int index,int  addr,int  size);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetNetTimeSafe(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ForceUnBindWindow(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr ReadIniPwd(IntPtr dm,string section,string  key,string  file_,string  pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteIniPwd(IntPtr dm,string section,string  key,string  v,string  file_,string  pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DecodeFile(IntPtr dm,string file_,string  pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int KeyDownChar(IntPtr dm,string key_str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int KeyUpChar(IntPtr dm,string key_str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int KeyPressChar(IntPtr dm,string key_str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int KeyPressStr(IntPtr dm,string key_str,int  delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableKeypadPatch(IntPtr dm,int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableKeypadSync(IntPtr dm,int en,int  time_out);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableMouseSync(IntPtr dm,int en,int  time_out);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DmGuard(IntPtr dm,int en,string  type_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FaqCaptureFromFile(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  file_,int  quality);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindIntEx(IntPtr dm,int hwnd,string  addr_range,int  int_value_min,int  int_value_max,int  type_,int  step,int  multi_thread,int  mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindFloatEx(IntPtr dm,int hwnd,string  addr_range,Single  float_value_min,Single  float_value_max,int  step,int  multi_thread,int  mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindDoubleEx(IntPtr dm,int hwnd,string  addr_range,double  double_value_min,double  double_value_max,int  step,int  multi_thread,int  mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindStringEx(IntPtr dm,int hwnd,string  addr_range,string  string_value,int  type_,int  step,int  multi_thread,int  mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindDataEx(IntPtr dm,int hwnd,string  addr_range,string  data,int  step,int  multi_thread,int  mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableRealMouse(IntPtr dm,int en,int  mousedelay,int  mousestep);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableRealKeypad(IntPtr dm,int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SendStringIme(IntPtr dm,string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarDrawLine(IntPtr dm,int hwnd,int  x1,int  y1,int  x2,int  y2,string  color,int  style,int  width);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindStrEx(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IsBind(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetDisplayDelay(IntPtr dm,int t);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetDmCount(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DisableScreenSave(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DisablePowerSave(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetMemoryHwndAsProcessId(IntPtr dm,int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindShape(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  offset_color,double  sim,int  dir,out object  x,out object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindShapeE(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  offset_color,double  sim,int  dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindShapeEx(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  offset_color,double  sim,int  dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindStrS(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim,out object  x,out object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindStrExS(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindStrFastS(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim,out object  x,out object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindStrFastExS(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindPicS(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  pic_name,string  delta_color,double  sim,int  dir,out object  x,out object  y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindPicExS(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  pic_name,string  delta_color,double  sim,int  dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ClearDict(IntPtr dm,int index);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetMachineCodeNoMac(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetClientRect(IntPtr dm,int hwnd,out object  x1,out object  y1,out object  x2,out object  y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableFakeActive(IntPtr dm,int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetScreenDataBmp(IntPtr dm,int x1,int  y1,int  x2,int  y2,out object  data,out object  size);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EncodeFile(IntPtr dm,string file_,string  pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCursorShapeEx(IntPtr dm,int type_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FaqCancel(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr IntToData(IntPtr dm,int int_value,int  type_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FloatToData(IntPtr dm,Single float_value);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr DoubleToData(IntPtr dm,double double_value);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr StringToData(IntPtr dm,string string_value,int  type_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetMemoryFindResultToFile(IntPtr dm,string file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableBind(IntPtr dm,int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetSimMode(IntPtr dm,int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int LockMouseRect(IntPtr dm,int x1,int  y1,int  x2,int  y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SendPaste(IntPtr dm,int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IsDisplayDead(IntPtr dm,int x1,int  y1,int  x2,int  y2,int  t);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetKeyState(IntPtr dm,int vk);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CopyFile(IntPtr dm,string src_file,string  dst_file,int  over);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IsFileExist(IntPtr dm,string file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DeleteFile(IntPtr dm,string file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int MoveFile(IntPtr dm,string src_file,string  dst_file);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CreateFolder(IntPtr dm,string folder_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DeleteFolder(IntPtr dm,string folder_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetFileLength(IntPtr dm,string file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr ReadFile(IntPtr dm,string file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WaitKey(IntPtr dm,int key_code,int  time_out);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DeleteIni(IntPtr dm,string section,string  key,string  file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DeleteIniPwd(IntPtr dm,string section,string  key,string  file_,string  pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableSpeedDx(IntPtr dm,int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableIme(IntPtr dm,int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Reg(IntPtr dm,string code,string  Ver);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr SelectFile(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr SelectDirectory(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int LockDisplay(IntPtr dm,int lock1);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarSetSave(IntPtr dm,int hwnd,string  file_,int  en,string   header);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr EnumWindowSuper(IntPtr dm,string spec1,int  flag1,int  type1,string  spec2,int  flag2,int  type2,int  sort);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int DownloadFile(IntPtr dm,string url,string  save_file,int  timeout);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableKeypadMsg(IntPtr dm,int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int EnableMouseMsg(IntPtr dm,int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int RegNoMac(IntPtr dm,string code,string  Ver);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int RegExNoMac(IntPtr dm,string code,string  Ver,string  ip);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetEnumWindowDelay(IntPtr dm,int delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindMulColor(IntPtr dm,int x1,int  y1,int  x2,int  y2,string  color,double  sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetDict(IntPtr dm,int index,int  font_index);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetBindWindow(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarStartGif(IntPtr dm, int hwnd, int x, int y, string pic_name, int repeat_limit, int delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FoobarStopGif(IntPtr dm, int hwnd, int x, int y, string pic_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FreeProcessMemory(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr ReadFileData(IntPtr dm, string file_, int start_pos, int end_pos);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int VirtualAllocEx(IntPtr dm, int hwnd, int addr, int size, int type_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int VirtualFreeEx(IntPtr dm, int hwnd, int addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCommandLine(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int TerminateProcess(IntPtr dm, int pid);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetNetTimeByIp(IntPtr dm, string ip);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr EnumProcess(IntPtr dm, string name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetProcessInfo(IntPtr dm, int pid);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ReadIntAddr(IntPtr dm, int hwnd, int addr, int type_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr ReadDataAddr(IntPtr dm, int hwnd, int addr, int len);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ReadDoubleAddr(IntPtr dm, int hwnd, int addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int ReadFloatAddr(IntPtr dm, int hwnd, int addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr ReadStringAddr(IntPtr dm, int hwnd, int addr, int type_, int len);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteDataAddr(IntPtr dm, int hwnd, int addr, string data);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteDoubleAddr(IntPtr dm, int hwnd, int addr, double v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteFloatAddr(IntPtr dm, int hwnd, int addr, Single v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteIntAddr(IntPtr dm, int hwnd, int addr, int type_, int v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int WriteStringAddr(IntPtr dm, int hwnd, int addr, int type_, string v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int Delays(IntPtr dm, int min_s, int max_s);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int FindColorBlock(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim, int count, int width, int height, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr FindColorBlockEx(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim, int count, int width, int height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int OpenProcess(IntPtr dm, int pid);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr EnumIniSection(IntPtr dm, string file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr EnumIniSectionPwd(IntPtr dm, string file_, string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr EnumIniKey(IntPtr dm, string section, string file_);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr EnumIniKeyPwd(IntPtr dm, string section, string file_, string pwd);


        #endregion

        private IntPtr _dm = IntPtr.Zero;
        private bool disposed = false;

        internal IntPtr DM
        {
            get { return _dm; }
            set { _dm = value; }
        }
      

        internal CDmSoft()
        {
            _dm = CreateDM("dm.dll");
        }

        internal CDmSoft(string path)
        {
            _dm = CreateDM(path);
        }

        internal string Ver(){
            return Marshal.PtrToStringUni(Ver(_dm));
        }

        internal int GetBindWindow()
        {
            return GetBindWindow(_dm);
        }

        internal int FoobarStartGif(int hwnd, int x, int y, string pic_name, int repeat_limit, int delay)
        {
            return FoobarStartGif(_dm, hwnd, x, y, pic_name, repeat_limit, delay);
        }

        internal int FoobarStopGif(int hwnd, int x, int y, string pic_name)
        {
            return FoobarStopGif(_dm, hwnd, x, y, pic_name);
        }

        internal int FreeProcessMemory(int hwnd)
        {
            return FreeProcessMemory(_dm, hwnd);
        }

        internal string ReadFileData(string file_, int start_pos, int end_pos)
        {
            return Marshal.PtrToStringUni(ReadFileData(_dm, file_, start_pos, end_pos));
        }

        internal int VirtualAllocEx(int hwnd, int addr, int size, int type_)
        {
            return VirtualAllocEx(_dm, hwnd, addr, size, type_);
        }

        internal int VirtualFreeEx(int hwnd, int addr)
        {
            return VirtualFreeEx(_dm, hwnd, addr);
        }

        internal string GetCommandLine(int hwnd)
        {
            return Marshal.PtrToStringUni(GetCommandLine(_dm, hwnd));
        }

        internal int TerminateProcess(int pid)
        {
            return TerminateProcess(_dm, pid);
        }

        internal string GetNetTimeByIp(string ip)
        {
            return Marshal.PtrToStringUni(GetNetTimeByIp(_dm, ip));
        }

        internal string EnumProcess(string name)
        {
            return Marshal.PtrToStringUni(EnumProcess(_dm, name));
        }

        internal string GetProcessInfo(int pid)
        {
            return Marshal.PtrToStringUni(GetProcessInfo(_dm, pid));
        }

        internal int ReadIntAddr(int hwnd, int addr, int type_)
        {
            return ReadIntAddr(_dm, hwnd, addr, type_);
        }

        internal string ReadDataAddr(int hwnd, int addr, int len)
        {
            return Marshal.PtrToStringUni(ReadDataAddr(_dm, hwnd, addr, len));
        }

        internal int ReadDoubleAddr(int hwnd, int addr)
        {
            return ReadDoubleAddr(_dm, hwnd, addr);
        }

        internal int ReadFloatAddr(int hwnd, int addr)
        {
            return ReadFloatAddr(_dm, hwnd, addr);
        }

        internal string ReadStringAddr(int hwnd, int addr, int type_, int len)
        {
            return Marshal.PtrToStringUni(ReadStringAddr(_dm, hwnd, addr, type_, len));
        }

        internal int WriteDataAddr(int hwnd, int addr, string data)
        {
            return WriteDataAddr(_dm, hwnd, addr, data);
        }

        internal int WriteDoubleAddr(int hwnd, int addr, double v)
        {
            return WriteDoubleAddr(_dm, hwnd, addr, v);
        }

        internal int WriteFloatAddr(int hwnd, int addr, Single v)
        {
            return WriteFloatAddr(_dm, hwnd, addr, v);
        }

        internal int WriteIntAddr(int hwnd, int addr, int type_, int v)
        {
            return WriteIntAddr(_dm, hwnd, addr, type_, v);
        }

        internal int WriteStringAddr(int hwnd, int addr, int type_, string v)
        {
            return WriteStringAddr(_dm, hwnd, addr, type_, v);
        }

        internal int Delays(int min_s, int max_s)
        {
            return Delays(_dm, min_s, max_s);
        }

        internal int FindColorBlock(int x1, int y1, int x2, int y2, string color, double sim, int count, int width, int height, out object x, out object y)
        {
            return FindColorBlock(_dm, x1, y1, x2, y2, color, sim, count, width, height, out x, out y);
        }

        internal string FindColorBlockEx(int x1, int y1, int x2, int y2, string color, double sim, int count, int width, int height)
        {
            return Marshal.PtrToStringUni(FindColorBlockEx(_dm, x1, y1, x2, y2, color, sim, count, width, height));
        }

        internal int OpenProcess(int pid)
        {
            return OpenProcess(_dm, pid);
        }

        internal string EnumIniSection(string file_)
        {
            return Marshal.PtrToStringUni(EnumIniSection(_dm, file_));
        }

        internal string EnumIniSectionPwd(string file_, string pwd)
        {
            return Marshal.PtrToStringUni(EnumIniSectionPwd(_dm, file_, pwd));
        }

        internal string EnumIniKey(string section, string file_)
        {
            return Marshal.PtrToStringUni(EnumIniKey(_dm, section, file_));
        }

        internal string EnumIniKeyPwd(string section, string file_, string pwd)
        {
            return Marshal.PtrToStringUni(EnumIniKeyPwd(_dm, section, file_, pwd));
        }




     

        internal int SetPath(string path)
        {
           return SetPath(_dm,path);
        }

        internal string Ocr(int x1,int  y1,int  x2,int  y2,string  color,double sim)
        {
           return Marshal.PtrToStringUni(Ocr(_dm,x1, y1, x2, y2, color,sim));
        }

        internal int FindStr(int x1,int  y1,int  x2,int  y2,string  str,string color,double  sim, out object x, out object y)
        {
           return FindStr(_dm,x1, y1, x2, y2, str,color, sim, out x, out y);
        }

        internal int GetResultCount(string str)
        {
           return GetResultCount(_dm,str);
        }

        internal int GetResultPos(string str,int  index, out object x,out object y)
        {
           return GetResultPos(_dm,str, index, out x,out y);
        }

        internal int StrStr(string s,string  str)
        {
           return StrStr(_dm,s, str);
        }

        internal int SendCommand(string cmd)
        {
           return SendCommand(_dm,cmd);
        }

        internal int UseDict(int index)
        {
           return UseDict(_dm,index);
        }

        internal string GetBasePath(){
           return Marshal.PtrToStringUni(GetBasePath(_dm));
        }

        internal int SetDictPwd(string pwd)
        {
           return SetDictPwd(_dm,pwd);
        }

        internal string OcrInFile(int x1,int  y1,int  x2,int  y2,string  pic_name,string  color,double  sim)
        {
           return Marshal.PtrToStringUni(OcrInFile(_dm,x1, y1, x2, y2, pic_name, color, sim));
        }

        internal int Capture(int x1,int  y1,int  x2,int  y2,string  file_)
        {
           return Capture(_dm,x1, y1, x2, y2, file_);
        }

        internal int KeyPress(int vk)
        {
           return KeyPress(_dm,vk);
        }

        internal int KeyDown(int vk)
        {
           return KeyDown(_dm,vk);
        }

        internal int KeyUp(int vk)
        {
           return KeyUp(_dm,vk);
        }

        internal int LeftClick(){
           return LeftClick(_dm);
        }

        internal int RightClick(){
           return RightClick(_dm);
        }

        internal int MiddleClick(){
           return MiddleClick(_dm);
        }

        internal int LeftDoubleClick(){
           return LeftDoubleClick(_dm);
        }

        internal int LeftDown(){
           return LeftDown(_dm);
        }

        internal int LeftUp(){
           return LeftUp(_dm);
        }

        internal int RightDown(){
           return RightDown(_dm);
        }

        internal int RightUp(){
           return RightUp(_dm);
        }

        internal int MoveTo(int x,int  y)
        {
           return MoveTo(_dm,x, y);
        }

        internal int MoveR(int rx,int  ry)
        {
           return MoveR(_dm,rx, ry);
        }

        internal string GetColor(int x,int  y)
        {
           return Marshal.PtrToStringUni(GetColor(_dm,x, y));
        }

        internal string GetColorBGR(int x,int  y)
        {
           return Marshal.PtrToStringUni(GetColorBGR(_dm,x, y));
        }

        internal string RGB2BGR(string rgb_color)
        {
           return Marshal.PtrToStringUni(RGB2BGR(_dm,rgb_color));
        }

        internal string BGR2RGB(string bgr_color)
        {
           return Marshal.PtrToStringUni(BGR2RGB(_dm,bgr_color));
        }

        internal int UnBindWindow(){
           return UnBindWindow(_dm);
        }

        internal int CmpColor(int x,int  y,string  color,double  sim)
        {
           return CmpColor(_dm,x, y, color, sim);
        }

        internal int ClientToScreen(int hwnd, ref object x, ref object y)
        {
           return ClientToScreen(_dm,hwnd, ref x, ref y);
        }

        internal int ScreenToClient(int hwnd, ref object x, ref object y)
        {
           return ScreenToClient(_dm,hwnd, ref x, ref y);
        }

        internal int ShowScrMsg(int x1,int  y1,int  x2,int  y2,string  msg,string color)
        {
           return ShowScrMsg(_dm,x1, y1, x2, y2, msg,color);
        }

        internal int SetMinRowGap(int row_gap)
        {
           return SetMinRowGap(_dm,row_gap);
        }

        internal int SetMinColGap(int col_gap)
        {
           return SetMinColGap(_dm,col_gap);
        }

        internal int FindColor(int x1,int  y1,int  x2,int  y2,string  color,double sim,int  dir, out object x, out object y)
        {
           return FindColor(_dm,x1, y1, x2, y2, color,sim, dir, out x, out y);
        }

        internal string FindColorEx(int x1,int  y1,int  x2,int  y2,string color,double  sim,int  dir)
        {
           return Marshal.PtrToStringUni(FindColorEx(_dm,x1, y1, x2, y2,color, sim, dir));
        }

        internal int SetWordLineHeight(int line_height)
        {
           return SetWordLineHeight(_dm,line_height);
        }

        internal int SetWordGap(int word_gap)
        {
           return SetWordGap(_dm,word_gap);
        }

        internal int SetRowGapNoDict(int row_gap)
        {
           return SetRowGapNoDict(_dm,row_gap);
        }

        internal int SetColGapNoDict(int col_gap)
        {
           return SetColGapNoDict(_dm,col_gap);
        }

        internal int SetWordLineHeightNoDict(int line_height)
        {
           return SetWordLineHeightNoDict(_dm,line_height);
        }

        internal int SetWordGapNoDict(int word_gap)
        {
           return SetWordGapNoDict(_dm,word_gap);
        }

        internal int GetWordResultCount(string str)
        {
           return GetWordResultCount(_dm,str);
        }

        internal int GetWordResultPos(string str,int  index, out object x,out object y)
        {
           return GetWordResultPos(_dm,str, index, out x,out y);
        }

        internal string GetWordResultStr(string str,int  index)
        {
           return Marshal.PtrToStringUni(GetWordResultStr(_dm,str, index));
        }

        internal string GetWords(int x1,int  y1,int  x2,int  y2,string  color,double sim)
        {
           return Marshal.PtrToStringUni(GetWords(_dm,x1, y1, x2, y2, color,sim));
        }

        internal string GetWordsNoDict(int x1,int  y1,int  x2,int  y2,string color)
        {
           return Marshal.PtrToStringUni(GetWordsNoDict(_dm,x1, y1, x2, y2,color));
        }

        internal int SetShowErrorMsg(int show)
        {
           return SetShowErrorMsg(_dm,show);
        }

        internal int GetClientSize(int hwnd, out object width, out object height)
        {
           return GetClientSize(_dm,hwnd, out width, out height);
        }

        internal int MoveWindow(int hwnd,int  x,int  y)
        {
           return MoveWindow(_dm,hwnd, x, y);
        }

        internal string GetColorHSV(int x,int  y)
        {
           return Marshal.PtrToStringUni(GetColorHSV(_dm,x, y));
        }

        internal string GetAveRGB(int x1,int  y1,int  x2,int  y2)
        {
           return Marshal.PtrToStringUni(GetAveRGB(_dm,x1, y1, x2, y2));
        }

        internal string GetAveHSV(int x1,int  y1,int  x2,int  y2)
        {
           return Marshal.PtrToStringUni(GetAveHSV(_dm,x1, y1, x2, y2));
        }

        internal int GetForegroundWindow(){
           return GetForegroundWindow(_dm);
        }

        internal int GetForegroundFocus(){
           return GetForegroundFocus(_dm);
        }

        internal int GetMousePointWindow(){
           return GetMousePointWindow(_dm);
        }

        internal int GetPointWindow(int x,int  y)
        {
           return GetPointWindow(_dm,x, y);
        }

        internal string EnumWindow(int parent,string  title,string  class_name,int filter)
        {
           return Marshal.PtrToStringUni(EnumWindow(_dm,parent, title, class_name,filter));
        }

        internal int GetWindowState(int hwnd,int  flag)
        {
           return GetWindowState(_dm,hwnd, flag);
        }

        internal int GetWindow(int hwnd,int  flag)
        {
           return GetWindow(_dm,hwnd, flag);
        }

        internal int GetSpecialWindow(int flag)
        {
           return GetSpecialWindow(_dm,flag);
        }

        internal int SetWindowText(int hwnd,string  text)
        {
           return SetWindowText(_dm,hwnd, text);
        }

        internal int SetWindowSize(int hwnd,int  width,int  height)
        {
           return SetWindowSize(_dm,hwnd, width, height);
        }

        internal int GetWindowRect(int hwnd, out object x1, out object y1, out object x2, out object y2)
        {
           return GetWindowRect(_dm,hwnd, out x1, out y1, out x2, out y2);
        }

        internal string GetWindowTitle(int hwnd)
        {
           return Marshal.PtrToStringUni(GetWindowTitle(_dm,hwnd));
        }

        internal string GetWindowClass(int hwnd)
        {
           return Marshal.PtrToStringUni(GetWindowClass(_dm,hwnd));
        }

        internal int SetWindowState(int hwnd,int  flag)
        {
           return SetWindowState(_dm,hwnd, flag);
        }

        internal int CreateFoobarRect(int hwnd,int  x,int  y,int  w,int  h)
        {
           return CreateFoobarRect(_dm,hwnd, x, y, w, h);
        }

        internal int CreateFoobarRoundRect(int hwnd,int  x,int  y,int  w,int  h,int  rw,int  rh)
        {
           return CreateFoobarRoundRect(_dm,hwnd, x, y, w, h, rw, rh);
        }

        internal int CreateFoobarEllipse(int hwnd,int  x,int  y,int  w,int  h)
        {
           return CreateFoobarEllipse(_dm,hwnd, x, y, w, h);
        }

        internal int CreateFoobarCustom(int hwnd,int  x,int  y,string  pic,string  trans_color,double  sim)
        {
           return CreateFoobarCustom(_dm,hwnd, x, y, pic, trans_color, sim);
        }

        internal int FoobarFillRect(int hwnd,int  x1,int  y1,int  x2,int  y2,string  color)
        {
           return FoobarFillRect(_dm,hwnd, x1, y1, x2, y2, color);
        }

        internal int FoobarDrawText(int hwnd,int  x,int  y,int  w,int  h,string  text,string  color,int  align)
        {
           return FoobarDrawText(_dm,hwnd, x, y, w, h, text, color, align);
        }

        internal int FoobarDrawPic(int hwnd,int  x,int  y,string  pic,string  trans_color)
        {
           return FoobarDrawPic(_dm,hwnd, x, y, pic, trans_color);
        }

        internal int FoobarUpdate(int hwnd)
        {
           return FoobarUpdate(_dm,hwnd);
        }

        internal int FoobarLock(int hwnd)
        {
           return FoobarLock(_dm,hwnd);
        }

        internal int FoobarUnlock(int hwnd)
        {
           return FoobarUnlock(_dm,hwnd);
        }

        internal int FoobarSetFont(int hwnd,string  font_name,int  size,int  flag)
        {
           return FoobarSetFont(_dm,hwnd, font_name, size, flag);
        }

        internal int FoobarTextRect(int hwnd,int  x,int  y,int  w,int  h)
        {
           return FoobarTextRect(_dm,hwnd, x, y, w, h);
        }

        internal int FoobarPrintText(int hwnd,string  text,string  color)
        {
           return FoobarPrintText(_dm,hwnd, text, color);
        }

        internal int FoobarClearText(int hwnd)
        {
           return FoobarClearText(_dm,hwnd);
        }

        internal int FoobarTextLineGap(int hwnd,int  gap)
        {
           return FoobarTextLineGap(_dm,hwnd, gap);
        }

        internal int Play(string file_)
        {
           return Play(_dm,file_);
        }

        internal int FaqCapture(int x1,int  y1,int  x2,int  y2,int  quality,int delay,int  time)
        {
           return FaqCapture(_dm,x1, y1, x2, y2, quality,delay, time);
        }

        internal int FaqRelease(int handle)
        {
           return FaqRelease(_dm,handle);
        }

        internal string FaqSend(string server,int  handle,int  request_type,int  time_out)
        {
           return Marshal.PtrToStringUni(FaqSend(_dm,server, handle, request_type, time_out));
        }

        internal int Beep(int fre,int  delay)
        {
           return Beep(_dm,fre, delay);
        }

        internal int FoobarClose(int hwnd)
        {
           return FoobarClose(_dm,hwnd);
        }

        internal int MoveDD(int dx,int  dy)
        {
           return MoveDD(_dm,dx, dy);
        }

        internal int FaqGetSize(int handle)
        {
           return FaqGetSize(_dm,handle);
        }

        internal int LoadPic(string pic_name)
        {
           return LoadPic(_dm,pic_name);
        }

        internal int FreePic(string pic_name)
        {
           return FreePic(_dm,pic_name);
        }

        internal int GetScreenData(int x1,int  y1,int  x2,int  y2)
        {
           return GetScreenData(_dm,x1, y1, x2, y2);
        }

        internal int FreeScreenData(int handle)
        {
           return FreeScreenData(_dm,handle);
        }

        internal int WheelUp(){
           return WheelUp(_dm);
        }

        internal int WheelDown(){
           return WheelDown(_dm);
        }

        internal int SetMouseDelay(string type_,int  delay)
        {
           return SetMouseDelay(_dm,type_, delay);
        }

        internal int SetKeypadDelay(string type_,int  delay)
        {
           return SetKeypadDelay(_dm,type_, delay);
        }

        internal string GetEnv(int index,string  name)
        {
           return Marshal.PtrToStringUni(GetEnv(_dm,index, name));
        }

        internal int SetEnv(int index,string  name,string  value)
        {
           return SetEnv(_dm,index, name, value);
        }

        internal int SendString(int hwnd,string  str)
        {
           return SendString(_dm,hwnd, str);
        }

        internal int DelEnv(int index,string  name)
        {
           return DelEnv(_dm,index, name);
        }

        internal string GetPath(){
           return Marshal.PtrToStringUni(GetPath(_dm));
        }

        internal int SetDict(int index,string  dict_name)
        {
           return SetDict(_dm,index, dict_name);
        }

        internal int FindPic(int x1,int  y1,int  x2,int  y2,string  pic_name,string  delta_color,double  sim,int  dir, out object x, out object y)
        {
           return FindPic(_dm,x1, y1, x2, y2, pic_name, delta_color, sim, dir, out x, out y);
        }

        internal string FindPicEx(int x1,int  y1,int  x2,int  y2,string  pic_name,string  delta_color,double  sim,int  dir)
        {
           return Marshal.PtrToStringUni(FindPicEx(_dm,x1, y1, x2, y2, pic_name, delta_color, sim, dir));
        }

        internal int SetClientSize(int hwnd,int  width,int  height)
        {
           return SetClientSize(_dm,hwnd, width, height);
        }

        internal int ReadInt(int hwnd,string  addr,int  type_)
        {
           return ReadInt(_dm,hwnd, addr, type_);
        }

        internal int ReadFloat(int hwnd,string  addr)
        {
           return ReadFloat(_dm,hwnd, addr);
        }

        internal int ReadDouble(int hwnd,string  addr)
        {
           return ReadDouble(_dm,hwnd, addr);
        }

        internal string FindInt(int hwnd,string  addr_range,int  int_value_min,int int_value_max,int  type_)
        {
           return Marshal.PtrToStringUni(FindInt(_dm,hwnd, addr_range, int_value_min,int_value_max, type_));
        }

        internal string FindFloat(int hwnd,string  addr_range,Single  float_value_min,Single  float_value_max)
        {
           return Marshal.PtrToStringUni(FindFloat(_dm,hwnd, addr_range, float_value_min, float_value_max));
        }

        internal string FindDouble(int hwnd,string  addr_range,double  double_value_min,double  double_value_max)
        {
           return Marshal.PtrToStringUni(FindDouble(_dm,hwnd, addr_range, double_value_min, double_value_max));
        }

        internal string FindString(int hwnd,string  addr_range,string  string_value,int  type_)
        {
           return Marshal.PtrToStringUni(FindString(_dm,hwnd, addr_range, string_value, type_));
        }

        internal int GetModuleBaseAddr(int hwnd,string  module_name)
        {
           return GetModuleBaseAddr(_dm,hwnd, module_name);
        }

        internal string MoveToEx(int x,int  y,int  w,int  h)
        {
           return Marshal.PtrToStringUni(MoveToEx(_dm,x, y, w, h));
        }

        internal string MatchPicName(string pic_name)
        {
           return Marshal.PtrToStringUni(MatchPicName(_dm,pic_name));
        }

        internal int AddDict(int index,string  dict_info)
        {
           return AddDict(_dm,index, dict_info);
        }

        internal int EnterCri(){
           return EnterCri(_dm);
        }

        internal int LeaveCri(){
           return LeaveCri(_dm);
        }

        internal int WriteInt(int hwnd,string  addr,int  type_,int  v)
        {
           return WriteInt(_dm,hwnd, addr, type_, v);
        }

        internal int WriteFloat(int hwnd,string  addr,Single  v)
        {
           return WriteFloat(_dm,hwnd, addr, v);
        }

        internal int WriteDouble(int hwnd,string  addr,double  v)
        {
           return WriteDouble(_dm,hwnd, addr, v);
        }

        internal int WriteString(int hwnd,string  addr,int  type_,string  v)
        {
           return WriteString(_dm,hwnd, addr, type_, v);
        }

        internal int AsmAdd(string asm_ins)
        {
           return AsmAdd(_dm,asm_ins);
        }

        internal int AsmClear(){
           return AsmClear(_dm);
        }

        internal int AsmCall(int hwnd,int  mode)
        {
           return AsmCall(_dm,hwnd, mode);
        }

        internal int FindMultiColor(int x1,int  y1,int  x2,int  y2,string  first_color,string  offset_color,double  sim,int  dir, out object x, out object y)
        {
           return FindMultiColor(_dm,x1, y1, x2, y2, first_color, offset_color, sim, dir, out x, out y);
        }

        internal string FindMultiColorEx(int x1,int  y1,int  x2,int  y2,string  first_color,string  offset_color,double  sim,int  dir)
        {
           return Marshal.PtrToStringUni(FindMultiColorEx(_dm,x1, y1, x2, y2, first_color, offset_color, sim, dir));
        }

        internal string AsmCode(int base_addr)
        {
           return Marshal.PtrToStringUni(AsmCode(_dm,base_addr));
        }

        internal string Assemble(string asm_code,int  base_addr,int  is_upper)
        {
           return Marshal.PtrToStringUni(Assemble(_dm,asm_code, base_addr, is_upper));
        }

        internal int SetWindowTransparent(int hwnd,int  v)
        {
           return SetWindowTransparent(_dm,hwnd, v);
        }

        internal string ReadData(int hwnd,string  addr,int  len)
        {
           return Marshal.PtrToStringUni(ReadData(_dm,hwnd, addr, len));
        }

        internal int WriteData(int hwnd,string  addr,string  data)
        {
           return WriteData(_dm,hwnd, addr, data);
        }

        internal string FindData(int hwnd,string  addr_range,string  data)
        {
           return Marshal.PtrToStringUni(FindData(_dm,hwnd, addr_range, data));
        }

        internal int SetPicPwd(string pwd)
        {
           return SetPicPwd(_dm,pwd);
        }

        internal int Log(string info)
        {
           return Log(_dm,info);
        }

        internal string FindStrE(int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim)
        {
           return Marshal.PtrToStringUni(FindStrE(_dm,x1, y1, x2, y2, str, color, sim));
        }

        internal string FindColorE(int x1,int  y1,int  x2,int  y2,string  color,double  sim,int  dir)
        {
           return Marshal.PtrToStringUni(FindColorE(_dm,x1, y1, x2, y2, color, sim, dir));
        }

        internal string FindPicE(int x1,int  y1,int  x2,int  y2,string  pic_name,string  delta_color,double  sim,int  dir)
        {
           return Marshal.PtrToStringUni(FindPicE(_dm,x1, y1, x2, y2, pic_name, delta_color, sim, dir));
        }

        internal string FindMultiColorE(int x1,int  y1,int  x2,int  y2,string first_color,string  offset_color,double sim,int  dir)
        {
           return Marshal.PtrToStringUni(FindMultiColorE(_dm,x1, y1, x2, y2,first_color, offset_color,sim, dir));
        }

        internal int SetExactOcr(int exact_ocr)
        {
           return SetExactOcr(_dm,exact_ocr);
        }

        internal string ReadString(int hwnd,string  addr,int  type_,int  len)
        {
           return Marshal.PtrToStringUni(ReadString(_dm,hwnd, addr, type_, len));
        }

        internal int FoobarTextPrintDir(int hwnd,int  dir)
        {
           return FoobarTextPrintDir(_dm,hwnd, dir);
        }

        internal string OcrEx(int x1,int  y1,int  x2,int  y2,string  color,double  sim)
        {
           return Marshal.PtrToStringUni(OcrEx(_dm,x1, y1, x2, y2, color, sim));
        }

        internal int SetDisplayInput(string mode)
        {
           return SetDisplayInput(_dm,mode);
        }

        internal int GetTime(){
           return GetTime(_dm);
        }

        internal int GetScreenWidth(){
           return GetScreenWidth(_dm);
        }

        internal int GetScreenHeight(){
           return GetScreenHeight(_dm);
        }

        internal int BindWindowEx(int hwnd,string  display,string  mouse,string  keypad,string  internal_desc,int  mode)
        {
           return BindWindowEx(_dm,hwnd, display, mouse, keypad, internal_desc, mode);
        }

        internal string GetDiskSerial(){
           return Marshal.PtrToStringUni(GetDiskSerial(_dm));
        }

        internal string Md5(string str)
        {
           return Marshal.PtrToStringUni(Md5(_dm,str));
        }

        internal string GetMac(){
           return Marshal.PtrToStringUni(GetMac(_dm));
        }

        internal int ActiveInputMethod(int hwnd,string  id)
        {
           return ActiveInputMethod(_dm,hwnd, id);
        }

        internal int CheckInputMethod(int hwnd,string  id)
        {
           return CheckInputMethod(_dm,hwnd, id);
        }

        internal int FindInputMethod(string id)
        {
           return FindInputMethod(_dm,id);
        }

        internal int GetCursorPos(out object x, out object y)
        {
           return GetCursorPos(_dm,out x, out y);
        }

        internal int BindWindow(int hwnd,string  display,string  mouse,string  keypad,int  mode)
        {
           return BindWindow(_dm,hwnd, display, mouse, keypad, mode);
        }

        internal int FindWindow(string class_name,string  title_name)
        {
           return FindWindow(_dm,class_name, title_name);
        }

        internal int GetScreenDepth(){
           return GetScreenDepth(_dm);
        }

        internal int SetScreen(int width,int  height,int  depth)
        {
           return SetScreen(_dm,width, height, depth);
        }

        internal int ExitOs(int type_)
        {
           return ExitOs(_dm,type_);
        }

        internal string GetDir(int type_)
        {
           return Marshal.PtrToStringUni(GetDir(_dm,type_));
        }

        internal int GetOsType(){
           return GetOsType(_dm);
        }

        internal int FindWindowEx(int parent,string  class_name,string  title_name)
        {
           return FindWindowEx(_dm,parent, class_name, title_name);
        }

        internal int SetExportDict(int index,string  dict_name)
        {
           return SetExportDict(_dm,index, dict_name);
        }

        internal string GetCursorShape(){
           return Marshal.PtrToStringUni(GetCursorShape(_dm));
        }

        internal int DownCpu(int rate)
        {
           return DownCpu(_dm,rate);
        }

        internal string GetCursorSpot(){
           return Marshal.PtrToStringUni(GetCursorSpot(_dm));
        }

        internal int SendString2(int hwnd,string  str)
        {
           return SendString2(_dm,hwnd, str);
        }

        internal int FaqPost(string server,int  handle,int  request_type,int  time_out)
        {
           return FaqPost(_dm,server, handle, request_type, time_out);
        }

        internal string FaqFetch(){
           return Marshal.PtrToStringUni(FaqFetch(_dm));
        }

        internal string FetchWord(int x1,int  y1,int  x2,int  y2,string  color,string  word)
        {
           return Marshal.PtrToStringUni(FetchWord(_dm,x1, y1, x2, y2, color, word));
        }

        internal int CaptureJpg(int x1,int  y1,int  x2,int  y2,string  file_,int  quality)
        {
           return CaptureJpg(_dm,x1, y1, x2, y2, file_, quality);
        }

        internal int FindStrWithFont(int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim,string   font_name,int  font_size,int  flag, out object x, out object y)
        {
           return FindStrWithFont(_dm,x1, y1, x2, y2, str, color, sim,  font_name, font_size, flag, out x, out y);
        }

        internal string FindStrWithFontE(int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim,string  font_name,int  font_size,int  flag)
        {
           return Marshal.PtrToStringUni(FindStrWithFontE(_dm,x1, y1, x2, y2, str, color, sim, font_name, font_size, flag));
        }

        internal string FindStrWithFontEx(int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim,string  font_name,int  font_size,int  flag)
        {
           return Marshal.PtrToStringUni(FindStrWithFontEx(_dm,x1, y1, x2, y2, str, color, sim, font_name, font_size, flag));
        }

        internal string GetDictInfo(string str,string  font_name,int  font_size,int  flag)
        {
           return Marshal.PtrToStringUni(GetDictInfo(_dm,str, font_name, font_size, flag));
        }

        internal int SaveDict(int index,string  file_)
        {
           return SaveDict(_dm,index, file_);
        }

        internal int GetWindowProcessId(int hwnd)
        {
           return GetWindowProcessId(_dm,hwnd);
        }

        internal string GetWindowProcessPath(int hwnd)
        {
           return Marshal.PtrToStringUni(GetWindowProcessPath(_dm,hwnd));
        }

        internal int LockInput(int lock1)
        {
           return LockInput(_dm,lock1);
        }

        internal string GetPicSize(string pic_name)
        {
           return Marshal.PtrToStringUni(GetPicSize(_dm,pic_name));
        }

        internal int GetID(){
           return GetID(_dm);
        }

        internal int CapturePng(int x1,int  y1,int  x2,int  y2,string  file_)
        {
           return CapturePng(_dm,x1, y1, x2, y2, file_);
        }

        internal int CaptureGif(int x1,int  y1,int  x2,int  y2,string  file_,int  delay,int  time)
        {
           return CaptureGif(_dm,x1, y1, x2, y2, file_, delay, time);
        }

        internal int ImageToBmp(string pic_name,string  bmp_name)
        {
           return ImageToBmp(_dm,pic_name, bmp_name);
        }

        internal int FindStrFast(int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim, out object x, out object y)
        {
           return FindStrFast(_dm,x1, y1, x2, y2, str, color, sim, out x, out y);
        }

        internal string FindStrFastEx(int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim)
        {
           return Marshal.PtrToStringUni(FindStrFastEx(_dm,x1, y1, x2, y2, str, color, sim));
        }

        internal string FindStrFastE(int x1,int  y1,int  x2,int  y2,string str,string  color,double  sim)
        {
           return Marshal.PtrToStringUni(FindStrFastE(_dm,x1, y1, x2, y2,str, color, sim));
        }

        internal int EnableDisplayDebug(int enable_debug)
        {
           return EnableDisplayDebug(_dm,enable_debug);
        }

        internal int CapturePre(string file_)
        {
           return CapturePre(_dm,file_);
        }

        internal int RegEx(string code,string  Ver,string  ip)
        {
           return RegEx(_dm,code, Ver, ip);
        }

        internal string GetMachineCode(){
           return Marshal.PtrToStringUni(GetMachineCode(_dm));
        }

        internal int SetClipboard(string data)
        {
           return SetClipboard(_dm,data);
        }

        internal string GetClipboard(){
           return Marshal.PtrToStringUni(GetClipboard(_dm));
        }

        internal int GetNowDict(){
           return GetNowDict(_dm);
        }

        internal int Is64Bit(){
           return Is64Bit(_dm);
        }

        internal int GetColorNum(int x1,int  y1,int  x2,int  y2,string  color,double  sim)
        {
           return GetColorNum(_dm,x1, y1, x2, y2, color, sim);
        }

        internal string EnumWindowByProcess(string process_name,string  title,string  class_name,int  filter)
        {
           return Marshal.PtrToStringUni(EnumWindowByProcess(_dm,process_name, title, class_name, filter));
        }

        internal int GetDictCount(int index)
        {
           return GetDictCount(_dm,index);
        }

        internal int GetLastError(){
           return GetLastError(_dm);
        }

        internal string GetNetTime(){
           return Marshal.PtrToStringUni(GetNetTime(_dm));
        }

        internal int EnableGetColorByCapture(int en)
        {
           return EnableGetColorByCapture(_dm,en);
        }

        internal int CheckUAC(){
           return CheckUAC(_dm);
        }

        internal int SetUAC(int uac)
        {
           return SetUAC(_dm,uac);
        }

        internal int DisableFontSmooth(){
           return DisableFontSmooth(_dm);
        }

        internal int CheckFontSmooth(){
           return CheckFontSmooth(_dm);
        }

        internal int SetDisplayAcceler(int level)
        {
           return SetDisplayAcceler(_dm,level);
        }

        internal int FindWindowByProcess(string process_name,string  class_name,string  title_name)
        {
           return FindWindowByProcess(_dm,process_name, class_name, title_name);
        }

        internal int FindWindowByProcessId(int process_id,string  class_name,string  title_name)
        {
           return FindWindowByProcessId(_dm,process_id, class_name, title_name);
        }

        internal string ReadIni(string section,string  key,string  file_)
        {
           return Marshal.PtrToStringUni(ReadIni(_dm,section, key, file_));
        }

        internal int WriteIni(string section,string  key,string  v,string  file_)
        {
           return WriteIni(_dm,section, key, v, file_);
        }

        internal int RunApp(string path,int  mode)
        {
           return RunApp(_dm,path, mode);
        }

        internal int delay(int mis)
        {
           return delay(_dm,mis);
        }

        internal int FindWindowSuper(string spec1,int  flag1,int  type1,string  spec2,int  flag2,int  type2)
        {
           return FindWindowSuper(_dm,spec1, flag1, type1, spec2, flag2, type2);
        }

        internal string ExcludePos(string all_pos,int  type_,int  x1,int  y1,int  x2,int  y2)
        {
           return Marshal.PtrToStringUni(ExcludePos(_dm,all_pos, type_, x1, y1, x2, y2));
        }

        internal string FindNearestPos(string all_pos,int  type_,int  x,int  y)
        {
           return Marshal.PtrToStringUni(FindNearestPos(_dm,all_pos, type_, x, y));
        }

        internal string SortPosDistance(string all_pos,int  type_,int  x,int  y)
        {
           return Marshal.PtrToStringUni(SortPosDistance(_dm,all_pos, type_, x, y));
        }

        internal int FindPicMem(int x1,int  y1,int  x2,int  y2,string  pic_info,string  delta_color,double  sim,int  dir, out object x, out object y)
        {
           return FindPicMem(_dm,x1, y1, x2, y2, pic_info, delta_color, sim, dir, out x, out y);
        }

        internal string FindPicMemEx(int x1,int  y1,int  x2,int  y2,string pic_info,string  delta_color,double  sim,int  dir)
        {
           return Marshal.PtrToStringUni(FindPicMemEx(_dm,x1, y1, x2, y2,pic_info, delta_color, sim, dir));
        }

        internal string FindPicMemE(int x1,int  y1,int  x2,int  y2,string  pic_info,string  delta_color,double  sim,int dir)
        {
           return Marshal.PtrToStringUni(FindPicMemE(_dm,x1, y1, x2, y2, pic_info, delta_color, sim,dir));
        }

        internal string AppendPicAddr(string pic_info,int  addr,int  size)
        {
           return Marshal.PtrToStringUni(AppendPicAddr(_dm,pic_info, addr, size));
        }

        internal int WriteFile(string file_,string  content)
        {
           return WriteFile(_dm,file_, content);
        }

        internal int Stop(int id)
        {
           return Stop(_dm,id);
        }

        internal int SetDictMem(int index,int  addr,int  size)
        {
           return SetDictMem(_dm,index, addr, size);
        }

        internal string GetNetTimeSafe(){
           return Marshal.PtrToStringUni(GetNetTimeSafe(_dm));
        }

        internal int ForceUnBindWindow(int hwnd)
        {
           return ForceUnBindWindow(_dm,hwnd);
        }

        internal string ReadIniPwd(string section,string  key,string  file_,string  pwd)
        {
           return Marshal.PtrToStringUni(ReadIniPwd(_dm,section, key, file_, pwd));
        }

        internal int WriteIniPwd(string section,string  key,string  v,string  file_,string  pwd)
        {
           return WriteIniPwd(_dm,section, key, v, file_, pwd);
        }

        internal int DecodeFile(string file_,string  pwd)
        {
           return DecodeFile(_dm,file_, pwd);
        }

        internal int KeyDownChar(string key_str)
        {
           return KeyDownChar(_dm,key_str);
        }

        internal int KeyUpChar(string key_str)
        {
           return KeyUpChar(_dm,key_str);
        }

        internal int KeyPressChar(string key_str)
        {
           return KeyPressChar(_dm,key_str);
        }

        internal int KeyPressStr(string key_str,int  delay)
        {
           return KeyPressStr(_dm,key_str, delay);
        }

        internal int EnableKeypadPatch(int en)
        {
           return EnableKeypadPatch(_dm,en);
        }

        internal int EnableKeypadSync(int en,int  time_out)
        {
           return EnableKeypadSync(_dm,en, time_out);
        }

        internal int EnableMouseSync(int en,int  time_out)
        {
           return EnableMouseSync(_dm,en, time_out);
        }

        internal int DmGuard(int en,string  type_)
        {
           return DmGuard(_dm,en, type_);
        }

        internal int FaqCaptureFromFile(int x1,int  y1,int  x2,int  y2,string  file_,int  quality)
        {
           return FaqCaptureFromFile(_dm,x1, y1, x2, y2, file_, quality);
        }

        internal string FindIntEx(int hwnd,string  addr_range,int  int_value_min,int  int_value_max,int  type_,int  step,int  multi_thread,int  mode)
        {
           return Marshal.PtrToStringUni(FindIntEx(_dm,hwnd, addr_range, int_value_min, int_value_max, type_, step, multi_thread, mode));
        }

        internal string FindFloatEx(int hwnd,string  addr_range,Single  float_value_min,Single  float_value_max,int  step,int  multi_thread,int  mode)
        {
           return Marshal.PtrToStringUni(FindFloatEx(_dm,hwnd, addr_range, float_value_min, float_value_max, step, multi_thread, mode));
        }

        internal string FindDoubleEx(int hwnd,string  addr_range,double  double_value_min,double  double_value_max,int  step,int  multi_thread,int  mode)
        {
           return Marshal.PtrToStringUni(FindDoubleEx(_dm,hwnd, addr_range, double_value_min, double_value_max, step, multi_thread, mode));
        }

        internal string FindStringEx(int hwnd,string  addr_range,string  string_value,int  type_,int  step,int  multi_thread,int  mode)
        {
           return Marshal.PtrToStringUni(FindStringEx(_dm,hwnd, addr_range, string_value, type_, step, multi_thread, mode));
        }

        internal string FindDataEx(int hwnd,string  addr_range,string  data,int  step,int  multi_thread,int  mode)
        {
           return Marshal.PtrToStringUni(FindDataEx(_dm,hwnd, addr_range, data, step, multi_thread, mode));
        }

        internal int EnableRealMouse(int en,int  mousedelay,int  mousestep)
        {
           return EnableRealMouse(_dm,en, mousedelay, mousestep);
        }

        internal int EnableRealKeypad(int en)
        {
           return EnableRealKeypad(_dm,en);
        }

        internal int SendStringIme(string str)
        {
           return SendStringIme(_dm,str);
        }

        internal int FoobarDrawLine(int hwnd,int  x1,int  y1,int  x2,int  y2,string  color,int  style,int  width)
        {
           return FoobarDrawLine(_dm,hwnd, x1, y1, x2, y2, color, style, width);
        }

        internal string FindStrEx(int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim)
        {
           return Marshal.PtrToStringUni(FindStrEx(_dm,x1, y1, x2, y2, str, color, sim));
        }

        internal int IsBind(int hwnd)
        {
           return IsBind(_dm,hwnd);
        }

        internal int SetDisplayDelay(int t)
        {
           return SetDisplayDelay(_dm,t);
        }

        internal int GetDmCount(){
           return GetDmCount(_dm);
        }

        internal int DisableScreenSave(){
           return DisableScreenSave(_dm);
        }

        internal int DisablePowerSave(){
           return DisablePowerSave(_dm);
        }

        internal int SetMemoryHwndAsProcessId(int en)
        {
           return SetMemoryHwndAsProcessId(_dm,en);
        }

        internal int FindShape(int x1,int  y1,int  x2,int  y2,string  offset_color,double  sim,int  dir, out object x, out object y)
        {
           return FindShape(_dm,x1, y1, x2, y2, offset_color, sim, dir, out x, out y);
        }

        internal string FindShapeE(int x1,int  y1,int  x2,int  y2,string  offset_color,double  sim,int  dir)
        {
           return Marshal.PtrToStringUni(FindShapeE(_dm,x1, y1, x2, y2, offset_color, sim, dir));
        }

        internal string FindShapeEx(int x1,int  y1,int  x2,int  y2,string  offset_color,double  sim,int  dir)
        {
           return Marshal.PtrToStringUni(FindShapeEx(_dm,x1, y1, x2, y2, offset_color, sim, dir));
        }

        internal string FindStrS(int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim, out object x, out object y)
        {
           return Marshal.PtrToStringUni(FindStrS(_dm,x1, y1, x2, y2, str, color, sim, out x, out y));
        }

        internal string FindStrExS(int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim)
        {
           return Marshal.PtrToStringUni(FindStrExS(_dm,x1, y1, x2, y2, str, color, sim));
        }

        internal string FindStrFastS(int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim, out object x, out object y)
        {
           return Marshal.PtrToStringUni(FindStrFastS(_dm,x1, y1, x2, y2, str, color, sim, out x, out y));
        }

        internal string FindStrFastExS(int x1,int  y1,int  x2,int  y2,string  str,string  color,double  sim)
        {
           return Marshal.PtrToStringUni(FindStrFastExS(_dm,x1, y1, x2, y2, str, color, sim));
        }

        internal string FindPicS(int x1,int  y1,int  x2,int  y2,string  pic_name,string  delta_color,double  sim,int  dir, out object x, out object y)
        {
           return Marshal.PtrToStringUni(FindPicS(_dm,x1, y1, x2, y2, pic_name, delta_color, sim, dir, out x, out y));
        }

        internal string FindPicExS(int x1,int  y1,int  x2,int  y2,string  pic_name,string  delta_color,double  sim,int  dir)
        {
           return Marshal.PtrToStringUni(FindPicExS(_dm,x1, y1, x2, y2, pic_name, delta_color, sim, dir));
        }

        internal int ClearDict(int index)
        {
           return ClearDict(_dm,index);
        }

        internal string GetMachineCodeNoMac(){
           return Marshal.PtrToStringUni(GetMachineCodeNoMac(_dm));
        }

        internal int GetClientRect(int hwnd, out object x1, out object y1, out object x2, out object y2)
        {
           return GetClientRect(_dm,hwnd, out x1, out y1, out x2, out y2);
        }

        internal int EnableFakeActive(int en)
        {
           return EnableFakeActive(_dm,en);
        }

        internal int GetScreenDataBmp(int x1,int  y1,int  x2,int  y2, out object data, out object size)
        {
           return GetScreenDataBmp(_dm,x1, y1, x2, y2, out data, out size);
        }

        internal int EncodeFile(string file_,string  pwd)
        {
           return EncodeFile(_dm,file_, pwd);
        }

        internal string GetCursorShapeEx(int type_)
        {
           return Marshal.PtrToStringUni(GetCursorShapeEx(_dm,type_));
        }

        internal int FaqCancel(){
           return FaqCancel(_dm);
        }

        internal string IntToData(int int_value,int  type_)
        {
           return Marshal.PtrToStringUni(IntToData(_dm,int_value, type_));
        }

        internal string FloatToData(Single float_value)
        {
           return Marshal.PtrToStringUni(FloatToData(_dm,float_value));
        }

        internal string DoubleToData(double double_value)
        {
           return Marshal.PtrToStringUni(DoubleToData(_dm,double_value));
        }

        internal string StringToData(string string_value,int  type_)
        {
           return Marshal.PtrToStringUni(StringToData(_dm,string_value, type_));
        }

        internal int SetMemoryFindResultToFile(string file_)
        {
           return SetMemoryFindResultToFile(_dm,file_);
        }

        internal int EnableBind(int en)
        {
           return EnableBind(_dm,en);
        }

        internal int SetSimMode(int mode)
        {
           return SetSimMode(_dm,mode);
        }

        internal int LockMouseRect(int x1,int  y1,int  x2,int  y2)
        {
           return LockMouseRect(_dm,x1, y1, x2, y2);
        }

        internal int SendPaste(int hwnd)
        {
           return SendPaste(_dm,hwnd);
        }

        internal int IsDisplayDead(int x1,int  y1,int  x2,int  y2,int  t)
        {
           return IsDisplayDead(_dm,x1, y1, x2, y2, t);
        }

        internal int GetKeyState(int vk)
        {
           return GetKeyState(_dm,vk);
        }

        internal int CopyFile(string src_file,string  dst_file,int  over)
        {
           return CopyFile(_dm,src_file, dst_file, over);
        }

        internal int IsFileExist(string file_)
        {
           return IsFileExist(_dm,file_);
        }

        internal int DeleteFile(string file_)
        {
           return DeleteFile(_dm,file_);
        }

        internal int MoveFile(string src_file,string  dst_file)
        {
           return MoveFile(_dm,src_file, dst_file);
        }

        internal int CreateFolder(string folder_name)
        {
           return CreateFolder(_dm,folder_name);
        }

        internal int DeleteFolder(string folder_name)
        {
           return DeleteFolder(_dm,folder_name);
        }

        internal int GetFileLength(string file_)
        {
           return GetFileLength(_dm,file_);
        }

        internal string ReadFile(string file_)
        {
           return Marshal.PtrToStringUni(ReadFile(_dm,file_));
        }

        internal int WaitKey(int key_code,int  time_out)
        {
           return WaitKey(_dm,key_code, time_out);
        }

        internal int DeleteIni(string section,string  key,string  file_)
        {
           return DeleteIni(_dm,section, key, file_);
        }

        internal int DeleteIniPwd(string section,string  key,string  file_,string  pwd)
        {
           return DeleteIniPwd(_dm,section, key, file_, pwd);
        }

        internal int EnableSpeedDx(int en)
        {
           return EnableSpeedDx(_dm,en);
        }

        internal int EnableIme(int en)
        {
           return EnableIme(_dm,en);
        }

        internal int Reg(string code,string  Ver)
        {
           return Reg(_dm,code, Ver);
        }

        internal string SelectFile(){
           return Marshal.PtrToStringUni(SelectFile(_dm));
        }

        internal string SelectDirectory(){
           return Marshal.PtrToStringUni(SelectDirectory(_dm));
        }

        internal int LockDisplay(int lock1)
        {
           return LockDisplay(_dm,lock1);
        }

        internal int FoobarSetSave(int hwnd,string  file_,int  en,string   header)
        {
           return FoobarSetSave(_dm,hwnd, file_, en,  header);
        }

        internal string EnumWindowSuper(string spec1,int  flag1,int  type1,string  spec2,int  flag2,int  type2,int  sort)
        {
           return Marshal.PtrToStringUni(EnumWindowSuper(_dm,spec1, flag1, type1, spec2, flag2, type2, sort));
        }

        internal int DownloadFile(string url,string  save_file,int  timeout)
        {
           return DownloadFile(_dm,url, save_file, timeout);
        }

        internal int EnableKeypadMsg(int en)
        {
           return EnableKeypadMsg(_dm,en);
        }

        internal int EnableMouseMsg(int en)
        {
           return EnableMouseMsg(_dm,en);
        }

        internal int RegNoMac(string code,string  Ver)
        {
           return RegNoMac(_dm,code, Ver);
        }

        internal int RegExNoMac(string code,string  Ver,string  ip)
        {
           return RegExNoMac(_dm,code, Ver, ip);
        }

        internal int SetEnumWindowDelay(int delay)
        {
           return SetEnumWindowDelay(_dm,delay);
        }

        internal int FindMulColor(int x1,int  y1,int  x2,int  y2,string  color,double  sim)
        {
           return FindMulColor(_dm,x1, y1, x2, y2, color, sim);
        }

        internal string GetDict(int index,int  font_index)
        {
           return Marshal.PtrToStringUni(GetDict(_dm,index, font_index));
        }





        #region 继承释放接口方法
        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        internal void Close()
        {
            Dispose();
        }

        ~CDmSoft()
        {
            //必须为false
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                // 清理托管资源
                //if (managedResource != null)
                //{
                //    managedResource.Dispose();
                //    managedResource = null;
                //}
            }
            // 清理非托管资源
            if (_dm != IntPtr.Zero)
            {
                UnBindWindow();
                _dm = IntPtr.Zero;
                int ret = FreeDM();
            }
            //让类型知道自己已经被释放
            disposed = true;
        }
        #endregion
    }
}
