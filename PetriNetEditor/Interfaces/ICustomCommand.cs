using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// This class represents a custom command for command testing.
    /// </summary>
    public interface ICustomCommand : IDelegateCommand
    {
        /// <summary> The type of the command to be tested. </summary>
        CommandTypes CommandType { get; set; }
    }
}
