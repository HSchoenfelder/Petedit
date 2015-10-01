using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetModel
{
    /// <summary>
    /// Represents a petrinet model that provides methods commonly accessed
    /// by visual elements.
    /// </summary>
    public interface IModel
    {
        #region events
        /// <summary>Occurs when any Transition of the petrinet changes state. </summary>
        event TransitionStateChangedEventHandler TransitionStateChangedEvent;

        /// <summary>Occurs when the amount of tokens on any Place of the petrinet changes.</summary>
        event TokensChangedEventHandler TokensChangedEvent;
        #endregion

        #region methods
        /// <summary> 
        /// Gets the coordinates of the Node specified by the provided id.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to return coordinates.</param>
        /// <returns>The coordinates of the specified Node.</returns>
        Point GetCoordinates(String nodeId);

        /// <summary>
        /// Gets the name of the Node specified by the provided id.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to return the name.</param>
        /// <returns>The name of the specified Node.</returns>
        String GetName(String nodeId);

        /// <summary>
        /// Gets the amount of tokens on the Place specified by the provided id.
        /// </summary>
        /// <param name="placeId">The id of the Place for which to return tokens.</param>
        /// <returns>The amount of tokens on the specified Place.</returns>
        int GetTokenCount(String placeId);

        /// <summary>
        /// Gets the state of the Transition specified by the provided id.
        /// </summary>
        /// <param name="transId">The id of the Transition for which to return state.</param>
        /// <returns>The state of the specified Transition.</returns>
        bool GetState(String transId);

        /// <summary>
        /// Gets all arcs that are connected to the Node specified by the provided id.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to return all connected arcs.</param>
        /// <returns>A list with the ids of all arcs that are conneced to the specified Node.</returns>
        List<String> GetArcsForNode(String nodeId);

        /// <summary>
        /// Gets the nodes to which the Arc specified by the provided id is connected.
        /// </summary>
        /// <param name="arcId">The id of the Arc for which to return connected nodes.</param>
        /// <returns>An array with the ids of the nodes to which the specified Arc is connected.</returns>
        String[] GetNodesForArc(String arcId);

        /// <summary>
        /// Gets the Node that is connected to the Node specified via the provided id through the
        /// Arc specivied via the provided id.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to return the connected node.</param>
        /// <param name="arcId">The id of the Arc that connects to the sought Node.</param>
        /// <returns>The Node that is connected to the specified Node via the specified Arc.</returns>
        String GetConnectedNode(String nodeId, String arcId);

        /// <summary>
        /// Returns the id of the Arc between the two specified nodes. If no Arc exists between the nodes, returns null.
        /// </summary>
        /// <param name="sourceId">The id of the potential source.</param>
        /// <param name="targetId">The id of the potential target.</param>
        /// <returns>The id of the Arc between the two nodes or null if no such Arc exists.</returns>
        String GetArc(String sourceId, String targetId);

        /// <summary>
        /// Adds a new Place with the specified coordinates and id to the petrinet.
        /// </summary>
        /// <param name="xCoordinate">The x-coordinate of the new Place.</param>
        /// <param name="yCoordinate">The y-coordinate of the new Place.</param>
        /// <param name="id">The id of the new Place.</param>
        void AddPlace(int xCoordinate, int yCoordinate, String id);

        /// <summary>
        /// Adds a new Transition with the specified coordinates and id to the petrinet.
        /// </summary>
        /// <param name="xCoordinate">The x-coordinate of the new Transition.</param>
        /// <param name="yCoordinate">The y-coordinate of the new Transition.</param>
        /// <param name="id">The id of the new Transition.</param>
        void AddTransition(int xCoordinate, int yCoordinate, String id);

        /// <summary>
        /// Adds a new Arc with the provided id and the source and target nodes specified 
        /// by the provided ids to the petrinet. 
        /// </summary>
        /// <param name="sourceId">The id of the source Node of the new Arc.</param>
        /// <param name="targetId">The id of the target Node of the new Arc.</param>
        /// <param name="id">The id of the new Arc.</param>
        void AddArc(String sourceId, String targetId, String id);

        /// <summary>
        /// Determines whether the Node specified by the provided id is the source of the
        /// Arc specified by the provided id.
        /// </summary>
        /// <param name="arcId">The id of the Arc to set the Node into relation to.</param>
        /// <param name="nodeId">The id of the Node to check the source trait of.</param>
        /// <returns>true if the specified Node is the source of the specified Arc; 
        /// otherwise false.</returns>
        bool IsSource(String arcId, String nodeId);

        /// <summary>
        /// Determines whether the Node specified by the provided id is a place.
        /// </summary>
        /// <param name="nodeId">The id of the Node to check.</param>
        /// <returns>true if the specified Node is a place; otherwise false.</returns>
        bool IsPlace(String nodeId);

        /// <summary>
        /// Determines whether the element specified by the provided id is a Node or an Arc.
        /// </summary>
        /// <param name="id">The id of the element to check.</param>
        /// <returns>true if the specified element is a Node; otherwise false.</returns>
        bool IsNode(String id);

        /// <summary>
        /// Determines whether the element specified by the provided id is present in the 
        /// petrinet.
        /// </summary>
        /// <param name="id">The id of the element to check for.</param>
        /// <returns>true if the specified element is present in the petrinet;
        /// otherwise false.</returns>
        bool Contains(String id);

        /// <summary>
        /// Removes the Node specified by the provided id from the petrinet.
        /// </summary>
        /// <param name="id">The id of the Node to remove.</param>
        void RemoveNode(String id);

        /// <summary>
        /// Removes the Arc specified by the provided id from the petrinet.
        /// </summary>
        /// <param name="id">The id of the Arc to remove.</param>
        void RemoveArc(String id);

        /// <summary>
        /// Changes the name of the Node specified by the provided id to the specified name.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to change the name.</param>
        /// <param name="newName">The new name of the Node.</param>
        void ChangeName(String nodeId, String newName);

        /// <summary>
        /// Changes the position of the Node specified by the provided id to the specified
        /// coordinates.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to change the coordinates.</param>
        /// <param name="xCoordinate">The new x-coordinate of the Node.</param>
        /// <param name="yCoordinate">The new y-coordinate of the Node.</param>
        void ChangePosition(String nodeId, int xCoordinate, int yCoordinate);

        /// <summary>
        /// Changes the amount of tokens on the Place specified by the provided id to the
        /// specified amount.
        /// </summary>
        /// <param name="placeId">The id of the Place for which to change the token count.</param>
        /// <param name="newTokenCount">The new amount of tokens for the Place.</param>
        void ChangeTokens(String placeId, int newTokenCount);

        /// <summary>
        /// Performs a transition on the Transition specified by the provided id.
        /// </summary>
        /// <param name="transitionId">The id of the Transition.</param>
        void PerformTransition(String transitionId);
        
        /// <summary>
        /// Performs an inverse transition on the Transition specified by the provided id.
        /// </summary>
        /// <param name="transitionId">The id of the Transition.</param>
        void InverseTransition(String transitionId);

        /// <summary>
        /// Reinitalizes the petrinet by removing all nodes and arcs.
        /// </summary>
        void Reinitialize();
        #endregion
    }
}
