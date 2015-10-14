using System;
using System.Runtime .InteropServices ;
using System.Security; 
using System.Security.Permissions;
using System.Text ;
namespace ProcessOrder.Util
{
	public class API 
	{ 
		#region Callback Delegate Section
		public delegate bool EnumDesktopWindowsDelegate(IntPtr hWnd, uint lParam);
		#endregion
     
		public const int MEM_COMMIT = 0x1000; 
		public const int OPEN_PROCESS_ALL = 0x1f0fff; 
		public const int PAGE_READWRITE = 4; 
		public const int PROCESS_CREATE_THREAD = 2; 
		public const int PROCESS_HEAP_ENTRY_BUSY = 4; 
		public const int PROCESS_VM_OPERATION = 8; 
		public const int PROCESS_VM_READ = 0x100; 
		public const int PROCESS_VM_WRITE = 0x20; 
		public const int WM_KEYDOWN = 0x100; 
		public const int WM_KEYUP = 0x101; 
		public const int WM_LBUTTONDBLCLK = 0x203; 
		public const int WM_LBUTTONDOWN = 0x201; 
		public const int WM_LBUTTONUP = 0x202; 
		public const int WM_MBUTTONDBLCLK = 0x209; 
		public const int WM_MBUTTONDOWN = 0x207; 
		public const int WM_MBUTTONUP = 520; 
		public const int WM_MOUSEMOVE = 0x200; 
		public const int WM_RBUTTONDBLCLK = 0x206; 
		public const int WM_RBUTTONDOWN = 0x204; 
		public const int WM_RBUTTONUP = 0x205; 
		public const int WM_SYSKEYDOWN = 260; 
		public const int WM_SYSKEYUP = 0x105; 
		public const int WS_SHOWNORMAL = 1;
		public const uint INFINITE=0xFFFFFFFF;
		public const int MEM_RELEASE= 0x8000   ;
		public const int WM_VSCROLL = 0x115;
        public const int SB_LINEDOWN =0x1;
		public const int WM_CHAR = 0x102;
		public const int WM_CLOSE = 0x10;
		public const int MK_LBUTTON = 0x1;
		 public const uint GW_HWNDNEXT = 0x02;
		public const uint BM_CLICK = 0xF5;
		public const uint EM_REPLACESEL = 0xC2;

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public readonly IntPtr HFILE_ERROR = new IntPtr(-1);


        [DllImport("user32.dll")]
        private static extern bool SetWindowText(IntPtr hwnd, string lPstring);

        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);   //判断文件是否被占用
        //---------------事例-----------------//
        #region
        //    string vFileName = @"c:\temp\temp.bmp";
        //if (!File.Exists(vFileName))
        //{
        //    MessageBox.Show("文件都不存在，你就不要拿来耍了");
        //    return;
        //}
        //IntPtr vHandle = _lopen(vFileName, OF_READWRITE | OF_SHARE_DENY_NONE);
        //if (vHandle == HFILE_ERROR)
        //{
        //    MessageBox.Show("文件被占用！");
        //    return;
        //}
        //CloseHandle(vHandle);
        //MessageBox.Show("没有被占用！");
        #endregion
        //------------------------------------//

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
	
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindow(IntPtr hWnd);
	
		[DllImport("user32.dll")]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder buf, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDesktopWindowsDelegate lpEnumCallbackFunction, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern int GetWindowText(
			int hWnd,        // handle to window or control with text
			string lpString,  // address of buffer for text
			int nMaxCount     // maximum number of characters to copy
			);

		[DllImport("Kernel32.dll")] 
		public static extern bool VirtualFreeEx(int hProcess, int lpAddress,int dwSize,int dwFreeType); 


