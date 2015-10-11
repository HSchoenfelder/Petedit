using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// Provides methods for writing to a PNML document.
    /// </summary>
    public interface IPNMLWriter
    {
        /// <summary>
        /// Starts a new XML document and writes the opening tags for PNML.
        /// </summary>
        void StartXMLDocument();

        /// <summary>
        /// Adds a new place to the XML document. StartXMLDocument() must be calles before this
        /// method is called.
        /// </summary>
        /// <param name="id"> The id of the place to write. </param>
        /// <param name="name"> The name of the place to write. </param>
        /// <param name="xPos"> The x-coordinate of the place to write. </param>
        /// <param name="yPos"> The y-coordinate of the place to write. </param>
        /// <param name="tokens"> The amount of tokens on the place to write. </param>
        void AddPlace(String id, String name, String xPos, String yPos, String tokens);

        /// <summary>
        /// Adds a new transition to the XML document. StartXMLDocument() must be called before this
        /// method is called.
        /// </summary>
        /// <param name="id"> The id of the transition to write. </param>
        /// <param name="name"> The name of the transition to write. </param>
        /// <param name="xPos"> The x-coordinate of the transition to write. </param>
        /// <param name="yPos"> The y-coordinate of the transition to write. </param>
        void AddTransition(String id, String name, String xPos, String yPos);

        /// <summary>
        /// Adds a new arc to the XML document. StartXMLDocument() must be calles before this
        /// method is called.
        /// </summary>
        /// <param name="id"> The id of the arc to write. </param>
        /// <param name="source"> The id of the source of the arc to write. </param>
        /// <param name="target"> The id of the target of the arc to write. </param>
        void AddArc(String id, String source, String target);

        /// <summary>
        /// Finishes an XML document with petrinet data.
        /// </summary>
        void FinishXMLDocument();
    }
}
