using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PetriNetEditor
{
    public class PNMLAccessorFactory
    {
        /// <summary> Store for the CustomParser property. </summary>
        private static IPNMLParser _customParser = null;

        /// <summary> Store for the CustomWriter property. </summary>
        private static IPNMLWriter _customWriter = null;

        /// <summary>
        /// Gets or sets a custom parser to be created.
        /// </summary>
        public static IPNMLParser CustomParser
        {
            private get { return _customParser; }
            set { _customParser = value; }
        }

        /// <summary>
        /// Gets or sets a custom writer to be created.
        /// </summary>
        public static IPNMLWriter CustomWriter
        {
            private get { return _customWriter; }
            set { _customWriter = value; }
        }

        /// <summary>
        /// Sets up a new PNMLParser for the provided file or returns the custom parser.
        /// </summary>
        /// <param name="filename">The name of the file to be parsed.</param>
        /// <param name="elementCreator">A reference to the ElementCreator.</param>
        /// <returns>A new PNMLParser or a custom parser, if one was provided.</returns>
        public IPNMLParser CreateParser(String filename, IElementCreator elementCreator)
        {
            if (CustomParser != null)
                return CustomParser;
            return new PNMLParser(filename, elementCreator);
        }

        /// <summary>
        /// Sets up a new PNMLWriter with the provided XMLWriter.
        /// </summary>
        /// <param name="xmlWriter">The XMLWriter to be used for writing.</param>
        /// <returns>A new PNMLWriter or a custom writer, if one was provided.</returns>
        public IPNMLWriter CreateWriter(XmlWriter xmlWriter)
        {
            if (CustomWriter != null)
                return CustomWriter;
            return new PNMLWriter(xmlWriter);
        }
    }
}
