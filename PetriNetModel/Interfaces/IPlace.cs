using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    internal interface IPlace : INode
    {
        int TokenCount { get; }
        void ChangeTokenCount(int tokenCount, CallbackDelegates.TransitionStateChanged callback);
    }
}
