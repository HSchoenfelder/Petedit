using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModelTests
{
    internal class ConcreteNode : Node
    {
        public ConcreteNode(int x, int y, String id)
            : base(x, y, id)
        {

        }
    }
}
