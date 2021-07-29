namespace System.NativeMethods
{       
    public class ActiveProcess:IActiveProcess
    {        
        // Resumen:
        //     Obtiene el identificador de la sesión de Terminal Services para el proceso asociado.
        //
        // Devuelve:
        //     Identificador de la sesión de Terminal Services para el proceso asociado.      
        public int SessionId { get; private set; }        
        // Resumen:
        //     Obtiene un valor que indica si la interfaz de usuario del proceso está respondiendo.
        //
        // Devuelve:
        //     Es true si la interfaz de usuario del proceso asociado está respondiendo al sistema;
        //     de lo contrario, es false.               
        public bool Responding { get; private set; }
        //
        // Resumen:
        //     Obtiene el nombre del proceso.
        //
        // Devuelve:
        //     Nombre que el sistema usa para identificar el proceso ante el usuario.
        //        
        public string ProcessName { get; private set; }
        // Resumen:
        //     Obtiene el título de la ventana principal del proceso.
        //
        // Devuelve:
        //     Título de la ventana principal del proceso.
        public string MainWindowTitle { get; private set; }        
        //
        // Resumen:
        //     Obtiene el nombre del equipo en el que se está ejecutando el proceso asociado.
        //
        // Devuelve:
        //     Nombre del equipo en el que se está ejecutando el proceso asociado.
        //
        // Excepciones:
        //   T:System.InvalidOperationException:
        //     No hay ningún proceso asociado a este objeto System.Diagnostics.Process.        
        public string MachineName { get; private set; }        
        // Resumen:
        //     Obtiene el identificador único del proceso asociado.
        //
        // Devuelve:
        //     Identificador único generado por el sistema del proceso al que hace referencia
        //     esta instancia de System.Diagnostics.Process.       
        public int Id { get; private set; }                    
        // Resumen:
        //     Obtiene el identificador nativo del proceso asociado.
        //
        // Devuelve:
        //     Identificador que el sistema operativo asignó al proceso asociado cuando este
        //     se inició. El sistema usa este identificador para hacer un seguimiento de los
        //     atributos del proceso.        
        public IntPtr Handle { get; private set; }                                 
                       
        // Resumen:
        //     Obtiene el identificador de ventana de la ventana principal del proceso asociado.
        //
        // Devuelve:
        //     Identificador de ventana generado por el sistema para la ventana principal del
        //     proceso asociado.        
        public IntPtr MainWindowHandle { get; private set; }
        // Resumen:
        //     Obtiene el tamaño de memoria física, expresado en bytes, que se asignó para el
        //     proceso asociado.
        //
        // Devuelve:
        //     Tamaño de memoria física, expresado en bytes, que se asignó para el proceso asociado.        
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
        // Resumen:
        //     Obtiene el identificador de la sesión de Terminal Services para el proceso asociado.
        //
        // Devuelve:
        //     Identificador de la sesión de Terminal Services para el proceso asociado.      
        int SessionId { get; }
        // Resumen:
        //     Obtiene un valor que indica si la interfaz de usuario del proceso está respondiendo.
        //
        // Devuelve:
        //     Es true si la interfaz de usuario del proceso asociado está respondiendo al sistema;
        //     de lo contrario, es false.               
        bool Responding { get; }
        //
        // Resumen:
        //     Obtiene el nombre del proceso.
        //
        // Devuelve:
        //     Nombre que el sistema usa para identificar el proceso ante el usuario.
        //        
        string ProcessName { get; }
        // Resumen:
        //     Obtiene el título de la ventana principal del proceso.
        //
        // Devuelve:
        //     Título de la ventana principal del proceso.
        string MainWindowTitle { get; }
        //
        // Resumen:
        //     Obtiene el nombre del equipo en el que se está ejecutando el proceso asociado.
        //
        // Devuelve:
        //     Nombre del equipo en el que se está ejecutando el proceso asociado.
        //
        // Excepciones:
        //   T:System.InvalidOperationException:
        //     No hay ningún proceso asociado a este objeto System.Diagnostics.Process.        
        string MachineName { get; }
        // Resumen:
        //     Obtiene el identificador único del proceso asociado.
        //
        // Devuelve:
        //     Identificador único generado por el sistema del proceso al que hace referencia
        //     esta instancia de System.Diagnostics.Process.       
        int Id { get; }
        // Resumen:
        //     Obtiene el identificador nativo del proceso asociado.
        //
        // Devuelve:
        //     Identificador que el sistema operativo asignó al proceso asociado cuando este
        //     se inició. El sistema usa este identificador para hacer un seguimiento de los
        //     atributos del proceso.        
        IntPtr Handle { get; }

        // Resumen:
        //     Obtiene el identificador de ventana de la ventana principal del proceso asociado.
        //
        // Devuelve:
        //     Identificador de ventana generado por el sistema para la ventana principal del
        //     proceso asociado.        
        IntPtr MainWindowHandle { get; }
        // Resumen:
        //     Obtiene el tamaño de memoria física, expresado en bytes, que se asignó para el
        //     proceso asociado.
        //
        // Devuelve:
        //     Tamaño de memoria física, expresado en bytes, que se asignó para el proceso asociado.        
        long WorkingSet64 { get; }

        void SetActiveProcess(System.Diagnostics.Process process);
    }
}
