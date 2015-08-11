using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides data related to the Notification event.
    /// </summary>
    public class NotificationEventArgs : EventArgs
    {
        /// <summary> Gets the message associated with the Notification event. </summary>
        public String Message { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the NotificationEventArgs class with the specified message. 
        /// </summary>
        /// <param name="message">The message associated with the Notification event.</param>
        public NotificationEventArgs(String message)
        {
            Message = message;
        }
    }
}
