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
    public class DelegateCommand : ICommand, IDelegateCommand
    {
        #region fields
        /// <summary> Store for the CanExecutePred property. </summary>
        private Predicate<dynamic> _canExecutePred;

        /// <summary> Store for the ExecuteAction property. </summary>
        private Action<dynamic> _executeActionOne;

        /// <summary> Store for the ExecuteAction property. </summary>
        private Action<dynamic, dynamic> _executeActionTwo;

        /// <summary> Store for the ExecuteAction property. </summary>
        private Action<dynamic, dynamic, dynamic> _executeActionThree;
        #endregion

        #region events
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
        #endregion

        #region properties
        /// <summary> Sets the Predicate to be checked in the CanExecute-method. </summary>
        public Predicate<dynamic> CanExecutePred
        {
            private get { return _canExecutePred; }
            set { _canExecutePred = value; }
        }

        /// <summary> Sets the Action to be performed in the Execute-method. </summary>
        public Action<dynamic> ExecuteActionOne
        {
            private get { return _executeActionOne; }
            set { _executeActionOne = value; }
        }

        /// <summary> Sets the Action to be performed in the Execute-method. </summary>
        public Action<dynamic, dynamic> ExecuteActionTwo
        {
            private get { return _executeActionTwo; }
            set { _executeActionTwo = value; }
        }

        /// <summary> Sets the Action to be performed in the Execute-method. </summary>
        public Action<dynamic, dynamic, dynamic> ExecuteActionThree
        {
            private get { return _executeActionThree; }
            set { _executeActionThree = value; }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        public DelegateCommand(Action<dynamic> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action and Predicate. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        public DelegateCommand(Action<dynamic> execute, Predicate<dynamic> canExecute)
        {
            _executeActionOne = execute;
            _canExecutePred = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        public DelegateCommand(Action<dynamic, dynamic> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action and Predicate. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        public DelegateCommand(Action<dynamic, dynamic> execute, Predicate<dynamic> canExecute)
        {
            _executeActionTwo = execute;
            _canExecutePred = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        public DelegateCommand(Action<dynamic, dynamic, dynamic> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class with the specified Action and Predicate. 
        /// </summary>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        public DelegateCommand(Action<dynamic, dynamic, dynamic> execute, Predicate<dynamic> canExecute)
        {
            _executeActionThree = execute;
            _canExecutePred = canExecute;
        }
        #endregion

        #region methods
        /// <summary>
        /// Executes the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>true if this command can be executed; otherwise false.</returns>
        public bool CanExecute(object parameter)
        {
            if (CanExecutePred == null)
                return true;
            return CanExecutePred(parameter);
        }

        /// <summary>
        /// Executes the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">The parameters to the command.</param>
        public void Execute(object parameter)
        {
            if (ExecuteActionOne != null)
                ExecuteActionOne(parameter);
            else if(ExecuteActionTwo != null)
            {
                object[] paramArray = (object[])parameter;
                ExecuteActionTwo(paramArray[0], paramArray[1]);
            }
            else if (ExecuteActionThree != null)
            {
                object[] paramArray = (object[])parameter;
                ExecuteActionThree(paramArray[0], paramArray[1], paramArray[2]);
            }
        }

        /// <summary>
        /// Raises the CanExecuteChanged event for this command.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
        #endregion
    }
}
