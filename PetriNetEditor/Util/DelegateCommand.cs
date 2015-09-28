using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetriNetEditor
{
    /// <summary>
    /// This class represents an ICommand that is used for ViewModel delegation.
    /// </summary>
    /// <typeparam name="T">The type of the command parameter.</typeparam>
    public class DelegateCommand<T> : ICommand, IDelegateCommand
    {
        /// <summary> The Predicate to be checked in the CanExecute-method. </summary>
        private readonly Predicate<T> _canExecute;

        /// <summary> The Action to be performed in the Execute-method. </summary>
        private readonly Action<T> _execute;

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action and Predicate. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Executes the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>true if this command can be executed; otherwise false.</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute((parameter == null) ? default(T) : (T)Convert.ChangeType(parameter, typeof(T)));
        }

        /// <summary>
        /// Executes the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        public void Execute(object parameter)
        {
            _execute((parameter == null) ? default(T) : (T)Convert.ChangeType(parameter, typeof(T)));
        }

        /// <summary>
        /// Raises the CanExecuteChanged event for this command.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// This class represents an ICommand that is used for ViewModel delegation and for which two parameters
    /// with the prescribed types can be supplied via an array as the command parameter.
    /// </summary>
    /// <typeparam name="T">The type of the first item supplied by the command parameter.</typeparam>
    /// <typeparam name="U">The type of the second item supplied by the command parameter.</typeparam>
    public class DelegateCommand<T, U> : ICommand, IDelegateCommand
    {
        /// <summary> The Predicate to be checked in the CanExecute-method. </summary>
        private readonly Predicate<object> _canExecute;

        /// <summary> The Action to be performed in the Execute-method. </summary>
        private readonly Action<T, U> _execute;

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        public DelegateCommand(Action<T, U> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action and Predicate. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        public DelegateCommand(Action<T, U> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Executes the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>true if this command can be executed; otherwise false.</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        /// <summary>
        /// Executes the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        public void Execute(object parameter)
        {
            object[] paramArray = (object[])parameter;
            _execute((T)Convert.ChangeType(paramArray[0], typeof(T)), (U)Convert.ChangeType(paramArray[1], typeof(U))); 
        }

        /// <summary>
        /// Raises the CanExecuteChanged event for this command.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// This class represents an ICommand that is used for ViewModel delegation and for which three parameters
    /// with the prescribed types can be supplied via an array as the command parameter.
    /// </summary>
    /// <typeparam name="T">The type of the first item supplied by the command parameter.</typeparam>
    /// <typeparam name="U">The type of the second item supplied by the command parameter.</typeparam>
    /// <typeparam name="V">The type of the third item supplied by the command parameter.</typeparam>
    public class DelegateCommand<T, U, V> : ICommand, IDelegateCommand
    {
        /// <summary> The Predicate to be checked in the CanExecute-method. </summary>
        private readonly Predicate<object> _canExecute;

        /// <summary> The Action to be performed in the Execute-method. </summary>
        private readonly Action<T, U, V> _execute;

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        public DelegateCommand(Action<T, U, V> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action and Predicate. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        public DelegateCommand(Action<T, U, V> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Executes the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>true if this command can be executed; otherwise false.</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        /// <summary>
        /// Executes the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        public void Execute(object parameter)
        {
            object[] paramArray = (object[])parameter;
            _execute((T)Convert.ChangeType(paramArray[0], typeof(T)), (U)Convert.ChangeType(paramArray[1], typeof(U)),
                                           (V)Convert.ChangeType(paramArray[2], typeof(V)));
        }

        /// <summary>
        /// Raises the CanExecuteChanged event for this command.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
