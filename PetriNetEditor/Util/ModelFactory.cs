using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// This factory class creates the model class.
    /// </summary>
    public class ModelFactory
    {
        /// <summary> Store for the Model property. </summary>
        private static IModel _customModel = null;

        /// <summary>
        /// Gets or sets a custom model to be created.
        /// </summary>
        public static IModel CustomModel
        {
            private get { return _customModel; }
            set { _customModel = value; }
        }

        /// <summary>
        /// Sets up a new Model or returns the custom model.
        /// </summary>
        /// <returns>A new Model or a custom model, if one was provided.</returns>
        public IModel CreateModel()
        {
            if (CustomModel != null)
                return CustomModel;
            return new ModelMain();
        }
    }
}
