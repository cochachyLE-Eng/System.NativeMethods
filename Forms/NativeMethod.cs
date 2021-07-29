using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace System.NativeMethods.Forms
{
    public class NativeMethod
    {
        /// <summary>
        /// Cambia la posición y las dimensiones de la ventana especificada.
        /// </summary>
        /// <param name="hWnd" type="IntPtr">A handle to the window.</param>
        /// <param name="X" type="int">The new position of the left side of the window.</param>
        /// <param name="Y" type="int">The new position of the top of the window.</param>
        /// <param name="nWidth" type="int">The new width of the window.</param>
        /// <param name="nHeight" type="int">The new height of the window.</param>
        /// <param name="bRepaint" type="bool">Indicates whether the window is to be repainted.</param>
        /// <example>
        /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~(Method 1)
        /// IntPtr hwnd = GetForegroundWindow();
        /// MoveWindow(hwnd, 600, 600, 600, 600, true);
        /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~(Method 2)
        /// System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById((int)Id);
        /// MoveWindow(p.MainWindowHandle, 600, 600, 600, 600, true);
        /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~(Method 3)        
        /// MoveWindow(System.NativeMethods.NativeMethod.ActiveProcess.MainWindowHandle, 600, 600, 600, 600, true);
        /// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        /// </example>

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        /// <summary>
        /// Recupera el identificador del hilo que creó la ventana especificada y, opcionalmente, el identificador del proceso que creó la ventana.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="ProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]        
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);
        /// <summary>
        /// Recupera un identificador de la ventana de primer plano (la ventana con la que el usuario está trabajando actualmente). 
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        /// <summary>
        /// Trae el hilo que creó la ventana especificada al primer plano y activa la ventana.
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
        /// Establece el proceso activo en la clase estatica System.NativeMethods.NativeMethod.ActiveProcess.
        /// </summary>
        public static void SetActiveProcess() {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            System.NativeMethods.NativeMethod.ActiveProcess.SetActiveProcess(System.Diagnostics.Process.GetProcessById((int)pid));
        }
        /// <summary>
        /// Establece el proceso activo en la clase estatica System.NativeMethods.NativeMethod.ActiveProcess.
        /// </summary>
        public static void SetActiveProcess(int pid) => System.NativeMethods.NativeMethod.ActiveProcess.SetActiveProcess(System.Diagnostics.Process.GetProcessById((int)pid));
    }
}
