using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// This factory class creates delegate commands or connects a custom command with
    /// the provided methods.
    /// </summary>
    public class CommandFactory
    {
        /// <summary> Store for the CustomCommand property. </summary>
        private static ICustomCommand _customCommand = null;

        /// <summary>
        /// Gets or sets a custom command to be created.
        /// </summary>
        public static ICustomCommand CustomCommand
        {
            private get { return _customCommand; }
            set { _customCommand = value; }
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided method in one parameter or 
        /// returns the custom command.
        /// </summary>
        /// <param name="commandType">The type of the command to be created.</param>
        /// <param name="execute">The Action to be executed.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T>(CommandTypes commandType, Action<T> execute)
        {
            if (CustomCommand != null)
            {
                if(CustomCommand.CommandType.Equals(commandType))
                    CustomCommand.ExecuteActionOne = Convert<T>(execute);
                return CustomCommand;
            }
            return new DelegateCommand(Convert<T>(execute));
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided methods in one parameter or 
        /// returns the custom command.
        /// </summary>
        /// <param name="commandType">The type of the command to be created.</param>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T>(CommandTypes commandType, Action<T> execute, Predicate<T> canExecute)
        {
            if (CustomCommand != null)
            {
                if (CustomCommand.CommandType.Equals(commandType))
                {
                    CustomCommand.CanExecutePred = Convert<T>(canExecute);
                    CustomCommand.ExecuteActionOne = Convert<T>(execute); 
                }
                return CustomCommand;
            }
            return new DelegateCommand(Convert<T>(execute), Convert<T>(canExecute));
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided method in two parameters or 
        /// returns the custom command.
        /// </summary>
        /// <param name="commandType">The type of the command to be created.</param>
        /// <param name="execute">The Action to be executed.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T, U>(CommandTypes commandType, Action<T, U> execute)
        {
            if (CustomCommand != null)
            {
                if (CustomCommand.CommandType.Equals(commandType))
                    CustomCommand.ExecuteActionTwo = Convert<T, U>(execute);
                return CustomCommand;
            }
            return new DelegateCommand(Convert<T, U>(execute));
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided methods in two parameters or 
        /// returns the custom command.
        /// </summary>
        /// <param name="commandType">The type of the command to be created.</param>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T, U>(CommandTypes commandType, Action<T, U> execute, Predicate<T> canExecute)
        {
            if (CustomCommand != null)
            {
                if (CustomCommand.CommandType.Equals(commandType))
                {
                    CustomCommand.CanExecutePred = Convert<T>(canExecute);
                    CustomCommand.ExecuteActionTwo = Convert<T, U>(execute); 
                }
                return CustomCommand;
            }
            return new DelegateCommand(Convert<T, U>(execute), Convert<T>(canExecute));
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided method in three parameters or 
        /// returns the custom command.
        /// </summary>
        /// <param name="commandType">The type of the command to be created.</param>
        /// <param name="execute">The Action to be executed.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T, U, V>(CommandTypes commandType, Action<T, U, V> execute)
        {
            if (CustomCommand != null)
            {
                if (CustomCommand.CommandType.Equals(commandType))
                    CustomCommand.ExecuteActionThree = Convert<T, U, V>(execute);
                return CustomCommand;
            }
            return new DelegateCommand(Convert<T, U, V>(execute));
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided methods in three parameters or 
        /// returns the custom command.
        /// </summary>
        /// <param name="commandType">The type of the command to be created.</param>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T, U, V>(CommandTypes commandType, Action<T, U, V> execute, Predicate<T> canExecute)
        {
            if (CustomCommand != null)
            {
                if (CustomCommand.CommandType.Equals(commandType))
                {
                    CustomCommand.CanExecutePred = Convert<T>(canExecute);
                    CustomCommand.ExecuteActionThree = Convert<T, U, V>(execute); 
                }
                return CustomCommand;
            }
            return new DelegateCommand(Convert<T, U, V>(execute), Convert<T>(canExecute));
        }

        private Predicate<dynamic> Convert<T>(Predicate<T> predicateT)
        {
            if (predicateT == null)
                return null;
            else return new Predicate<dynamic>(o => predicateT((T)o));
        }

        private Action<dynamic> Convert<T>(Action<T> actionT)
        {
            if (actionT == null)
                return null;
            else return new Action<dynamic>(o => actionT((T)o));
        }

        private Action<dynamic, dynamic> Convert<T, U>(Action<T, U> actionTU)
        {
            if (actionTU == null)
                return null;
            else return new Action<dynamic, dynamic>((o, p) => actionTU((T)o, (U)p));
        }

        private Action<dynamic, dynamic, dynamic> Convert<T, U, V>(Action<T, U, V> actionTUV)
        {
            if (actionTUV == null)
                return null;
            else return new Action<dynamic, dynamic, dynamic>((o, p, q) => actionTUV((T)o, (U)p, (V)q));
        }
    }
}
