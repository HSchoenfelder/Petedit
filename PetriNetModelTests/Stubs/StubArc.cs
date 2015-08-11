using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModelTests
{
    internal class StubArc : IArc
    {
        public String Id { get; set; }

        public StubArc(String id)
        {
            Id = id;
        }

        public string GetSourceId()
        {
            return null;
        }

        public string GetTargetId()
        {
            return null;
        }

        public void Add(CallbackDelegates.TransitionStateChanged callback)
        { }

        public void Remove(CallbackDelegates.TransitionStateChanged callback)
        { }
    }
}
