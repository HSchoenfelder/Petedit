using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    /// <summary>
    /// This class represents a node in the petrinet.
    /// </summary>
    internal abstract class Node : INode
    {
        #region fields
        /// <summary> Store for the Id property. </summary>
        private readonly String _id;

        /// <summary> Store for the Name property. </summary>
        private String _name;

        /// <summary> Store for the X property. </summary>
        private int _x;

        /// <summary> Store for the Y property. </summary>
        private int _y;

        /// <summary> Store for the Prececessors property. </summary>
        private readonly IList<INode> _predecessors;

        /// <summary> Store for the Successors property. </summary>
        private readonly IList<INode> _successors;

        /// <summary> Store for the Arcs property. </summary>
        private readonly IList<IArc> _arcs;
        #endregion

        #region properties
        /// <summary>Gets the Id that uniquely identifies this Node. </summary>
        public String Id
        {
            get { return _id; }
        }

        /// <summary>Gets or sets the name of this Node. </summary>
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>Gets or sets the x-coordinate of this Node. </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>Gets or sets the y-coordinate of this Node. </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>Gets a list of all predecessors of this Node. </summary>
        public IList<INode> Predecessors
        {
            get { return _predecessors; }
        }

        /// <summary>Gets a list of all successors of this Node. </summary>
        public IList<INode> Successors
        {
            get { return _successors; }
        }

        /// <summary> Gets a list of all arcs from and to this Node. </summary>
        internal IList<IArc> Arcs
        {
            get { return _arcs; }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the Node class with the specified coordinates and id. 
        /// </summary>
        /// <param name="x">The x-coordinate of the new Node.</param>
        /// <param name="y">The y-coordinate of the new Node.</param>
        /// <param name="id">The id of the new Node.</param>
        protected Node(int x, int y, String id)
        {
            if (x < 0)
                throw new ArgumentOutOfRangeException("x", x, "negative coordinate");
            if (y < 0)
                throw new ArgumentOutOfRangeException("y", y, "negative coordinate");
            if (id == null)
                throw new ArgumentNullException("id");
            _x = x;
            _y = y;
            _id = id;
            _predecessors = new List<INode>();
            _successors = new List<INode>();
            _arcs = new List<IArc>();
        }
        #endregion

        #region methods
        /// <summary>
        /// Adds the specified arc to this Node.
        /// </summary>
        /// <param name="addedArc">The arc to be added to this Node.</param>
        public void AddArc(IArc addedArc)
        {
            if (addedArc != null)
            {
                foreach (IArc arc in Arcs)
                {
                    if (arc.Equals(addedArc)) { return; }
                }
                Arcs.Add(addedArc);
            }
            else
                throw new ArgumentNullException("addedArc");
        }

        /// <summary>
        /// Removes the specified arc from this Node.
        /// </summary>
        /// <param name="deletedArc">The arc to be removed from this Node.</param>
        public void RemoveArc(IArc deletedArc)
        {
            Arcs.Remove(deletedArc);
        }

        /// <summary>
        /// Returns a list with the ids of all the arcs for this Node.
        /// </summary>
        /// <returns>A list with the ids of all the arcs for this Node.</returns>
        public List<String> GetArcList()
        {
            List<String> arcIds = new List<String>();
            foreach (Arc arc in Arcs)
            {
                arcIds.Add(arc.Id);
            }
            return arcIds;
        }

        /// <summary>
        /// Adds the specified Node as a predecessor of this Node.
        /// </summary>
        /// <param name="predecessor">The Node to be added as a predecessor.</param>
        public void AddPredecessor(INode predecessor)
        {
            if (predecessor != null)
            {
                foreach (INode node in Predecessors)
                {
                    if (node.Equals(predecessor)) { return; }
                }
                Predecessors.Add(predecessor);
            }
            else
                throw new ArgumentNullException("predecessor");
        }

        /// <summary>
        /// Removes the specified Node as a predecessor for this Node.
        /// </summary>
        /// <param name="deletedNode">The Node to be removed as a predecessor.</param>
        public void RemovePredecessor(INode deletedNode)
        {
            Predecessors.Remove(deletedNode);
        }

        /// <summary>
        /// Adds the specified Node as a successor of this Node.
        /// </summary>
        /// <param name="successor">The Node to be added as a successor.</param>
        public void AddSuccessor(INode successor)
        {
            if (successor != null)
            {
                foreach (INode node in Successors)
                {
                    if (node.Equals(successor)) { return; }
                }
                Successors.Add(successor);
            }
            else
                throw new ArgumentNullException("successor");
        }

        /// <summary>
        /// Removes the specified Node as a successor for this Node.
        /// </summary>
        /// <param name="deletedNode">The Node to be removed as a successor.</param>
        public void RemoveSuccessor(INode deletedNode)
        {
            Successors.Remove(deletedNode);
        }

        /// <summary>
        /// Determines whether the specified object is a Node and whether it contains the same id as this Node.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>true if obj is a Node and contains the same id as this Node; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj is INode)
            {
                if ((obj as INode).Id.Equals(Id))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the hash code for this Node.
        /// </summary>
        /// <returns>The hash code for this Node. </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
