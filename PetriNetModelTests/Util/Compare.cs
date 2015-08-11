using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModelTests
{
    public class Compare
    {
        public static bool UnorderedEqual<T>(ICollection<T> a, ICollection<T> b)
        {
            // Require that the counts are equal
            if (a.Count != b.Count)
            {
                return false;
            }

            // Initialize new Dictionary of the type
            Dictionary<T, int> d = new Dictionary<T, int>();

            // Add each key's frequency from collection A to the Dictionary
            foreach (T item in a)
            {
                int c;
                if (d.TryGetValue(item, out c))
                {
                    d[item] = c + 1;
                }
                else
                {
                    d.Add(item, 1);
                }
            }

            // Add each key's frequency from collection B to the Dictionary
            // Return early if we detect a mismatch
            foreach (T item in b)
            {
                int c;
                if (d.TryGetValue(item, out c))
                {
                    if (c == 0)
                    {
                        return false;
                    }
                    else
                    {
                        d[item] = c - 1;
                    }
                }
                else
                {
                    // Not in dictionary
                    return false;
                }
            }

            // Verify that all frequencies are zero
            foreach (int v in d.Values)
            {
                if (v != 0)
                {
                    return false;
                }
            }

            // We know the collections are equal
            return true;
        }
    }
}
