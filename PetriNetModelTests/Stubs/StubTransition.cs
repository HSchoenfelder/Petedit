using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModelTests
{
    internal class StubTransition : StubNode, ITransition
    {
        public bool Enabled { get; set; }

        public StubTransition(String id, IList<INode> predecessors, IList<INode> successors)
        {
            Id = id;
            Predecessors = predecessors;
            Successors = successors;
        }

        public void TransitionTokens(CallbackDelegates.TransitionStateChanged callbackTrans, 
                                     CallbackDelegates.TokensChanged callbackTokens)
        { }

        public void InverseTransitionTokens(CallbackDelegates.TransitionStateChanged callbackTrans,
                                            CallbackDelegates.TokensChanged callbackTokens)
        { }
    }
}
