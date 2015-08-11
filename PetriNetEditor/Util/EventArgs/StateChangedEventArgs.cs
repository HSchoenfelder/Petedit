using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides data related to the StateChanged event.
    /// </summary>
    public class StateChangedEventArgs
    {
        /// <summary> Gets the new state. </summary>
        public bool State { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the StateChangedEventArgs class with the specified state.
        /// </summary>
        /// <param name="state">The new state associated with the event.</param>
        public StateChangedEventArgs(bool state)
        {
            State = state;
        }
    }
}
