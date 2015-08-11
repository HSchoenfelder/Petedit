using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    internal class Transition : Node, ITransition
    {
        #region fields
        /// <summary>Store for the Enabled property. </summary>
        private bool _enabled;
        #endregion

        #region properties
        /// <summary>Gets the current state of this Transition. </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the Transition class with the specified coordinates and id. 
        /// </summary>
        /// <param name="x">The x-coordinate of the new Transition.</param>
        /// <param name="y">The y-coordinate of the new Transition.</param>
        /// <param name="id">The id of the new Transition.</param>
        internal Transition(int x, int y, String id)
            : base(x, y, id)
        {
            _enabled = true;
        }
        #endregion

        #region methods
        /// <summary>
        /// Transitions tokens from connected Places through this Transition and sends notification
        /// about new token values.
        /// </summary>
        /// <param name="callbackTrans">
        /// The method to be called by a connected Place if the state of one of its connected
        /// transitions has changed. </param>
        /// <param name="callbackTokens">
        /// The method to be called if the token count of a connected Place has changed.</param>
        public void TransitionTokens(CallbackDelegates.TransitionStateChanged callbackTrans,
                                     CallbackDelegates.TokensChanged callbackTokens)
        {
            // reduce the token count of all predecessors by one
            foreach (INode predecessor in Predecessors)
            {
                IPlace toChange = (IPlace)predecessor;
                int newTokenCount = toChange.TokenCount - 1;
                if (newTokenCount < 0)
                    throw new InvalidOperationException("TokenCount reduced below zero!");
                toChange.ChangeTokenCount(newTokenCount, callbackTrans);
                callbackTokens(toChange.Id, newTokenCount);
            }
            // increase the token count of all successors by one
            foreach (INode successor in Successors)
            {
                IPlace toChange = (IPlace)successor;
                int newTokenCount = toChange.TokenCount + 1;
                toChange.ChangeTokenCount(newTokenCount, callbackTrans);
                callbackTokens(toChange.Id, newTokenCount);
            }
        }

        /// <summary>
        /// Inverses a transition of tokens through this Transition and sends notification about new 
        /// token values.
        /// </summary>
        /// <param name="callbackTrans">
        /// The method to be called by a connected Place if the state of one of its connected
        /// transitions has changed. </param>
        /// <param name="callbackTokens">
        /// The method to be called if the token count of a connected Place has changed. </param>
        public void InverseTransitionTokens(CallbackDelegates.TransitionStateChanged callbackTrans,
                                            CallbackDelegates.TokensChanged callbackTokens)
        {
            // reduce the token count of all successors by one
            foreach (INode successor in Successors)
            {
                IPlace toChange = (IPlace)successor;
                int newTokenCount = toChange.TokenCount - 1;
                if (newTokenCount < 0)
                    throw new InvalidOperationException("TokenCount reduced below zero!");
                toChange.ChangeTokenCount(newTokenCount, callbackTrans);
                callbackTokens(toChange.Id, newTokenCount);
            }
            // increase the token count of all predecessors by one
            foreach (INode predecessor in Predecessors)
            {
                IPlace toChange = (IPlace)predecessor;
                int newTokenCount = toChange.TokenCount + 1;
                toChange.ChangeTokenCount(newTokenCount, callbackTrans);
                callbackTokens(toChange.Id, newTokenCount);
            }
        }
        #endregion
    }
}
