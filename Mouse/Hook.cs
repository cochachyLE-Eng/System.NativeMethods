using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using Vaetech.Data.ContentResult.Events;

namespace Vaetech.NativeMethods.Mouse
{
    public static class Hook
    {
        #region Win32
        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;        
        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
                {
                    if (MouseDownEventHandler != null)
                    { 
                        MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                        OnMouseDown(hookStruct);
                        //Debug.WriteLine("Mouse down: {0}", hookStruct.pt.x + ", " + hookStruct.pt.y);
                    }
                }
                else if (MouseMessages.WM_LBUTTONUP == (MouseMessages)wParam)
                {
                    if (MouseUpEventHandler != null)
                    {
                        MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));                        
                        OnMouseUp(hookStruct);
                        //Debug.WriteLine("mouse up: {0}", hookStruct.pt.x + ", " + hookStruct.pt.y);                        
                    }
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private const int WH_MOUSE_LL = 14;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);        
        #endregion

        #region Methods
        public static IntPtr GetWindowsHook() => _hookID = SetHook(_proc);
        public static void RunMouseEvents() => UnhookWindowsHookEx(_hookID);
        public static void FinalizeEvents()
        {
            MouseDownEventHandler = null;
            MouseUpEventHandler = null;
        }
        #endregion

        #region Events
        public static DynamicEventHandler<MSLLHOOKSTRUCT> MouseDownEventHandler;
        public static DynamicEventHandler<MSLLHOOKSTRUCT> MouseUpEventHandler;

        private static void OnMouseDown(MSLLHOOKSTRUCT mSLLHOOKSTRUCT)
        {
            if (MouseDownEventHandler != null)            
                MouseDownEventHandler(null,new DynamicEventArgs<MSLLHOOKSTRUCT>(mSLLHOOKSTRUCT));            
        }
        private static void OnMouseUp(MSLLHOOKSTRUCT mSLLHOOKSTRUCT)
        {
            if (MouseUpEventHandler != null)            
                MouseUpEventHandler(null, new DynamicEventArgs<MSLLHOOKSTRUCT>(mSLLHOOKSTRUCT));            
        }
        #endregion
    }
}
