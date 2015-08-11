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
        #region methods
        /// <summary>
        /// Gets all arcs that are connected to the Node specified by the provided id.
        /// </summary>
        /// <param name="nodeId">The id of the Node for which to return all connected arcs.</param>
        /// <returns>A list with the ids of all arcs that are conneced to the specified Node.</returns>
        List<String> GetArcsForNode(String nodeId);

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
        /// Determines whether the Node specified by the provided id is the source of the
        /// Arc specified by the provided id.
        /// </summary>
        /// <param name="arcId">The id of the Arc to set the Node into relation to.</param>
        /// <param name="nodeId">The id of the Node to check the source trait of.</param>
        /// <returns>true if the specified Node is the source of the specified Arc; 
        /// otherwise false.</returns>
        bool IsSource(String arcId, String nodeId);

        /// <summary>
        /// Determines whether the element specified by the provided id is a Node or an Arc.
        /// </summary>
        /// <param name="id">The id of the element to check.</param>
        /// <returns>true if the specified element is a Node; otherwise false.</returns>
        bool IsNode(String id);

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
        #endregion
    }
}
