using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// This factory class creates the dependencies needed by the viewmodel or injects custom 
    /// dependencies instead.
    /// </summary>
    public class DependencyFactory
    {
        #region fields
        /// <summary> Store for the CustomProvider property. </summary>
        private static IElementProvider _customProvider = null;

        /// <summary> Store for the CustomSelectionManager property. </summary>
        private static ISelectionManager _customSelectionManager = null;

        /// <summary> Store for the CustomUndoManager property. </summary>
        private static IUndoManager _customUndoManager = null;

        /// <summary> Store for the CustomElementManager property. </summary>
        private static IElementManager _customElementManager = null;
        #endregion

        #region properties
        /// <summary>
        /// Gets or sets a custom element provider to be created.
        /// </summary>
        public static IElementProvider CustomProvider
        {
            private get { return _customProvider; }
            set { _customProvider = value; }
        }

        /// <summary>
        /// Gets or sets a custom selection manager to be created.
        /// </summary>
        public static ISelectionManager CustomSelectionManager
        {
            private get { return _customSelectionManager; }
            set { _customSelectionManager = value; }
        }

        /// <summary>
        /// Gets or sets a custom undo manager to be created.
        /// </summary>
        public static IUndoManager CustomUndoManager
        {
            private get { return _customUndoManager; }
            set { _customUndoManager = value; }
        }

        /// <summary>
        /// Gets or sets a custom element manager to be created.
        /// </summary>
        public static IElementManager CustomElementManager
        {
            private get { return _customElementManager; }
            set { _customElementManager = value; }
        }
        #endregion

        #region methods
        /// <summary>
        /// Sets up a new ElementProvider or returns the custom provider.
        /// </summary>
        /// <returns>A new ElementProvider or a custom provider, if one was provided.</returns>
        public IElementProvider CreateProvider()
        {
            if (CustomProvider != null)
                return CustomProvider;
            return new ElementProvider();
        }

        /// <summary>
        /// Sets up a new SelectionManager or returns the custom selection manager.
        /// </summary>
        /// <returns>A new SelectionManager or a custom selection manager, if one was provided.</returns>
        public ISelectionManager CreateSelectionManager(IElementProvider elementProvider, IModel model)
        {
            if (CustomSelectionManager != null)
                return CustomSelectionManager;
            return new SelectionManager(elementProvider, model);
        }

        /// <summary>
        /// Sets up a new UndoManager or returns the custom undo manager.
        /// </summary>
        /// <returns>A new UndoManager or a custom undo manager, if one was provided.</returns>
        public IUndoManager CreateUndoManager()
        {
            if (CustomUndoManager != null)
                return CustomUndoManager;
            return new UndoManager();
        }

        /// <summary>
        /// Sets up a new ElementManager or returns the custom element manager.
        /// </summary>
        /// <returns>A new ElementManager or a custom element manager, if one was provided.</returns>
        public IElementManager CreateElementManager(IElementProvider elementProvider, ISelectionManager selectionManager, 
                                                    IUndoManagerEx undoManager, IModel model, int drawSize, int arrowheadSize)
        {
            if (CustomElementManager != null)
                return CustomElementManager;
            return new ElementManager(elementProvider, selectionManager, undoManager, model, drawSize, arrowheadSize);
        }
        #endregion
    }
}
