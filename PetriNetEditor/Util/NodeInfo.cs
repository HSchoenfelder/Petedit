using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// This class provides a data strucure for storing the information needed to restore 
    /// a VisualNode.
    /// </summary>
    public class NodeInfo
    {
        #region properties
        /// <summary> Gets the Id that uniquely identifies this Node. </summary>
        public String Id { get; private set; }

        /// <summary> Gets the name of this Node. </summary>
        public String Name { get; private set; }

        /// <summary> Gets the x-coordinate of this Node. </summary>
        public int X { get; private set; }

        /// <summary> Gets the y-coordinate of this Node. </summary>
        public int Y { get; private set; }

        /// <summary> Gets the current amount of tokens on this Place. </summary>
        public int? TokenCount { get; private set; }
        #endregion

        #region constructors
        /// <summary>
        /// Saves the specified node information into a new NodeInfo. 
        /// </summary>
        /// <param name="id">The id of the Node.</param>
        /// <param name="name">The name of the Node</param>
        /// <param name="x">The x-coordinate of the Node.</param>
        /// <param name="y">The y-coordinate of the Node.</param>
        /// <param name="tokenCount">The amount of tokens on the Node.</param>
        /// 
        public NodeInfo(String id, String name, int x, int y, int? tokenCount)
        {
            Id = id;
            Name = name;
            X = x;
            Y = y;
            TokenCount = tokenCount;
        }
        #endregion
    }
}
