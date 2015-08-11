using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides data related to the SizeFactorChanged event.
    /// </summary>
    public class SizeFactorChangedEventArgs : EventArgs
    {
        /// <summary> Gets the new size factor. </summary>
        public int SizeFactor { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the SizeFactorChangedEventArgs class with the specified size factor. 
        /// </summary>
        /// <param name="sizeFactor">The new size factor associated with the event.</param>
        public SizeFactorChangedEventArgs(int sizeFactor)
        {
            SizeFactor = sizeFactor;
        }
    }
}
