using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Vaetech.NativeMethods.Windows
{
    public static class Form
    {
        /// <summary>
        /// <para>Changes the position and dimensions of the specified window.</para>
        /// <example>        
        /// <b>Method 1:</b><br/>
        /// IntPtr hwnd = GetForegroundWindow();<br/>
        /// MoveWindow(hwnd, 600, 600, 600, 600, true);<br/><br/>   
        /// 
        /// <b>Method 2:</b><br/>        
        /// System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById((int)Id);<br/>
        /// MoveWindow(p.MainWindowHandle, 600, 600, 600, 600, true);<br/><br/>
        /// 
        /// <b>Method 3:</b><br/>
        /// MoveWindow(System.NativeMethods.NativeMethod.ActiveProcess.MainWindowHandle, 600, 600, 600, 600, true);<br/>
        /// </example>
        /// </summary>
        /// <param name="hWnd" type="IntPtr">A handle to the window.</param>
        /// <param name="X" type="int">The new position of the left side of the window.</param>
        /// <param name="Y" type="int">The new position of the top of the window.</param>
        /// <param name="nWidth" type="int">The new width of the window.</param>
        /// <param name="nHeight" type="int">The new height of the window.</param>
        /// <param name="bRepaint" type="bool">Indicates whether the window is to be repainted.</param>

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        /// <summary>
        /// Retrieves the ID of the thread that created the specified window and optionally the ID of the process that created the window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="ProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);
        /// <summary>
        /// Retrieves a handle to the foreground window (the window the user is currently working with).
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        /// <summary>
        /// Brings the thread that created the specified window to the foreground and activates the window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        /// <example>
        /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~(Method 1)
        /// System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById((int)Id);
        /// SetForegroundWindow(p.MainWindowHandle);
        /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~(Method 2)
        /// IntPtr hwnd = GetForegroundWindow();
        /// SetForegroundWindow(hwnd);
        /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~(Method 3)
        /// SetForegroundWindow(System.NativeMethods.NativeMethod.ActiveProcess.MainWindowHandle);
        /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        /// </example>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Sets the active process in the static class Vaetech.NativeMethods.NativeMethod.ActiveProcess.
        /// </summary>
        public static void SetActiveProcess(int pid) => NativeMethod.ActiveProcess.SetActiveProcess(pid);
        public static void SetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            GetWindowThreadProcessId(hwnd, out uint pid);
            NativeMethod.ActiveProcess.SetActiveProcess(System.Diagnostics.Process.GetProcessById((int)pid));
        }        

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public struct ActiveForm
        {
            public int Id;
            public string Caption;
        }
        /// <summary>
        /// <b>Returns:</b> Get ID and title of the active window.
        /// </summary>        
        public static ActiveForm GetActiveWindow()
        {
            ActiveForm activeForm = new ActiveForm();
            const int nChars = 256; IntPtr handle;
            StringBuilder Buff = new StringBuilder(nChars);

            handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                activeForm.Id =(Int32) handle;
                activeForm.Caption = Buff.ToString();
            }
            return activeForm;
        }
    }
}
