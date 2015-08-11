using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    internal interface ITransition : INode
    {
        bool Enabled { get; set; }
        void TransitionTokens(CallbackDelegates.TransitionStateChanged callbackTrans,
                              CallbackDelegates.TokensChanged callbackTokens);
        void InverseTransitionTokens(CallbackDelegates.TransitionStateChanged callbackTrans,
                                     CallbackDelegates.TokensChanged callbackTokens);
    }
}
