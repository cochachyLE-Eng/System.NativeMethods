using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vaetech.NativeMethods.Mouse
{
    public static class Cursor
    {        
        /// <summary>
        /// <para>Get the position of Cursor/Mouse.</para>
        /// <example>        
        /// <b>Method 1:</b><br/>
        /// Point defPnt = new Point();<br/>
        /// GetCursorPos(ref defPnt);        
        /// </example>
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("user32.dll")]
        internal static extern BOOL SetCursorPos([In] int x, [In] int y);

        [DllImport("user32.dll")]
        internal static extern bool ShowCursor(bool bShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ClipCursor([In] ref RECT rcClip);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ClipCursor([In] IntPtr rcClip);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetClipCursor([Out] out RECT rcClip);
        public enum BOOL : uint
        {
            False = 0,
            True = 1,
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator RECT(in Rectangle r)
            {
                return new RECT
                {
                    Left = r.Left,
                    Top = r.Top,
                    Right = r.Right,
                    Bottom = r.Bottom
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static implicit operator Rectangle(in RECT r)
            {
                return Rectangle.FromLTRB(r.Left, r.Top, r.Right, r.Bottom);
            }
        }
    }
}
