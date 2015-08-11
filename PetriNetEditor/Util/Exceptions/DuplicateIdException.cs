using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// This class represents an exception that is thrown when a PNML file that has been read
    /// contains duplicate Ids.
    /// </summary>
    public class DuplicateIdException : Exception
    {
        /// <summary>
        /// Initializes a new exception with the corresponding error message.
        /// </summary>
        public DuplicateIdException() : base("The file contains elements with duplicate Ids.") 
        { }
    }
}
