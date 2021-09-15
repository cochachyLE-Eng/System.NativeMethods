using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace System.NativeMethods.Drawing
{
    public class Cursor
    {
        /// <summary>
        /// <para>Obtener la position de Cursor/Mouse.</para>
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
    }
}
