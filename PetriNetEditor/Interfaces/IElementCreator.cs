using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    public interface IElementCreator
    {
        /// <summary>
        /// Creates a new visual node with the specified id, adds it to the presentation and adds it to the model 
        /// as a place. Adjusts the size of the presentation area and scrolls according to the new visual nodes 
        /// position.
        /// </summary>
        /// <param name="pos">The position on which the new place is to be created.</param>
        /// <param name="id">The id of the place to be created.</param>
        /// <returns>The id of the created place.</returns>
        String CreatePlace(Point pos, String id);

        /// <summary>
        /// Creates a new visual node, adds it to the presentation and adds it to the model as a place.
        /// Adjusts the size of the presentation area and scrolls according to the new visual nodes position.
        /// </summary>
        /// <param name="pos">The position on which the new place is to be created.</param>
        /// <returns>The id of the created transition.</returns>
        String CreatePlace(Point pos);

        /// <summary>
        /// Creates a new visual node with the specified id, adds it to the presentation and adds it to the model 
        /// as a transition. Adjusts the size of the presentation area and scrolls according to the new visual 
        /// nodes position.
        /// </summary>
        /// <param name="pos">The position on which the new transition is to be created.</param>
        /// <param name="id">The id of the transition to be created.</param>
        /// /// <returns>The id of the created transition.</returns>
        String CreateTrans(Point pos, String id);

        /// <summary>
        /// Creates a new visual node, adds it to the presentation and adds it to the model as a transition.
        /// Adjusts the size of the presentation area and scrolls according to the new visual nodes position.
        /// </summary>
        /// <param name="pos">The position on which the new transition is to be created.</param>
        /// <returns>The id of the created place.</returns>
        String CreateTrans(Point pos);

        /// <summary>
        /// Creates a new visual arc, adds it to the presentation and adds it to the model as an arc.
        /// </summary>
        /// <param name="id"> The id of the arc to be created. </param>
        /// <param name="sourceId"> The id of the source node of the arc. </param>
        /// <param name="targetId"> The id of the target node of the arc. </param>
        /// <param name="selected"> This value indicates whether the arc is to be created in selected state. </param>
        void CreateArc(String id, String sourceId, String targetId, bool selected);

        /// <summary>
        /// Sets a name for the node with the specified id and updates the model with the new name.
        /// </summary>
        /// <param name="id">The id of the node for which to set the name.</param>
        /// <param name="name">The new name of the node.</param>
        void SetNodeName(String id, String name);

        /// <summary>
        /// Sets a token value for the place with the specified id and updates the model with the n
        /// ew value.
        /// </summary>
        /// <param name="id">The id of the node for which to set the token value.</param>
        /// <param name="tokens">The new token value.</param>
        void SetPlaceTokens(String id, int tokens);
    }
}
