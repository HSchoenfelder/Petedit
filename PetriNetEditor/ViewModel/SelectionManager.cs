using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// This class manages the selections made in the editor.
    /// </summary>
    public class SelectionManager : ISelectionManager
    {
        #region fields
        /// <summary> Store for the Model property. </summary>
        private IModel _model;

        /// <summary> Store for the ElementProvider property. </summary>
        private IElementProvider _elementProvider;

        /// <summary> Store for the SelectedItems property. </summary>
        private readonly List<String> _selectedItems;

        /// <summary> Store for the AutoSelectedArcs property. </summary>
        private readonly IList<String> _autoSelectedArcs;
        #endregion

        #region events
        /// <summary> Occurs when a reevaluation of command state is required. </summary>
        public event EventHandler ReevaluateCommandState;
        #endregion

        #region properties
        /// <summary> Gets the Model that allows for manipulation of the petrinet. </summary>
        private IModel Model
        {
            get { return _model; }
        }

        /// <summary>
        /// Gets the element provider that enables access to individual elements of the petrinet.
        /// operation.
        /// </summary>
        private IElementProvider ElementProvider
        {
            get { return _elementProvider; }
        }

        /// <summary> Gets the list that manages the selected items via their ids. </summary>
        private List<String> SelectedItems
        {
            get { return _selectedItems; }
        }

        /// <summary>
        /// Gets the list that manages the automatically selected arcs via their ids.
        /// </summary>
        private IList<String> AutoSelectedArcs
        {
            get { return _autoSelectedArcs; }
        }

        /// <summary> Gets the current number of selected items. </summary>
        public int SelectedItemsCount
        {
            get { return SelectedItems.Count; }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new SelectionManager with the specified references.
        /// </summary>
        /// <param name="elementProvider">Reference to the element provider.</param>
        /// <param name="model">Reference to the model of the petrinet.</param>
        public SelectionManager(IElementProvider elementProvider, IModel model)
        {
            _model = model;
            _elementProvider = elementProvider;
            _selectedItems = new List<String>();
            _autoSelectedArcs = new List<String>();
        }
        #endregion

        #region methods
        /// <summary>
        /// Gets a copy of the list of the ids of all selected items.
        /// </summary>
        /// <returns>A copy of the list of the ids of all selected items.</returns>
        public IList<String> GetSelectedItems()
        {
            return new List<String>(SelectedItems);
        }

        /// <summary>
        /// Gets a copy of the list of the ids of all auto selected arcs.
        /// </summary>
        /// <returns>A copy of the list of the ids of all auto selected arcs.</returns>
        public IList<String> GetAutoSelectedArcs()
        {
            return new List<String>(AutoSelectedArcs);
        }

        /// <summary>
        /// Checks if the current selection contains the element with the specified id.
        /// </summary>
        /// <param name="id">The id of the element to check for.</param>
        /// <returns>true if the element is present in the selection; otherwise false.</returns>
        public bool SelectionContains(String id)
        {
            return SelectedItems.Contains(id);
        }

        /// <summary>
        /// Selects the element with the specified id.
        /// </summary>
        /// <param name="id">The id of the element to select.</param>
        public void SelectElement(String id)
        {
            if (Model.IsNode(id))
                ElementProvider.GetNode(id).Selected = true;
            else
                ElementProvider.GetArc(id).Selected = true;
            SelectedItems.Add(id);
        }

        /// <summary>
        /// Deselects the element with the specified id.
        /// </summary>
        /// <param name="id">The id of the element to deselect.</param>
        public void RemoveElement(String id)
        {
            if (Model.IsNode(id))
                ElementProvider.GetNode(id).Selected = false;
            else
                ElementProvider.GetArc(id).Selected = false;
            SelectedItems.Remove(id);
        }

        /// <summary>
        /// Sets a visual arc to automatically selected and adds it to the corresponding list.
        /// </summary>
        /// <param name="id">The id of the visual arc to be added to the automatically 
        /// selected arcs.</param>
        public void AddAutoSelectedArc(String id)
        {
            if (!AutoSelectedArcs.Contains(id))
            {
                IVisualArc arc = ElementProvider.GetArc(id);
                arc.AutoSelected = true;
                AutoSelectedArcs.Add(id);
            }
        }

        /// <summary>
        /// Attempts to set a visual arc to unselected and remove it from the list of automatically
        /// selected arcs. The visual arc is only unselected and removed, if it is not still connected
        /// to any selected node. 
        /// </summary>
        /// <param name="arcId">The id of the visual arc to be removed from the automatically selected
        /// arcs.</param>
        /// <param name="connectedNodeId">The id of the remaining visual node the arc is connected to.</param>
        public void TryRemoveAutoSelectedArc(String id, String connectedNodeId)
        {
            if (!ElementProvider.GetNode(connectedNodeId).Selected)
            {
                IVisualArc arc = ElementProvider.GetArc(id);
                arc.AutoSelected = false;
                AutoSelectedArcs.Remove(id);
            }
        }

        /// <summary>
        /// Updates the auto selection state of the arc with the specified id.
        /// </summary>
        /// <param name="arcId">The id of the arc to update.</param>
        public void UpdateAutoSelection(String arcId)
        {
            String[] nodes = Model.GetNodesForArc(arcId);
            String sourceId = Model.IsSource(arcId, nodes[0]) ? nodes[0] : nodes[1];
            String targetId = Model.IsSource(arcId, nodes[0]) ? nodes[1] : nodes[0];
            if (ElementProvider.GetNode(sourceId).Selected || ElementProvider.GetNode(targetId).Selected)
            {
                ElementProvider.GetArc(arcId).AutoSelected = true;
                AutoSelectedArcs.Add(arcId);
            }
        }

        /// <summary>
        /// Adds a range of elements to the current selection.
        /// </summary>
        /// <param name="range">The range of elements to be added to the current selection.</param>
        public void AddRange(IList<String> range)
        {
            SelectedItems.AddRange(range);
        }

        /// <summary>
        /// Removes all elements from the current selection and adjusts their selected state.
        /// </summary>
        public void ClearSelectedItems()
        {
            foreach (String id in SelectedItems)
            {
                if (Model.IsNode(id))
                    ElementProvider.GetNode(id).Selected = false;
                else
                    ElementProvider.GetArc(id).Selected = false;
            }
            SelectedItems.Clear();
            ReevaluateCommandState(this, new EventArgs());
        }

        /// <summary>
        /// Removes all arcs from the current autoselection and adjusts their autoselected state.
        /// </summary>
        public void ClearAutoSelectedArcs()
        {
            foreach (String id in AutoSelectedArcs)
                ElementProvider.GetArc(id).AutoSelected = false;
            AutoSelectedArcs.Clear();
        }
        #endregion
    }
}
