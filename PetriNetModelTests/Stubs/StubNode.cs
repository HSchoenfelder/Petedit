using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModelTests
{
    internal class StubNode : INode
    {
        public String Id { get; set; }
        public IList<INode> Predecessors { get; set; }
        public IList<INode> Successors { get; set; }
        public IList<IArc> Arcs { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public String Name { get; set; }

        public void RemoveSuccessor(INode deletedNode)
        {
            Successors.Remove(deletedNode);
        }

        public void RemovePredecessor(INode deletedNode)
        {
            Predecessors.Remove(deletedNode);
        }

        public void RemoveArc(IArc deletedArc)
        {
            Arcs.Remove(deletedArc);
        }

        public void AddSuccessor(INode successor) { }
        public void AddPredecessor(INode predecessor) { }
        public void AddArc(IArc arc) { }
        public List<String> GetArcList() { return null; }
    }
}
