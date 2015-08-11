using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// This class provides a data strucure for storing the information needed to restore 
    /// a VisualArc.
    /// </summary>
    public class ArcInfo
    {
        #region properties
        /// <summary> Gets the Id that uniquely identifies this Arc. </summary>
        public String Id { get; private set; }

        /// <summary> Gets the source Node of this Arc. </summary>
        public String SourceId { get; private set; }

        /// <summary> Gets the target Node of this Arc. </summary>
        public String TargetId { get; private set; }

        /// <summary> Gets the selection state of this Arc. </summary>
        public bool Selected { get; private set; }
        #endregion

        #region constructors
        /// <summary>
        /// Saves the specified arc information into a new ArcInfo. 
        /// </summary>
        /// <param name="id">The id of the Arc.</param>
        /// <param name="sourceId">The id of the source node of the Arc.</param>
        /// <param name="targetId">The id of the target node of the Arc.</param>
        /// <param name="selected">The selection state of the Arc.</param>
        /// 
        public ArcInfo(String id, String sourceId, String targetId, bool selected)
        {
            Id = id;
            SourceId = sourceId;
            TargetId = targetId;
            Selected = selected;
        }
        #endregion
    }
}
