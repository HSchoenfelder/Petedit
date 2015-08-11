using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Represents a visual arc in the presentation of the petrinet.
    /// </summary>
    public interface IVisualArc
    {
        /// <summary> Gets the Id that uniquely identifies this visual arc. </summary>
        String Id { get; }

        /// <summary> Sets the draw size of this visual arc. </summary>
        int DrawSize { set; }

        /// <summary> Sets the size of the arrowhead of this visual arc. </summary>
        int ArrowheadSize { set; }

        /// <summary>
        /// Gets or sets the id of the brother of this VisualArc in case of a double arc between two nodes.
        /// <summary>
        String BrotherId { get; set; }

        /// <summary>
        /// Sets a value which indicates whether this visual arc is currently selected in 
        /// the graphical presentation.
        /// </summary>
        bool Selected { get; set; }

        /// <summary>
        /// Sets a value which indicates whether this visual arc is currently selected in
        /// the graphical presentation because an adjacent node is selected.
        /// </summary>
        bool AutoSelected { set; }

        /// <summary>
        /// Sets the coordinates of the center of the source of this visual arc.
        /// </summary>
        NPoint SourceNodePos { set; }

        /// <summary>
        /// Sets the coordinates of the center of the target of this visual arc.
        /// </summary>
        NPoint TargetNodePos { set; }
    }
}
