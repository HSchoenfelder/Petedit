using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides logic for the manipulation of the workspace area of the petrinet.
    /// </summary>
    public interface IWorkspaceManager
    {
        #region events
        /// <summary> Occurs when a modification is made to the model. </summary>
        event EventHandler Modified;

        /// <summary> Occurs when a reevaluation of command state is required. </summary>
        event EventHandler ReevaluateCommandState;

        /// <summary> Occurs when the selecting state changed. </summary>
        event SelectingStateChangedEventHandler SelectingStateChanged;
        #endregion

        #region properties
        /// <summary> 
        /// Gets or sets the width of the visual presentation of the petrinet. 
        /// </summary>
        double ViewWidth { get; set; }

        /// <summary> 
        /// Gets or sets the height of the visual presentation of the petrinet. 
        /// </summary>
        double ViewHeight { get; set; }

        /// <summary>
        /// Gets or sets a value which indicates whether a rectangle select operation is
        /// currently in progress.
        /// </summary>
        bool Selecting { get; set; }

        /// <summary>
        /// Gets or sets a value which indicates whether an arc drawing operation is
        /// currently in progress.
        /// </summary>
        bool Drawing { get; set; }
        #endregion
    }
}
