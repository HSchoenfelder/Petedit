using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    /// <summary>
    /// This class provides data for the TransitionStateChangedEvent.
    /// </summary>
    public class TransitionStateChangedEventArgs
    {
        #region properties
        /// <summary>Gets the id of the Transition for which the event was raised.</summary>
        public String TransitionId { get; private set; }

        /// <summary>Gets the new state of the Transition for which the event was raised.</summary>
        public bool Active { get; private set; }
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the TransitionStateChangedEventArgs class.
        /// </summary>
        /// <param name="transId">The id of the Transition for which the event was raised.</param>
        /// <param name="active">The new state of the Transition.</param>
        public TransitionStateChangedEventArgs(String transId, bool active)
        {
            TransitionId = transId;
            Active = active;
        }
        #endregion
    }
}
