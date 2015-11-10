using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetriNetEditor;

namespace PetriNetEditorTests
{
    public class StubSelectionManager : ISelectionManager
    {
        private IList<String> SelectedItems { get; set; }

        private IList<String> AutoSelectedArcs { get; set; }

        public void SelectElement(string id)
        {
            SelectedItems.Add(id);
        }

        public void AddAutoSelectedArc(string id)
        {
            AutoSelectedArcs.Add(id);
        }

        public StubSelectionManager()
        {
            SelectedItems = new List<String>();
            AutoSelectedArcs = new List<String>();
        }

        public void ClearSelectedItems()
        {
            SelectedItems.Clear();
        }

        public void ClearAutoSelectedArcs()
        {
            AutoSelectedArcs.Clear();
        }

        public IList<string> GetSelectedItems()
        {
            return new List<String>(SelectedItems);
        }

        public IList<string> GetAutoSelectedArcs()
        {
            return new List<String>(AutoSelectedArcs);
        }

        public int SelectedItemsCount
        {
            get { return SelectedItems.Count; }
        }

        public bool SelectionContains(string id)
        {
            return true;
        }

        public void RemoveElement(string id)
        {
            throw new NotImplementedException();
        }

        public void TryRemoveAutoSelectedArc(string id, string connectedNodeId)
        {
            throw new NotImplementedException();
        }

        public void UpdateAutoSelection(string arcId)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IList<string> range)
        {
            throw new NotImplementedException();
        }

        #pragma warning disable 0067
        public event EventHandler ReevaluateCommandState;
        #pragma warning restore 0067
    }
}
