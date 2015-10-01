using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    public class DependencyFactory
    {
        /// <summary> Store for the CustomProvider property. </summary>
        private static IElementProvider _customProvider = null;

        /// <summary>
        /// Gets or sets a custom element provider to be created.
        /// </summary>
        public static IElementProvider CustomProvider
        {
            private get { return _customProvider; }
            set { _customProvider = value; }
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
    }
}