		[DllImport("Kernel32.dll")] 
		public static extern  int WaitForSingleObject(int hHandle,uint dwMilliseconds );
		[DllImport("Kernel32.dll")] 
		public static extern  int WaitForSingleObject(IntPtr hHandle,uint dwMilliseconds );

		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)] 
		public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam); 
		[DllImport("kernel32.dll")] 
		public static extern int CloseHandle(int hObject); 
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)] 
		public static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, int bInitialOwner, string lpName); 
		[DllImport("kernel32")] 
		public static extern int CreateRemoteThread(int hProcess, int lpThreadAttributes, int dwStackSize, int lpStartAddress, int lpParameter, int dwCreationFlags, ref int lpThreadId); 
		[DllImport("user32.dll")] 
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName); 
		[DllImport("user32.dll")] 
		public static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string lpszClass, string lpszWindow); 
		[DllImport("user32.dll")] 
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow); 
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)] 
		public static extern bool FreeLibrary(IntPtr dllPointer); 
		[DllImport("Kernel32")] 
		public static extern int FreeLibrary(int handle); 
		[DllImport("user32.dll")] 
		public static extern int GetClassName(int hwnd, ref StringBuilder lpClassName, int nMaxCount); 
		[DllImport("user32.dll")] 
		public static extern int GetCursorPos(out POINTAPI lpPoint); 
		[DllImport("user32.dll")] 
		public static extern int GetDC(int hwnd); 
		[DllImport("user32.dll")] 
		public static extern IntPtr GetForegroundWindow(); 
		[DllImport("user32.dll")] 
		public static extern int GetKeyboardState(byte[] pbKeyState); 
		public static IntPtr GetLocalWindow() 
		{ 
			POINTAPI point; 
			GetCursorPos(out point); 
			return WindowFromPoint(point); 
		}

		[DllImport("kernel32.dll")] 
		public static extern int GetModuleHandle(string lpModuleName); 
		[DllImport("gdi32.dll")] 
		public static extern int GetPixel(int hdc, int x, int y); 
		[DllImport("kernel32")] 
		public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath); 
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)] 
		public static extern IntPtr GetProcAddress(IntPtr dllPointer, string functionName); 
		[DllImport("Kernel32")] 
		public static extern int GetProcAddress(int handle, String funcname); 
		[DllImport("user32.dll")] 
		public static extern int GetWindowThreadProcessId(int hwnd, ref int lpdwProcessId); 
		[DllImport("user32.dll")] 
		public static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessId); 
		[DllImport("kernel32.dll", EntryPoint = "LoadLibraryA", CharSet = CharSet.Ansi)] 
		public static extern int LoadLibrary(string lpLibFileName); 
		//[DllImport("Kernel32.dll")] 
		//public static extern int LoadLibrary(String funcname); 
		[DllImport("kernel32.dll", EntryPoint = "LoadLibrarEx")] 
		public static extern IntPtr LoadLibraryEx(string dllFilePath, IntPtr hFile, uint dwFlags); 
		[DllImport("user32.dll")] 
		public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo); 
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)] 
		public static extern IntPtr OpenMutex(uint dwDesiredAccess, int bInheritHandle, string lpName); 
		[DllImport("kernel32.dll")] 
		public static extern int OpenProcess(int dwDesiredAccess, int bInheritHandle, int dwProcessId); 
		[DllImport("kernel32.dll")] 
		public static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId); 
		[DllImport("user32.dll", CharSet = CharSet.Unicode)] 
		public static extern IntPtr PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, ref int lpBaseAddress, ref int lpBuffer, int nSize, ref int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, out byte lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, out double lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, uint nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, out int lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, out float lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, out string lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int nSize, ref int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, out char lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, ref int lpBuffer, int nSize, ref int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, char lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, IntPtr lpBaseAddress, out int lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, IntPtr lpBaseAddress, ref int lpBuffer, int nSize, ref int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, IntPtr lpBaseAddress, int lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, out IntPtr lpBaseAddress, out int lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("Kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, byte lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(int hProcess, ref IntPtr lpBaseAddress, ref int lpBuffer, int nSize, ref int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, ref int lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int ReadProcessMemory(IntPtr hProcess, ref int lpBaseAddress, ref int lpBuffer, int nSize, int lpNumberOfBytesWritten); 
		[DllImport("user32.dll")] 
		public static extern int ReleaseDC(int hwnd, int hdc); 
		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, string lParam);

		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)] 
		public static extern int SendMessage(int hwnd, int wMsg, IntPtr wParam, IntPtr lParam); 
		[DllImport("user32.dll", CharSet = CharSet.Auto)] 
		public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam); 
		[DllImport("user32.dll", CharSet = CharSet.Auto)] 
		public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, string lParam); 
		[DllImport("user32.dll")] 
		public static extern int SetCursorPos(int x, int y); 
		[DllImport("user32.dll")] 
		public static extern void SetForegroundWindow(int hwnd); 
		[DllImport("user32.dll")] 
		public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags); 
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)] 
		public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, int hInstance, int threadId); 
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)] 
		public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId); 
		[DllImport("user32.dll")] 
		public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState); 
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)] 
		public static extern bool UnhookWindowsHookEx(int idHook); 
		[DllImport("Kernel32.dll")] 
		public static extern int VirtualAllocEx(int hProcess, int lpAddress, int dwSize, int flAllocationType, int flProtect); 
		[DllImport("Kernel32.dll")] 
		public static extern int VirtualAllocEx(IntPtr hProcess, int lpAddress, int dwSize, short flAllocationType, short flProtect); 
		[DllImport("Kernel32.dll")] 
		public static extern uint VirtualAllocEx(IntPtr hProcess, uint lpAddress, uint dwSize, uint flAllocationType, uint flProtect); 
		[DllImport("kernel32.dll")] 
		public static extern int VirtualProtectEx(int hProcess, int lpAddress, int dwSize, int flNewProtect, int lpflOldProtect); 
		[DllImport("kernel32.dll")] 
		public static extern int VirtualProtectEx(int hProcess, ref int lpAddress, int dwSize, int flNewProtect, ref int lpflOldProtect); 
		[DllImport("user32.dll")] 
		public static extern IntPtr WindowFromPoint(POINTAPI lpPoint); 
		[DllImport("kernel32")] 
		public static extern long WritePrivateProfileString(string section, string key, string val, string filePath); 
		[DllImport("kernel32.dll")] 
		public static extern int WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int WriteProcessMemory(int hProcess, ref int lpBaseAddress, ref char[] lpBuffer, int nSize, ref int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int WriteProcessMemory(int hProcess, ref int lpBaseAddress, ref int lpBuffer, int nSize, ref int lpNumberOfBytesWritten); 
		[DllImport("kernel32.dll")] 
		public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, int size, out IntPtr lpNumberOfBytesWritten); 
		[DllImport("User32.dll")] 
		public static extern bool SetForegroundWindow(IntPtr hWnd); 
		[DllImport("User32.dll")] 
		public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow); 
		[DllImport("kernel32.dll")] 
		public static extern IntPtr CreateFile(string lpFileName, int dwDesAccess, int dwShareMode, SecurityAttribute lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile); 
		[DllImport("gdi32.dll")] 
		public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, Int64 lpInitData); 
		[DllImport("gdi32.dll")] 
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc); 
		[DllImport("gdi32.dll")] 
		public static extern int GetDeviceCaps(IntPtr hdc, GetDeviceCapsIndex nIndex); 
		[DllImport("gdi32.dll")] 
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight); 
		[DllImport("gdi32.dll")] 
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj); 
		[DllImport("gdi32.dll")] 
		public static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, UInt32 dwRop); 
		[DllImport("gdi32.dll")] 
		public static extern int DeleteDC(IntPtr hdc); 
		[DllImport("user32.dll")] 
		public static extern int EnumWindows(CallBack x, int y);

		public delegate int HookProc(int nCode, int wParam, IntPtr lParam); 
		public delegate bool CallBack(int hwnd, int lParam);

		public enum HookType 
		{ 
			WH_JOURNALRECORD, 
			WH_JOURNALPLAYBACK, 
			WH_KEYBOARD, 
			WH_GETMESSAGE, 
			WH_CALLWNDPROC, 
			WH_CBT, 
			WH_SYSMSGFILTER, 
			WH_MOUSE, 
			WH_HARDWARE, 
			WH_DEBUG, 
			WH_SHELL, 
			WH_FOREGROUNDIDLE, 
			WH_CALLWNDPROCRET, 
			WH_KEYBOARD_LL, 
			WH_MOUSE_LL 
		} 
		public enum uType 
		{ 
			MB_ICONASTERISK 
		} 
		public enum GetDeviceCapsIndex 
		{ 
			ASPECTX, 
			ASPECTXY, 
			ASPECTY, 
			BITSPIXEL, 
			CLIPCAPS, 
			COLORRES, 
			CC_CHORD, 
			CC_CIRCLES, 
			CC_ELLIPSES, 
			HORZSIZE, 
			VERTSIZE, 
			HORZRES, 
			VERTRES, 
			LOGPIXELSX, 
			LOGPIXELSY, 
			PLANES,

		}

		[StructLayout(LayoutKind.Sequential)] 
			public class KeyboardHookStruct 
		{ 
			public int vkCode; 
			public int scanCode; 
			public int flags; 
			public int time; 
			public int dwExtraInfo; 
		}

		[StructLayout(LayoutKind.Sequential)] 
			public class MouseHookStruct 
		{ 
			public API.POINTAPI pt; 
			public int hwnd; 
			public int wHitTestCode; 
			public int dwExtraInfo; 
		}

		[StructLayout(LayoutKind.Sequential)] 
			public struct POINTAPI 
		{ 
			public int x; 
			public int y; 
		} 
	} 


}
