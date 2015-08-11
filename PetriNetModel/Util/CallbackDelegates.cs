using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    /// <summary>
    /// This class provides delegates for notification about changes to transition state
    /// and token count of nodes.
    /// </summary>
    internal class CallbackDelegates
    {
        /// <summary>
        /// Called when the state of a Transition changes.
        /// </summary>
        /// <param name="transitionId">The id of the Transition.</param>
        /// <param name="active">The new state of the Transition.</param>
        internal delegate void TransitionStateChanged(String transitionId, bool active);

        /// <summary>
        /// Called when the amount of tokens on a Place changes.
        /// </summary>
        /// <param name="placeId">The id of the Place.</param>
        /// <param name="tokenCount">The new amount of tokens.</param>
        internal delegate void TokensChanged(String placeId, int tokenCount);
    }
}
