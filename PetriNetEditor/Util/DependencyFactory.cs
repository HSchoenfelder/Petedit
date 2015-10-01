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
        /// <summary> Store for the CustomProvider property. </summary>
        private static IElementProvider _customProvider = null;

        /// <summary> Store for the CustomSelectionManager property. </summary>
        private static ISelectionManager _customSelectionManager = null;

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
    }
}
