using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// This class provides access to the individual visual elements of the petrinet.
    /// </summary>
    public class ElementProvider : IElementProvider
    {
        #region fields
        /// <summary> Store for the NameFields property. </summary>
        private readonly IDictionary<String, INameField> _nameFields;

        /// <summary> Store for the VisualNodes property. </summary>
        private readonly IDictionary<String, IVisualNode> _visualNodes;

        /// <summary> Store for the VisualArcs property. </summary>
        private readonly IDictionary<String, IVisualArc> _visualArcs;
        #endregion

        #region properties
        /// <summary> 
        /// Gets the dictionary that manages the name fields of the visual nodes via their ids.
        /// </summary>
        public IDictionary<String, INameField> NameFields
        {
            get { return _nameFields; }
        }

        /// <summary> 
        /// Gets the dictionary that manages the individual visual nodes via their ids.
        /// </summary>
        public IDictionary<String, IVisualNode> VisualNodes
        {
            get { return _visualNodes; }
        }

        /// <summary> 
        /// Gets the dictionary that manages the individual visual arcs via their ids.
        /// </summary>
        public IDictionary<String, IVisualArc> VisualArcs
        {
            get { return _visualArcs; }
        }

        /// <summary> 
        /// Gets the amount of name fields currently present in the provider.
        /// </summary>
        public int NameFieldsCount
        {
            get { return NameFields.Keys.Count; }
        }

        /// <summary> 
        /// Gets the amount of nodes currently present in the provider.
        /// </summary>
        public int NodesCount
        {
            get { return VisualNodes.Keys.Count; }
        }

        /// <summary> 
        /// Gets the amount of arcs currently present in the provider.
        /// </summary>
        public int ArcsCount
        {
            get { return VisualArcs.Keys.Count; }
        }
        #endregion

        #region constructors
        /// <summary> Initializes a new ElementProvider. </summary>
        public ElementProvider()
        {
            _nameFields = new ObservableDictionary<String, INameField>();
            _visualNodes = new ObservableDictionary<String, IVisualNode>();
            _visualArcs = new ObservableDictionary<String, IVisualArc>();
        }
        #endregion

        #region methods
        /// <summary>
        /// Changes the arrowhead size of all arcs.
        /// </summary>
        /// <param name="newArrowheadSize">The new arrowhead size.</param>
        public void ChangeArrowheadSize(int newArrowheadSize)
        {
            foreach (IVisualArc arc in VisualArcs.Values)
            {
                arc.ArrowheadSize = newArrowheadSize;
            }
        }

        /// <summary>
        /// Changes the drawsize size of all nodes, arcs and namefields.
        /// </summary>
        /// <param name="newDrawSize">The new drawsize.</param>
        public void ChangeDrawSize(int newDrawSize)
        {
            foreach (IVisualArc arc in VisualArcs.Values)
            {
                arc.DrawSize = newDrawSize;
            }
            foreach (IVisualNode node in VisualNodes.Values)
            {
                node.DrawSize = newDrawSize;
            }
            foreach (INameField nField in NameFields.Values)
            {
                nField.DrawSize = newDrawSize;
            }
        }

        /// <summary>
        /// Returns a reference to the NameField with the given id.
        /// </summary>
        /// <param name="id">The id of the NameField to return.</param>
        /// <returns>The NameField with the specified id.</returns>
        public INameField GetNameField(String id)
        {
            return NameFields[id];
        }

        /// <summary>
        /// Returns a reference to the NameField with the given index in the
        /// underlying collection. This method can be used to iterate over
        /// all name fields.
        /// </summary>
        /// <param name="i">The index of the NameField to return.</param>
        /// <returns>The NameField with the specified index.</returns>
        public INameField GetNameField(int i)
        {
            return NameFields.ElementAt(i).Value;
        }

        /// <summary>
        /// Adds the specified NameField to the visual presentation of the petrinet.
        /// </summary>
        /// <param name="nf">A reference to the NameField that is to be added.</param>
        public void AddNameField(INameField nf)
        {
            NameFields.Add(nf.Id, nf);
        }

        /// <summary>
        /// Removes the NameField with the specified id from the visual presentation
        /// of the petrinet.
        /// </summary>
        /// <param name="id">The id of the NameField to remove.</param>
        public void RemoveNameField(String id)
        {
            NameFields.Remove(id);
        }

        /// <summary>
        /// Returns a reference to the VisualNode with the given id.
        /// </summary>
        /// <param name="id">The id of the VisualNode to return.</param>
        /// <returns>The VisualNode with the specified id.</returns>
        public IVisualNode GetNode(String id)
        {
            return VisualNodes[id];
        }

        /// <summary>
        /// Returns a reference to the VisualNode with the given index in the
        /// underlying collection. This method can be used to iterate over
        /// all visual nodes.
        /// </summary>
        /// <param name="i">The index of the VisualNode to return.</param>
        /// <returns>The VisualNode with the specified index.</returns>
        public IVisualNode GetNode(int i)
        {
            return VisualNodes.ElementAt(i).Value;
        }

        /// <summary>
        /// Gets the id of the VisualNode with the given index in the underlying 
        /// collection. This method can be used to iterate over the ids of 
        /// all visual nodes.
        /// </summary>
        /// <param name="i">The index of the VisualNode to return the id of.</param>
        /// <returns>The id of the VisualNode with the specified index.</returns>
        public String GetNodeId(int i)
        {
            return VisualNodes.ElementAt(i).Key;
        }

        /// <summary>
        /// Adds the specified VisualNode to the visual presentation of the petrinet.
        /// </summary>
        /// <param name="node">A reference to the VisualNode that is to be added.</param>
        public void AddNode(IVisualNode node)
        {
            VisualNodes.Add(node.Id, node);
        }

        /// <summary>
        /// Removes the VisualNode with the specified id from the visual presentation
        /// of the petrinet.
        /// </summary>
        /// <param name="id">The id of the VisualNode to remove.</param>
        public void RemoveNode(String id)
        {
            VisualNodes.Remove(id);
        }

        /// <summary>
        /// Removes the specified VisualNode from the visual presentation of the petrinet.
        /// </summary>
        /// <param name="node">The VisualNode to remove.</param>
        public void RemoveNode(IVisualNode node)
        {
            VisualNodes.Remove(node.Id);
        }

        /// <summary>
        /// Returns a reference to the VisualArc with the given id.
        /// </summary>
        /// <param name="id">The id of the VisualArc to return.</param>
        /// <returns>The VisualArc with the specified id.</returns>
        public IVisualArc GetArc(String id)
        {
            return VisualArcs[id];
        }

        /// <summary>
        /// Returns a reference to the VisualArc with the given index in the
        /// underlying collection. This method can be used to iterate over
        /// all visual arcs.
        /// </summary>
        /// <param name="i">The index of the VisualArc to return.</param>
        /// <returns>The VisualArc with the specified index.</returns>
        public IVisualArc GetArc(int i)
        {
            return VisualArcs.ElementAt(i).Value;
        }

        /// <summary>
        /// Gets the id of the VisualArc with the given index in the underlying 
        /// collection. This method can be used to iterate over the ids of 
        /// all visual arcs.
        /// </summary>
        /// <param name="i">The index of the VisualArc to return the id of.</param>
        /// <returns>The id of the VisualArc with the specified index.</returns>
        public String GetArcId(int i)
        {
            return VisualArcs.ElementAt(i).Key;
        }

        /// <summary>
        /// Adds the specified VisualArc to the visual presentation of the petrinet.
        /// </summary>
        /// <param name="arc">A reference to the VisualArc that is to be added.</param>
        public void AddArc(IVisualArc arc)
        {
            VisualArcs.Add(arc.Id, arc);
        }

        /// <summary>
        /// Removes the VisualArc with the specified id from the visual presentation
        /// of the petrinet.
        /// </summary>
        /// <param name="id">The id of the VisualArc to remove.</param>
        public void RemoveArc(String id)
        {
            VisualArcs.Remove(id);
        }

        /// <summary>
        /// Removes the specified VisualArc from the visual presentation of the petrinet.
        /// </summary>
        /// <param name="arc">The VisualArc to remove.</param>
        public void RemoveArc(IVisualArc arc)
        {
            VisualArcs.Remove(arc.Id);
        }

        /// <summary>
        /// Clears all NameFields from the presentation of the petrinet.
        /// </summary>
        public void ClearNameFields()
        {
            NameFields.Clear();
        }

        /// <summary>
        /// Clears all VisualNodes from the presentation of the petrinet.
        /// </summary>
        public void ClearNodes()
        {
            VisualNodes.Clear();
        }

        /// <summary>
        /// Clears all VisualArcs from the presentation of the petrinet.
        /// </summary>
        public void ClearArcs()
        {
            VisualArcs.Clear();
        }
        #endregion
    }
}
