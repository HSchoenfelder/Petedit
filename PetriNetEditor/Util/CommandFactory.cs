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
        private static IDelegateCommand _customCommand = null;

        /// <summary>
        /// Gets or sets a custom command to be created.
        /// </summary>
        public static IDelegateCommand CustomCommand
        {
            private get { return _customCommand; }
            set { _customCommand = value; }
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided method in one parameter or 
        /// returns the custom command.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="execute">The Action to be executed.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T>(Action<T> execute)
        {
            if (CustomCommand != null)
                return CustomCommand;
            return new DelegateCommand<T>(execute);
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided methods in one parameter or 
        /// returns the custom command.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T>(Action<T> execute, Predicate<T> canExecute)
        {
            if (CustomCommand != null)
                return CustomCommand;
            return new DelegateCommand<T>(execute, canExecute);
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided method in two parameters or 
        /// returns the custom command.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter.</typeparam>
        /// <typeparam name="U">The type of the second parameter.</typeparam>
        /// <param name="execute">The Action to be executed.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T, U>(Action<T, U> execute)
        {
            if (CustomCommand != null)
                return CustomCommand;
            return new DelegateCommand<T, U>(execute);
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided methods in two parameters or 
        /// returns the custom command.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter.</typeparam>
        /// <typeparam name="U">The type of the second parameter.</typeparam>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T, U>(Action<T, U> execute, Predicate<object> canExecute)
        {
            if (CustomCommand != null)
                return CustomCommand;
            return new DelegateCommand<T, U>(execute, canExecute);
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided method in three parameters or 
        /// returns the custom command.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter.</typeparam>
        /// <typeparam name="U">The type of the second parameter.</typeparam>
        /// <typeparam name="V">The type of the third parameter.</typeparam>
        /// <param name="execute">The Action to be executed.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T, U, V>(Action<T, U, V> execute)
        {
            if (CustomCommand != null)
                return CustomCommand;
            return new DelegateCommand<T, U, V>(execute);
        }

        /// <summary>
        /// Sets up a new DelegateCommand with the provided methods in three parameters or 
        /// returns the custom command.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter.</typeparam>
        /// <typeparam name="U">The type of the second parameter.</typeparam>
        /// <typeparam name="V">The type of the third parameter.</typeparam>
        /// <param name="execute">The Action to be executed.</param>
        /// <param name="canExecute">The Predicate to be checked.</param>
        /// <returns>A new DelegateCommand or a custom command, if one was provided.</returns>
        public IDelegateCommand Create<T, U, V>(Action<T, U, V> execute, Predicate<object> canExecute)
        {
            if (CustomCommand != null)
                return CustomCommand;
            return new DelegateCommand<T, U, V>(execute, canExecute);
        }
    }
}
