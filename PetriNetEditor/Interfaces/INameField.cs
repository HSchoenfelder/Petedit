using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides logic for a visual presentation of the Name of a Node 
    /// in the petrinet.
    /// </summary>
    public interface INameField
    {
        #region properties
        /// <summary> Gets the Id that uniquely identifies this name field. </summary>
        String Id { get; }

        /// <summary> Gets or sets the Name of this NameField. </summary>
        String Name { get; set; }

        /// <summary> Gets or sets the x-coordinate of this NameField. </summary>
        double XPos { get; set; }
        

        /// <summary> Gets or sets the y-coordinate of this NameField.</summary>
        double YPos { get; set; }

        /// <summary> Gets or sets the Width of this Namefield. </summary>
        double Width { get; set; }
        
        /// <summary> Gets or sets the Height of this Namefield. </summary>
        double Height { get; set; }

        /// <summary> Sets the draw size of the associated node. </summary>
        int DrawSize { set; }

        /// <summary>
        /// Sets a value which indicates whether the NameField currently has focus.
        /// </summary>
        bool TextFieldActive { set; }
        
        /// <summary>
        /// Gets or sets a value which indicates whether the NameField is currently visible.
        /// </summary>
        bool NameFieldVisible { get; set; }

        /// <summary>
        /// Sets a value which indicates whether this NameField is currently beyond
        /// the bounds of the visual presentation and requires a scroll operation.
        /// </summary>
        bool IsBeyondEdge { set; }
        #endregion

        #region methods
        /// <summary>
        /// Sets a new name to this NameField and writes it to the model.
        /// </summary>
        /// <param name="name">The new name for the NameField.</param>
        void SetName(String name);

        /// <summary>
        /// Adjust the position of the NameField after the position of its parent node has
        /// changed.
        /// </summary>
        void AdjustNameField();
        #endregion
    }
}
