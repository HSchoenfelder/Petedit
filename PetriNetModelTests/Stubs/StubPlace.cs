using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModelTests
{
    internal class StubPlace : StubNode, IPlace
    {
        public int TokenCount { get; set; }

        public StubPlace(String id, IList<INode> predecessors, IList<INode> successors)
        {
            Id = id;
            Predecessors = predecessors;
            Successors = successors;
        }

        public void ChangeTokenCount(int tokenCount, CallbackDelegates.TransitionStateChanged callback)
        {
            TokenCount = tokenCount;
        }
    }
}
