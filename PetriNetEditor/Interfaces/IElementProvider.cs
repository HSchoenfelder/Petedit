using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides access to the individual visual elements of the petrinet.
    /// </summary>
    public interface IElementProvider
    {
        /// <summary> 
        /// Gets the amount of name fields currently present in the provider.
        /// </summary>
        int NameFieldsCount { get; }

        /// <summary> 
        /// Gets the amount of nodes currently present in the provider.
        /// </summary>
        int NodesCount { get; }

        /// <summary> 
        /// Gets the amount of arcs currently present in the provider.
        /// </summary>
        int ArcsCount { get; }

        /// <summary>
        /// Returns a reference to the NameField with the given id.
        /// </summary>
        /// <param name="id">The id of the NameField to return.</param>
        /// <returns>The NameField with the specified id.</returns>
        INameField GetNameField(String id);

        /// <summary>
        /// Returns a reference to the NameField with the given index in the
        /// underlying collection. This method can be used to iterate over
        /// all name fields.
        /// </summary>
        /// <param name="i">The index of the NameField to return.</param>
        /// <returns>The NameField with the specified index.</returns>
        INameField GetNameField(int i);

        /// <summary>
        /// Adds the specified NameField to the visual presentation of the petrinet.
        /// </summary>
        /// <param name="nf">A reference to the NameField that is to be added.</param>
        void AddNameField(INameField nf);

        /// <summary>
        /// Removes the NameField with the specified id from the visual presentation
        /// of the petrinet.
        /// </summary>
        /// <param name="id">The id of the NameField to remove.</param>
        void RemoveNameField(String id);

        /// <summary>
        /// Returns a reference to the VisualNode with the given id.
        /// </summary>
        /// <param name="id">The id of the VisualNode to return.</param>
        /// <returns>The VisualNode with the specified id.</returns>
        IVisualNode GetNode(String id);

        /// <summary>
        /// Returns a reference to the VisualNode with the given index in the
        /// underlying collection. This method can be used to iterate over
        /// all visual nodes.
        /// </summary>
        /// <param name="i">The index of the VisualNode to return.</param>
        /// <returns>The VisualNode with the specified index.</returns>
        IVisualNode GetNode(int i);

        /// <summary>
        /// Gets the id of the VisualNode with the given index in the underlying 
        /// collection. This method can be used to iterate over the ids of 
        /// all visual nodes.
        /// </summary>
        /// <param name="i">The index of the VisualNode to return the id of.</param>
        /// <returns>The id of the VisualNode with the specified index.</returns>
        String GetNodeId(int i);

        /// <summary>
        /// Adds the specified VisualNode to the visual presentation of the petrinet.
        /// </summary>
        /// <param name="node">A reference to the VisualNode that is to be added.</param>
        void AddNode(IVisualNode node);

        /// <summary>
        /// Removes the VisualNode with the specified id from the visual presentation
        /// of the petrinet.
        /// </summary>
        /// <param name="id">The id of the VisualNode to remove.</param>
        void RemoveNode(String id);

        /// <summary>
        /// Returns a reference to the VisualArc with the given id.
        /// </summary>
        /// <param name="id">The id of the VisualArc to return.</param>
        /// <returns>The VisualArc with the specified id.</returns>
        IVisualArc GetArc(String id);

        /// <summary>
        /// Returns a reference to the VisualArc with the given index in the
        /// underlying collection. This method can be used to iterate over
        /// all visual arcs.
        /// </summary>
        /// <param name="i">The index of the VisualArc to return.</param>
        /// <returns>The VisualArc with the specified index.</returns>
        IVisualArc GetArc(int i);

        /// <summary>
        /// Gets the id of the VisualArc with the given index in the underlying 
        /// collection. This method can be used to iterate over the ids of 
        /// all visual arcs.
        /// </summary>
        /// <param name="i">The index of the VisualArc to return the id of.</param>
        /// <returns>The id of the VisualArc with the specified index.</returns>
        String GetArcId(int i);

        /// <summary>
        /// Adds the specified VisualArc to the visual presentation of the petrinet.
        /// </summary>
        /// <param name="arc">A reference to the VisualArc that is to be added.</param>
        void AddArc(IVisualArc arc);

        /// <summary>
        /// Removes the VisualArc with the specified id from the visual presentation
        /// of the petrinet.
        /// </summary>
        /// <param name="id">The id of the VisualArc to remove.</param>
        void RemoveArc(String id);

        /// <summary>
        /// Removes the specified VisualArc from the visual presentation of the petrinet.
        /// </summary>
        /// <param name="arc">The VisualArc to remove.</param>
        void RemoveArc(IVisualArc arc);

        /// <summary>
        /// Clears all NameFields from the presentation of the petrinet.
        /// </summary>
        void ClearNameFields();

        /// <summary>
        /// Clears all VisualNodes from the presentation of the petrinet.
        /// </summary>
        void ClearNodes();

        /// <summary>
        /// Clears all VisualArcs from the presentation of the petrinet.
        /// </summary>
        void ClearArcs();
    }
}
