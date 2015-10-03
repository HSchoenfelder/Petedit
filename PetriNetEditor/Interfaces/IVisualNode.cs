using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// Represents a visual node in the presentation of the petrinet.
    /// </summary>
    public interface IVisualNode
    {
        #region properties
        /// <summary> Gets the Id that uniquely identifies this visual node. </summary>
        String Id { get; }

        /// <summary> Gets or sets the draw size of this visual node. </summary>
        int DrawSize { get; set; }

        /// <summary> Gets or sets the x-coordinate of the center of this visual node. </summary>
        double XPos { get; set; }

        /// <summary> Gets or sets the y-coordinate of the center of this visual node. </summary>
        double YPos { get; set; }

        /// <summary> Gets the x-coordinate of the leftmost point of this VisualNode.  </summary>
        double XLeftEnd { get; }

        /// <summary> Gets the y-coordinate of the topmost point of this VisualNode. </summary>
        double YTopEnd { get; }

        /// <summary>
        /// Gets or sets a value which indicates whether this VisualNode is currently the source 
        /// of a drag operation.
        /// </summary>
        bool IsDragSource { get; set; }

        /// <summary>
        /// Gets or sets a value which indicates whether this visual node is currently selected in 
        /// the graphical presentation.
        /// </summary>
        bool Selected { get; set; }

        /// <summary> Gets the type of this visual node. </summary>
        NodeType NodeType { get; }

        /// <summary>
        /// Sets a value which indicates whether this visual node is currently the target 
        /// of a draw operation.
        /// </summary>
        bool IsDrawTarget { set; }

        /// <summary>
        /// Sets a value which indicates whether this visual node is currently manually highlighted.
        /// </summary>
        bool IsHighlighted { set; }

        /// <summary>
        /// Sets a value which indicates whether this VisualNode is currently beyond
        /// the bounds of the visual presentation and requires a scroll operation.
        /// </summary>
        bool IsBeyondEdge { set; }

        /// <summary>
        /// Gets or sets the current amount of tokens on this VisualNode.
        /// </summary>
        int TokenCount { get; set; }

        /// <summary>
        /// Sets a value which indicates whether this visual node is currently enabled.
        /// </summary>
        bool Enabled { set; }
        #endregion

        #region methods
        /// <summary>
        /// Sets the amount of tokens on this VisualNode to the specified value and updates the model 
        /// accordingly.
        /// </summary>
        /// <param name="tokens">The new token count of this VisualNode.</param>
        void SetTokens(int tokens);

        /// <summary>
        /// Determines whether the specified point is contained within the visual presentation of this
        /// visual node.
        /// </summary>
        /// <param name="p">The point for which to perform the containment check.</param>
        /// <returns>true if the point is contained within the visual presentation of this visual node;
        /// otherwise false.</returns>
        bool IsContained(Point p);

        /// <summary>
        /// Determines whether the center of the visual presentation of this visual node is contained 
        /// within the rectangle defined by the specified parameters.
        /// </summary>
        /// <param name="x">The x-coordinate of the top left point of the rectangle.</param>
        /// <param name="y">The y-coordinate of the top left point of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <returns>true if the visual presentation of the visual node is contained within the 
        /// rectangle; otherwise false.</returns>
        bool IsContained(double x, double y, double width, double height);
        #endregion
    }
}
