using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetModel
{
    /// <summary>
    /// This class implements a data structure for a petrinet and provides its basic functionality.
    /// </summary>
    public class ModelMain : IModel
    {
        #region fields
        /// <summary>Store for the dictionary of contained nodes. </summary>
        private IDictionary<String, INode> _nodes;
        /// <summary>Store for the dictionary of contained arcs. </summary>
        private IDictionary<String, IArc> _arcs;
        #endregion

        #region events
        /// <summary>Occurs when any Transition of the petrinet changes state. </summary>
        public event TransitionStateChangedEventHandler TransitionStateChangedEvent;
        
        /// <summary>Occurs when the amount of tokens on any Place of the petrinet changes.</summary>
        public event TokensChangedEventHandler TokensChangedEvent;
        #endregion

        #region properties
        /// <summary>Gets or sets the dictionary that manages the individual nodes via their ids.</summary>
        private IDictionary<String, INode> Nodes
        {
            get { return _nodes; }
            set { _nodes = value; }
        }

        /// <summary>Gets or sets the dictionary that manages the individual arcs via their ids.</summary>
        private IDictionary<String, IArc> Arcs
        {
            get { return _arcs; }
            set { _arcs = value; }
        }
        #endregion

        #region constructors
        /// <summary>Initializes a new instance of the ModelMain class.</summary>
        public ModelMain()
        {
            _nodes = new Dictionary<String, INode>();
            _arcs = new Dictionary<String, IArc>();
        }
        #endregion

        #region methods

        #region private
        /// <summary>Raises the TransitionStateChangedEvent.</summary>
        /// <param name="transId">The id of the Transition that caused the event.</param>
        /// <param name="active">The new status of the Transition that caused the event.</param>
        private void OnTransitionStateChanged(String transId, bool active)
        {
            if (TransitionStateChangedEvent != null)
                TransitionStateChangedEvent(new TransitionStateChangedEventArgs(transId, active));
        }

        /// <summary>Raises the TokensChangedEvent.</summary>
        /// <param name="placeId">The id of the Place that caused the event.</param>
        /// <param name="tokenCount">The new amount of tokens on the Place that caused the event.</param>
        private void OnTokensChanged(String placeId, int tokenCount)
        {
            if (TokensChangedEvent != null)
                TokensChangedEvent(new TokensChangedEventArgs(placeId, tokenCount));
        }
        #endregion

        #region public
        /// <summary> 
        /// Gets the coordinates of the Node specified by the provided id.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to return coordinates.</param>
        /// <returns>The coordinates of the specified Node.</returns>
        public Point GetCoordinates(String nodeId)
        {
            INode node = Nodes[nodeId];
            return new Point(node.X, node.Y);
        }

        /// <summary>
        /// Gets the name of the Node specified by the provided id.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to return the name.</param>
        /// <returns>The name of the specified Node.</returns>
        public String GetName(String nodeId)
        {
            return Nodes[nodeId].Name;
        }

        /// <summary>
        /// Gets the amount of tokens on the Place specified by the provided id.
        /// </summary>
        /// <param name="placeId">The id of the Place for which to return tokens.</param>
        /// <returns>The amount of tokens on the specified Place.</returns>
        public int GetTokenCount(String placeId)
        {
            IPlace place = (IPlace)Nodes[placeId];
            return place.TokenCount;
        }

        /// <summary>
        /// Gets the state of the Transition specified by the provided id.
        /// </summary>
        /// <param name="transId">The id of the Transition for which to return state.</param>
        /// <returns>The state of the specified Transition.</returns>
        public bool GetState(String transId)
        {
            ITransition trans = (ITransition)Nodes[transId];
            return trans.Enabled;
        }

        /// <summary>
        /// Gets all arcs that are connected to the Node specified by the provided id.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to return all connected arcs.</param>
        /// <returns>A list with the ids of all arcs that are conneced to the specified Node.</returns>
        public List<String> GetArcsForNode(String nodeId)
        {
            INode node = Nodes[nodeId];
            return node.GetArcList();
        }

        /// <summary>
        /// Gets the nodes to which the Arc specified by the provided id is connected.
        /// </summary>
        /// <param name="arcId">The id of the Arc for which to return connected nodes.</param>
        /// <returns>An array with the ids of the nodes to which the specified Arc is connected.</returns>
        public String[] GetNodesForArc(String arcId)
        {
            String[] connectedNodes = new String[2];
            connectedNodes[0] = Arcs[arcId].GetSourceId();
            connectedNodes[1] = Arcs[arcId].GetTargetId();
            return connectedNodes;
        }

        /// <summary>
        /// Gets the Node that is connected to the Node specified via the provided id through the
        /// Arc specivied via the provided id.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to return the connected node.</param>
        /// <param name="arcId">The id of the Arc that connects to the sought Node.</param>
        /// <returns>The Node that is connected to the specified Node via the specified Arc.</returns>
        public String GetConnectedNode(String nodeId, String arcId)
        {
            IArc arc = Arcs[arcId];
            return nodeId.Equals(arc.GetSourceId()) ? arc.GetTargetId() : arc.GetSourceId();
        }

        /// <summary>
        /// Returns the id of the Arc between the two specified nodes. If no Arc exists between the nodes, returns null.
        /// </summary>
        /// <param name="sourceId">The id of the potential source.</param>
        /// <param name="targetId">The id of the potential target.</param>
        /// <returns>The id of the Arc between the two nodes or null if no such Arc exists.</returns>
        public String GetArc(String sourceId, String targetId)
        {
            foreach (IArc arc in Arcs.Values)
            {
                if (arc.GetSourceId().Equals(sourceId) && arc.GetTargetId().Equals(targetId))
                    return arc.Id;
            }
            return null;
        }

        /// <summary>
        /// Determines whether the Node specified by the provided id is the source of the
        /// Arc specified by the provided id.
        /// </summary>
        /// <param name="arcId">The id of the Arc to set the Node into relation to.</param>
        /// <param name="nodeId">The id of the Node to check the source trait of.</param>
        /// <returns>true if the specified Node is the source of the specified Arc; 
        /// otherwise false.</returns>
        public bool IsSource(String arcId, String nodeId)
        {
            IArc arc = Arcs[arcId];
            return arc.GetSourceId().Equals(nodeId);
        }

        /// <summary>
        /// Determines whether the Node specified by the provided id is a place.
        /// </summary>
        /// <param name="nodeId">The id of the Node to check.</param>
        /// <returns>true if the specified Node is a place; otherwise false.</returns>
        public bool IsPlace(String nodeId)
        {
            INode node = Nodes[nodeId];
            return node is IPlace;
        }

        /// <summary>
        /// Adds a new Place with the specified coordinates and id to the petrinet.
        /// </summary>
        /// <param name="xCoordinate">The x-coordinate of the new Place.</param>
        /// <param name="yCoordinate">The y-coordinate of the new Place.</param>
        /// <param name="id">The id of the new Place.</param>
        public void AddPlace(int xCoordinate, int yCoordinate, String id)
        {
            Nodes.Add(id, ElementFactory.CreatePlace(xCoordinate, yCoordinate, id));
        }

        /// <summary>
        /// Adds a new Transition with the specified coordinates and id to the petrinet.
        /// </summary>
        /// <param name="xCoordinate">The x-coordinate of the new Transition.</param>
        /// <param name="yCoordinate">The y-coordinate of the new Transition.</param>
        /// <param name="id">The id of the new Transition.</param>
        public void AddTransition(int xCoordinate, int yCoordinate, String id)
        {
            Nodes.Add(id, ElementFactory.CreateTransition(xCoordinate, yCoordinate, id));
        }

        /// <summary>
        /// Adds a new Arc with the provided id and the source and target nodes specified 
        /// by the provided ids to the petrinet. 
        /// </summary>
        /// <param name="sourceId">The id of the source Node of the new Arc.</param>
        /// <param name="targetId">The id of the target Node of the new Arc.</param>
        /// <param name="id">The id of the new Arc.</param>
        public void AddArc(String sourceId, String targetId, String id)
        {
            IArc createdArc = ElementFactory.CreateArc(Nodes[sourceId], Nodes[targetId], id);
            createdArc.Add(OnTransitionStateChanged);
            Arcs.Add(id, createdArc);
        }

        /// <summary>
        /// Determines whether the element specified by the provided id is a Node or an Arc.
        /// </summary>
        /// <param name="id">The id of the element to check.</param>
        /// <returns>true if the specified element is a Node; otherwise false.</returns>
        public bool IsNode(String id)
        {
            if (Nodes.ContainsKey(id))
                return true;
            return false;
        }

            /// <summary>
            /// Determines whether the element specified by the provided id is present in the 
            /// petrinet.
            /// </summary>
            /// <param name="id">The id of the element to check for.</param>
            /// <returns>true if the specified element is present in the petrinet;
            /// otherwise false.</returns>
            public bool Contains(String id)
            {
                if (id == null)
                    throw new ArgumentNullException("id");
                bool found = false;
                foreach (String nodeId in Nodes.Keys)
                {
                    found = id.Equals(nodeId);
                    if (found)
                        return true;
                }
                foreach (String arcId in Arcs.Keys)
                {
                    found = id.Equals(arcId);
                    if (found)
                        return true;
                }
                return false;
            }

        /// <summary>
        /// Removes the Node specified by the provided id from the petrinet.
        /// </summary>
        /// <param name="id">The id of the Node to remove.</param>
        public void RemoveNode(String id)
        {
            Nodes.Remove(id);
        }

        /// <summary>
        /// Removes the Arc specified by the provided id from the petrinet.
        /// </summary>
        /// <param name="id">The id of the Arc to remove.</param>
        public void RemoveArc(String id)
        {
            IArc removedArc = Arcs[id];
            Arcs.Remove(id);
            removedArc.Remove(OnTransitionStateChanged);
        }

        /// <summary>
        /// Changes the name of the Node specified by the provided id to the specified name.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to change the name.</param>
        /// <param name="newName">The new name of the Node.</param>
        public void ChangeName(String nodeId, String newName)
        {
            INode changedNode = Nodes[nodeId];
            changedNode.Name = newName;
        }

        /// <summary>
        /// Changes the position of the Node specified by the provided id to the specified
        /// coordinates.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to change the coordinates.</param>
        /// <param name="xCoordinate">The new x-coordinate of the Node.</param>
        /// <param name="yCoordinate">The new y-coordinate of the Node.</param>
        public void ChangePosition(String nodeId, int xCoordinate, int yCoordinate)
        {
            INode changedNode = Nodes[nodeId];
            changedNode.X = xCoordinate;
            changedNode.Y = yCoordinate;
        }

        /// <summary>
        /// Changes the amount of tokens on the Place specified by the provided id to the
        /// specified amount.
        /// </summary>
        /// <param name="placeId">The id of the Place for which to change the token count.</param>
        /// <param name="newTokenCount">The new amount of tokens for the Place.</param>
        public void ChangeTokens(String placeId, int newTokenCount)
        {
            IPlace changedPlace = (IPlace)Nodes[placeId];
            changedPlace.ChangeTokenCount(newTokenCount, OnTransitionStateChanged);
        }

        /// <summary>
        /// Performs a transition on the Transition specified by the provided id.
        /// </summary>
        /// <param name="transitionId">The id of the Transition.</param>
        public void PerformTransition(String transitionId)
        {
            ITransition transNode = (ITransition)Nodes[transitionId];
            transNode.TransitionTokens(OnTransitionStateChanged, OnTokensChanged);
        }

        /// <summary>
        /// Performs an inverse transition on the Transition specified by the provided id.
        /// </summary>
        /// <param name="transitionId">The id of the Transition.</param>
        public void InverseTransition(String transitionId)
        {
            ITransition transNode = (ITransition)Nodes[transitionId];
            transNode.InverseTransitionTokens(OnTransitionStateChanged, OnTokensChanged);
        }

        /// <summary>
        /// Reinitalizes the petrinet by removing all nodes and arcs.
        /// </summary>
        public void Reinitialize()
        {
            Nodes = new Dictionary<String, INode>();
            Arcs = new Dictionary<String, IArc>();
        }
        #endregion

        #endregion
    }
}
