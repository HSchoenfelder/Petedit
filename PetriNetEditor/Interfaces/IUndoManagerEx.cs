using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides methods for managing the undo and the redo queue for the element manager.
    /// </summary>
    public interface IUndoManagerEx : IUndoManager
    {
        #region properties
        /// <summary> 
        /// Gets or sets the amount of right shift during the current move operation. 
        /// </summary>
        double MoveRightShift { get; set; }

        /// <summary> 
        /// Gets or sets the amount of down shift during the current move operation. 
        /// </summary>
        double MoveDownShift { get; set; }
        
        Point MoveUndoStart { get; set; }

        /// <summary> 
        /// Gets or sets the size of the view at the start of the move operation to undo. 
        /// </summary>
        Point MoveUndoViewSize { get; set; }

        /// <summary> Gets the command that performs an undo operation. </summary>
        IDelegateCommand UndoCommand { get; }
        #endregion

        #region methods
        /// <summary>
        /// Assembles the complete parameters for a move undo operation.
        /// </summary>
        /// <param name="xNewPos">The x-coordinate of the position after the move.</param>
        /// <param name="yNewPos">The y-coordinate of the position after the move.</param>
        void AssembleMoveUndoParams(double xNewPos, double yNewPos);

        /// <summary>
        /// Resets the move undo parameters for the next use.
        /// </summary>
        void ClearMoveUndoParams();
        #endregion
    }
}
