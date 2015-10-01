using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    /// <summary>Handles the TransitionStateChangedEvent.</summary>
    /// <param name="e">The data for the TransitionStateChangedEvent.</param>
    public delegate void TransitionStateChangedEventHandler(TransitionStateChangedEventArgs e);

    /// <summary>Handles the TokensChangedEvent.</summary>
    /// <param name="e">The data for the TokensChangedEvent.</param>
    public delegate void TokensChangedEventHandler(TokensChangedEventArgs e);
}
