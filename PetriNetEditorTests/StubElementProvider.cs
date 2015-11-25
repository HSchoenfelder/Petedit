using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetriNetEditor;

namespace PetriNetEditorTests
{
    public class StubElementProvider : IElementProvider
    {
        private IList<String> Nodes { get; set; }

        private IList<String> Arcs { get; set; }

        public int NodesCount
        {
            get { return Nodes.Count; }
        }

        public int ArcsCount
        {
            get { return Arcs.Count; }
        }

        public StubElementProvider()
        {
            Nodes = new List<String>();
            Arcs = new List<String>();
        }

        public void AddNodeId(String nodeId)
        {
            Nodes.Add(nodeId);
        }

        public void AddArcId(String arcId)
        {
            Arcs.Add(arcId);
        }

        public string GetNodeId(int i)
        {
            return Nodes[i];
        }

        public string GetArcId(int i)
        {
            return Arcs[i];
        }

        public int NameFieldsCount
        {
            get { throw new NotImplementedException(); }
        }

        public void ChangeArrowheadSize(int newArrowheadSize)
        {
            throw new NotImplementedException();
        }

        public void ChangeDrawSize(int newDrawSize)
        {
            throw new NotImplementedException();
        }

        public INameField GetNameField(string id)
        {
            throw new NotImplementedException();
        }

        public INameField GetNameField(int i)
        {
            throw new NotImplementedException();
        }

        public void AddNameField(INameField nf)
        {
            throw new NotImplementedException();
        }

        public void RemoveNameField(string id)
        {
            throw new NotImplementedException();
        }

        public IVisualNode GetNode(string id)
        {
            throw new NotImplementedException();
        }

        public IVisualNode GetNode(int i)
        {
            throw new NotImplementedException();
        }

        public void AddNode(IVisualNode node)
        {
            throw new NotImplementedException();
        }

        public void RemoveNode(string id)
        {
            throw new NotImplementedException();
        }

        public IVisualArc GetArc(string id)
        {
            throw new NotImplementedException();
        }

        public IVisualArc GetArc(int i)
        {
            throw new NotImplementedException();
        }

        public void AddArc(IVisualArc arc)
        {
            throw new NotImplementedException();
        }

        public void RemoveArc(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveArc(IVisualArc arc)
        {
            throw new NotImplementedException();
        }

        public void ClearNameFields()
        {
            throw new NotImplementedException();
        }

        public void ClearNodes()
        {
            throw new NotImplementedException();
        }

        public void ClearArcs()
        {
            throw new NotImplementedException();
        }
    }
}
