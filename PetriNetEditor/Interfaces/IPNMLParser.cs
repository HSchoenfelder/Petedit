using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides methods for parsing a PNML document.
    /// </summary>
    public interface IPNMLParser
    {
        /// <summary>
        /// Reads the XML file and delegates the extracted XML elements to the respective
        /// methods.
        /// </summary>
        void Parse();

        /// <summary>
        /// Closes the XML Parser. Should be used to properly close the parser after an
        /// error that has been handled outside of the parser.
        /// </summary>
        void CloseParser();
    }
}
