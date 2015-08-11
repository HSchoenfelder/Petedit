using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    /// <summary>
    /// This class provides data for the TokenChangedEvent.
    /// </summary>
    public class TokensChangedEventArgs
    {
        #region properties
        /// <summary>Gets the id of the Place for which the event was raised.</summary>
        public String PlaceId { get; private set; }

        /// <summary>Gets the new amount of tokens on the Place for which the event was raised.</summary>
        public int TokenCount { get; private set; }
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the TokensChangedEventArgs class.
        /// </summary>
        /// <param name="placeId">The id of the Place for which the event was raised.</param>
        /// <param name="tokenCount">The new amount of tokens on the Place.</param>
        public TokensChangedEventArgs(String placeId, int tokenCount)
        {
            PlaceId = placeId;
            TokenCount = tokenCount;
        }
        #endregion
    }
}
