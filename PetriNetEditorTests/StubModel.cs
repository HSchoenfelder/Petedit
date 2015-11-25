using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetriNetModel;
using System.Windows;

namespace PetriNetEditorTests
{
    public class StubModel : IModel
    {
        private IList<String> Nodes { get; set; }

        private IList<String> Arcs { get; set; }

        public StubModel()
        {
            Nodes = new List<String>();
            Arcs = new List<String>();
        }

        public void AddPlace(int xCoordinate, int yCoordinate, string id)
        {
            Nodes.Add(id);
        }

        public void AddTransition(int xCoordinate, int yCoordinate, string id)
        {
            Nodes.Add(id);
        }

        public void AddArc(string sourceId, string targetId, string id)
        {
            Arcs.Add(id);
        }

        public bool Contains(string id)
        {
            if (Nodes.Contains(id))
                return true;
            else if (Arcs.Contains(id))
                return true;
            else
                return false;
        }

        public void RemoveNode(string id)
        {
            Nodes.Remove(id);
        }

        public void RemoveArc(string id)
        {
            Arcs.Remove(id);
        }

        public string[] GetNodesForArc(string arcId)
        {
            String[] dummyNodes = new String[2] { "dummySource", "dummyTarget" };
            return dummyNodes;
        }

        public bool IsSource(string arcId, string nodeId)
        {
            return false;
        }

        public bool IsNode(string id)
        {
            if (Nodes.Contains(id))
                return true;
            return false;
        }

        public bool IsPlace(string nodeId)
        {
            if (Nodes[0].ToLower().Contains("testplace"))
                return true;
            else
                return false;
        }

        public string GetName(string nodeId)
        {
            return "testDummy";
        }

        public int GetTokenCount(string placeId)
        {
            return 0;
        }

        public System.Windows.Point GetCoordinates(string nodeId)
        {
            return new Point(20, 20);
        }

        public bool GetState(string transId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetArcsForNode(string nodeId)
        {
            throw new NotImplementedException();
        }

        public string GetConnectedNode(string nodeId, string arcId)
        {
            throw new NotImplementedException();
        }

        public string GetArc(string sourceId, string targetId)
        {
            throw new NotImplementedException();
        }
                
        public void ChangeName(string nodeId, string newName)
        {
            throw new NotImplementedException();
        }

        public void ChangePosition(string nodeId, int xCoordinate, int yCoordinate)
        {
            throw new NotImplementedException();
        }

        public void ChangeTokens(string placeId, int newTokenCount)
        {
            throw new NotImplementedException();
        }

        public void PerformTransition(string transitionId)
        {
            throw new NotImplementedException();
        }

        public void InverseTransition(string transitionId)
        {
            throw new NotImplementedException();
        }

        public void Reinitialize()
        {
            throw new NotImplementedException();
        }

        #pragma warning disable 0067
        public event TransitionStateChangedEventHandler TransitionStateChangedEvent;
        public event TokensChangedEventHandler TokensChangedEvent;
        #pragma warning restore 0067
    }
}
