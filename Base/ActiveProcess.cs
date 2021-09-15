namespace System.NativeMethods.Base
{       
    public class ActiveProcess:IActiveProcess
    {
        /// <summary>        
        /// <b>Resumen:</b>
        ///     Obtiene el identificador de la sesión de Terminal Services para el proceso asociado.<br/>
        /// <b>Devuelve:</b>
        ///     Identificador de la sesión de Terminal Services para el proceso asociado.                      
        /// </summary>
        public int SessionId { get; private set; }
        /// <summary>        
        /// <b>Resumen:</b>
        ///     Obtiene un valor que indica si la interfaz de usuario del proceso está respondiendo.<br/>
        /// <b>Devuelve:</b>
        ///     Es true si la interfaz de usuario del proceso asociado está respondiendo al sistema;
        ///     de lo contrario, es false.                       
        /// </summary>
        public bool Responding { get; private set; }
        /// <summary>        
        /// <b>Resumen:</b>
        ///     Obtiene el nombre del proceso.<br/>
        /// <b>Devuelve:</b>
        ///     Nombre que el sistema usa para identificar el proceso ante el usuario.                        
        /// </summary>
        public string ProcessName { get; private set; }
        /// <summary>        
        /// <b>Resumen:</b>
        ///     Obtiene el título de la ventana principal del proceso.<br/>
        /// <b>Devuelve:</b>
        ///     Título de la ventana principal del proceso.        
        /// </summary>
        public string MainWindowTitle { get; private set; }
        /// <summary>        
        /// <b>Resumen:</b>
        ///     Obtiene el nombre del equipo en el que se está ejecutando el proceso asociado.<br/>
        /// <b>Devuelve:</b>
        ///     Nombre del equipo en el que se está ejecutando el proceso asociado.                        
        /// </summary>
        public string MachineName { get; private set; }
        /// <summary>        
        /// <b>Resumen:</b>
        ///     Obtiene el identificador único del proceso asociado.<br/>
        /// <b>Devuelve:</b>
        ///     Identificador único generado por el sistema del proceso al que hace referencia
        ///     esta instancia de System.Diagnostics.Process.               
        /// </summary>
        public int Id { get; private set; }
        /// <summary>        
        /// <b>Resumen:</b>
        ///     Obtiene el identificador nativo del proceso asociado.<br/>
        /// <b>Devuelve:</b>
        ///    Identificador que el sistema operativo asignó al proceso asociado cuando este
        ///    se inició. El sistema usa este identificador para hacer un seguimiento de los
        ///    atributos del proceso.            
        /// </summary>
        public IntPtr Handle { get; private set; }
        /// <summary>        
        /// <b>Resumen:</b>
        ///    Obtiene el identificador de ventana de la ventana principal del proceso asociado.<br/>
        /// <b>Devuelve:</b>
        ///    Identificador de ventana generado por el sistema para la ventana principal del
        ///    proceso asociado.                
        /// </summary>
        public IntPtr MainWindowHandle { get; private set; }
        /// <summary>        
        /// <b>Resumen:</b>
        ///    Obtiene el tamaño de memoria física, expresado en bytes, que se asignó para el
        ///    proceso asociado.<br/>
        /// <b>Devuelve:</b>
        ///    Tamaño de memoria física, expresado en bytes, que se asignó para el proceso asociado.                
        /// </summary>
        public long WorkingSet64 { get; private set; }

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
