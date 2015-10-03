using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>Handles the BlockStateChangedEvent.</summary>
    /// <param name="e">The data for the BlockStateChangedEvent.</param>
    public delegate void BlockStateChangedEventHandler(object source, StateChangedEventArgs e);

    /// <summary>Handles the ViewSizeChangedEvent.</summary>
    /// <param name="e">The data for the ViewSizeChangedEvent.</param>
    public delegate void ViewSizeChangedEventHandler(object source, ViewSizeChangedEventArgs e);

    /// <summary>Handles the DrawingStateChangedEvent.</summary>
    /// <param name="e">The data for the DrawingStateChangedEvent.</param>
    public delegate void DrawingStateChangedEventHandler(object source, StateChangedEventArgs e);
}
