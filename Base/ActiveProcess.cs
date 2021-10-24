using System;

namespace Vaetech.NativeMethods.Base
{       
    public class ActiveProcess:IActiveProcess
    {
        /// <summary>        
        /// <b>Summary:</b>
        ///     Gets the ID of the Terminal Services session for the associated process.<br/>
        /// <b>Returns:</b>
        ///     Terminal Services session identifier for the associated process.                      
        /// </summary>
        public int SessionId { get; private set; }
        /// <summary>        
        /// <b>Summary:</b>
        ///     Gets a value that indicates whether the process UI is responding.<br/>
        /// <b>Returns:</b>
        ///     True if the associated process UI is responding to the system; otherwise, it is false.                       
        /// </summary>
        public bool Responding { get; private set; }
        /// <summary>        
        /// <b>Summary:</b>
        ///     Gets the name of the process.<br/>
        /// <b>Returns:</b>
        ///     Name that the system uses to identify the process to the user.                        
        /// </summary>
        public string ProcessName { get; private set; }
        /// <summary>        
        /// <b>Summary:</b>
        ///     Gets the title of the main process window.<br/>
        /// <b>Returns:</b>
        ///     Title of the main window of the process.        
        /// </summary>
        public string MainWindowTitle { get; private set; }
        /// <summary>        
        /// <b>Summary:</b>
        ///     Gets the name of the computer on which the associated process is running.<br/>
        /// <b>Returns:</b>
        ///     Name of the computer on which the associated process is running.                        
        /// </summary>
        public string MachineName { get; private set; }
        /// <summary>        
        /// <b>Summary:</b>
        ///     Gets the unique identifier of the associated process.<br/>
        /// <b>Returns:</b>
        ///     System-generated unique identifier of the process referenced by this instance of System.Diagnostics.Process.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>        
        /// <b>Summary:</b>
        ///     Gets the native identifier of the associated process.<br/>
        /// <b>Returns:</b>
        ///    The identifier that the operating system assigned to the associated process when it started. 
        ///    The system uses this identifier to keep track of the attributes of the process.         
        /// </summary>
        public IntPtr Handle { get; private set; }
        /// <summary>        
        /// <b>Summary:</b>
        ///    Gets the window handle of the main window of the associated process.<br/>
        /// <b>Returns:</b>
        ///    System generated window identifier for the main window of the associated process.                
        /// </summary>
        public IntPtr MainWindowHandle { get; private set; }
        /// <summary>        
        /// <b>Summary:</b>
        ///    Gets the size of physical memory, expressed in bytes, that was allocated for the associated process.<br/>
        /// <b>Returns:</b>
        ///    Physical memory size, expressed in bytes, that was allocated for the associated process.                
        /// </summary>
        public long WorkingSet64 { get; private set; }

        public void SetActiveProcess(int pid) => SetActiveProcess(System.Diagnostics.Process.GetProcessById(pid));
        public void SetActiveProcess(System.Diagnostics.Process process)
        {
            SessionId = process.SessionId;
            Responding = process.Responding;
            ProcessName = process.ProcessName;
            MainWindowTitle = process.MainWindowTitle;
            MachineName = process.MachineName;
            Id = process.Id;
            Handle = process.Handle;
            MainWindowHandle = process.MainWindowHandle;
            WorkingSet64 = process.WorkingSet64;
        }
    }
    public interface IActiveProcess
    {        
        int SessionId { get; }               
        bool Responding { get; }        
        string ProcessName { get; }        
        string MainWindowTitle { get; }        
        string MachineName { get; }        
        int Id { get; }        
        IntPtr Handle { get; }      
        IntPtr MainWindowHandle { get; }        
        long WorkingSet64 { get; }

        void SetActiveProcess(System.Diagnostics.Process process);
    }
}
