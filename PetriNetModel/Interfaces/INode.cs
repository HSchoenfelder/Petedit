using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    internal interface INode
    {
        String Id { get; }
        int X { get; set; }
        int Y { get; set; }
        String Name { get; set; }
        IList<INode> Predecessors { get; }
        List<String> GetArcList();
        void AddArc(IArc addedArc);
        void AddPredecessor(INode predecessor);
        void AddSuccessor(INode successor);
        void RemoveSuccessor(INode deletedNode);
        void RemovePredecessor(INode deletedNode);
        void RemoveArc(IArc deletedArc);
    }
}
