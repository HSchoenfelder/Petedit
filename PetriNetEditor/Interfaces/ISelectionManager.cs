using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides methods to manage the selections made in the editor.
    /// </summary>
    public interface ISelectionManager
    {
        /// <summary> Occurs when a reevaluation of command state is required. </summary>
        event EventHandler ReevaluateCommandState;

        /// <summary> Gets the current number of selected items. </summary>
        int SelectedItemsCount { get; }

        /// <summary>
        /// Gets a copy of the list of the ids of all selected items.
        /// </summary>
        /// <returns>A copy of the list of the ids of all selected items.</returns>
        IList<String> GetSelectedItems();

        /// <summary>
        /// Gets a copy of the list of the ids of all auto selected arcs.
        /// </summary>
        /// <returns>A copy of the list of the ids of all auto selected arcs.</returns>
        IList<String> GetAutoSelectedArcs();

        /// <summary>
        /// Checks if the current selection contains the element with the specified id.
        /// </summary>
        /// <param name="id">The id of the element to check for.</param>
        /// <returns>true if the element is present in the selection; otherwise false.</returns>
        bool SelectionContains(String id);

        /// <summary>
        /// Selects the element with the specified id.
        /// </summary>
        /// <param name="id">The id of the element to select.</param>
        void SelectElement(String id);

        /// <summary>
        /// Deselects the element with the specified id.
        /// </summary>
        /// <param name="id">The id of the element to deselect.</param>
        void RemoveElement(String id);

        /// <summary>
        /// Sets a visual arc to automatically selected and adds it to the corresponding list.
        /// </summary>
        /// <param name="id">The id of the visual arc to be added to the automatically 
        /// selected arcs.</param>
        void AddAutoSelectedArc(String id);

        /// <summary>
        /// Attempts to set a visual arc to unselected and remove it from the list of automatically
        /// selected arcs. The visual arc is only unselected and removed, if it is not still connected
        /// to any selected node. 
        /// </summary>
        /// <param name="arcId">The id of the visual arc to be removed from the automatically selected
        /// arcs.</param>
        /// <param name="connectedNodeId">The id of the remaining visual node the arc is connected to.</param>
        void TryRemoveAutoSelectedArc(String id, String connectedNodeId);

        /// <summary>
        /// Updates the auto selection state of the arc with the specified id.
        /// </summary>
        /// <param name="arcId">The id of the arc to update.</param>
        void UpdateAutoSelection(String arcId);

        /// <summary>
        /// Adds a range of elements to the current selection.
        /// </summary>
        /// <param name="range">The range of elements to be added to the current selection.</param>
        void AddRange(IList<String> range);

        /// <summary>
        /// Removes all elements from the current selection and adjusts their selected state.
        /// </summary>
        void ClearSelectedItems();

        /// <summary>
        /// Removes all arcs from the current autoselection and adjusts their autoselected state.
        /// </summary>
        void ClearAutoSelectedArcs();
    }
}
