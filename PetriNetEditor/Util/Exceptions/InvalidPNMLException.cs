using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// This class represents an exception that is thrown when an attempt has been made
    /// to read a file that is not a valid PNML file.
    /// </summary>
    public class InvalidPNMLException : Exception
    {
        /// <summary>
        /// Initializes a new exception with the corresponding error message.
        /// </summary>
        /// <param name="path"> The path to the file that causes the error. </param>
        public InvalidPNMLException(String path) : base(String.Format("The file {0} is not a valid PNML file.", path))
        { }
    }
}
