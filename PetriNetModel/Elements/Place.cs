using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    /// <summary>
    /// This class represents a place in the petrinet.
    /// </summary>
    internal class Place : Node, IPlace
    {
        #region fields
        /// <summary> Store for the TokenCount property. </summary>
        private int _tokenCount;
        #endregion

        #region properties
        /// <summary> Gets the current amount of tokens on this Place. </summary>
        public int TokenCount
        {
            get { return _tokenCount; }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the Place class with the specified coordinates and id. 
        /// </summary>
        /// <param name="x">The x-coordinate of the new Place.</param>
        /// <param name="y">The y-coordinate of the new Place.</param>
        /// <param name="id">The id of the new Place.</param>
        internal Place(int x, int y, String id)
            : base(x, y, id)
        {
            _tokenCount = 0;
        }
        #endregion

        #region methods
        /// <summary>
        /// Changes the amount of tokens to the amount provided and activates or deactivates transitions
        /// respectively. Sends notification if a transition has been activated or deactivated.
        /// </summary>
        /// <param name="tokenCount">The new amount of tokens.</param>
        /// <param name="callback">The method to be called in case a state change is caused.</param>
        public void ChangeTokenCount(int tokenCount, CallbackDelegates.TransitionStateChanged callback)
        {
            _tokenCount = tokenCount;

            bool checkActivated = tokenCount != 0;
            bool transitionEnabled = checkActivated;
            foreach (INode successor in Successors)
            {
                if (!(checkActivated ^ ((ITransition)successor).Enabled))  // if transition already activated,
                    continue;                                             // continue with next one
                foreach (INode predecessor in successor.Predecessors)
                {
                    if (((IPlace)predecessor).TokenCount == 0)
                        transitionEnabled = !checkActivated;
                }
                if (transitionEnabled)      // if transition needs to be activated
                {
                    ((ITransition)successor).Enabled = checkActivated;   // activate transition
                    callback(successor.Id, checkActivated);
                }
                transitionEnabled = checkActivated;   // reinitialize for next round
            }
        }
        

        /// <summary>
        /// Determines whether the specified object is a Place and whether it contains the same id and amount
        /// of tokens as this Place.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>
        /// true if obj is a Place and contains the same id and amount of tokens as this Pode; otherwise false
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is IPlace))
                return false;
            if (!((obj as IPlace).TokenCount == _tokenCount))
                return false;
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns the hash code for this Place.
        /// </summary>
        /// <returns>The hash code for this Place. </returns>
        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            hash = hash * 31 + _tokenCount;
            return hash;
        }
        #endregion methods
    }
}
