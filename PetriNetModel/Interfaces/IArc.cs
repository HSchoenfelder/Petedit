using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    internal interface IArc
    {
        String Id { get; }
        String GetSourceId();
        String GetTargetId();
        void Add(CallbackDelegates.TransitionStateChanged callback);
        void Remove(CallbackDelegates.TransitionStateChanged callback);
    }
}
