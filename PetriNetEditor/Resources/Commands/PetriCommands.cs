using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetriNetEditor
{
    /// <summary>
    /// This class holds the routed commands used by the application.
    /// </summary>
    public static class PetriCommands
    {
        #region fields
        /// <summary> Store for the NewClicked property. </summary>
        private static RoutedUICommand _newClicked;

        /// <summary> Store for the OpenClicked property. </summary>
        private static RoutedUICommand _openClicked;

        /// <summary> Store for the SaveClicked property. </summary>
        private static RoutedUICommand _saveClicked;

        /// <summary> Store for the SaveAsClicked property. </summary>
        private static RoutedUICommand _saveAsClicked;

        /// <summary> Store for the ExitClicked property. </summary>
        private static RoutedUICommand _exitClicked;

        /// <summary> Store for the SetTokenCountClicked property. </summary>
        private static RoutedUICommand _setTokenCountClicked;
        #endregion

        #region properties
        /// <summary> Gets the NewClicked command. </summary>
        public static RoutedUICommand NewClicked
        {
            get { return _newClicked; }
        }

        /// <summary> Gets the OpenClicked command. </summary>
        public static RoutedUICommand OpenClicked
        {
            get { return _openClicked; }
        }

        /// <summary> Gets the SaveClicked command. </summary>
        public static RoutedUICommand SaveClicked
        {
            get { return _saveClicked; }
        }

        /// <summary> Gets the SaveAsClicked command. </summary>
        public static RoutedUICommand SaveAsClicked
        {
            get { return _saveAsClicked; }
        }

        /// <summary> Gets the ExitClicked command. </summary>
        public static RoutedUICommand ExitClicked
        {
            get { return _exitClicked; }
        }

        /// <summary> Gets the SetTokenCountClicked command. </summary>
        public static RoutedUICommand SetTokenCountClicked
        {
            get { return _setTokenCountClicked; }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Initializes the routed commands.
        /// </summary>
        static PetriCommands()
        {
            _newClicked = new RoutedUICommand();
            _openClicked = new RoutedUICommand();
            _saveClicked = new RoutedUICommand();
            _saveAsClicked = new RoutedUICommand();
            _exitClicked = new RoutedUICommand();
            _setTokenCountClicked = new RoutedUICommand();
        }
        #endregion
    }
}
