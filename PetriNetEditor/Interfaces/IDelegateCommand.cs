using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Represents a command that is used for ViewModel delegation.
    /// </summary>
    public interface IDelegateCommand
    {
        #region properties
        /// <summary> Sets the Predicate to be checked in the CanExecute-method. </summary>
        Predicate<dynamic> CanExecutePred { set; }

        /// <summary> Sets the Action to be performed in the Execute-method. </summary>
        Action<dynamic> ExecuteActionOne { set; }

        /// <summary> Sets the Action to be performed in the Execute-method. </summary>
        Action<dynamic, dynamic> ExecuteActionTwo { set; }

        /// <summary> Sets the Action to be performed in the Execute-method. </summary>
        Action<dynamic, dynamic, dynamic> ExecuteActionThree { set; }
        #endregion

        #region methods
        /// <summary>
        /// Executes the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>true if this command can be executed; otherwise false.</returns>
        bool CanExecute(object parameter);
        
        /// <summary>
        /// Executes the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        void Execute(object parameter);

        /// <summary>
        /// Raises the CanExecuteChanged event for this command.
        /// </summary>
        void RaiseCanExecuteChanged();
        #endregion
    }
}
