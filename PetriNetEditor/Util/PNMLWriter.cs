using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PetriNetEditor
{
    /// <summary>
    /// This class implements a PNML Writer.
    /// </summary>
    public class PNMLWriter
    {
        /// <summary> Sets the length of a single indent. </summary>
        private const String Indent = "   ";

        /// <summary> Store for the IndentStack property. </summary>
        private int _indentStack;

        /// <summary> Store for the Writer property. </summary>
        private XmlWriter _writer;

        /// <summary> Gets or sets a value that controls the indentation level. </summary>
        private int IndentStack
        {
            get { return _indentStack; }
            set { _indentStack = value; }
        }

        /// <summary> Gets the underlying XmlWriter. </summary>
        private XmlWriter Writer
        {
            get { return _writer; }
            set { _writer = value; }
        }

        /// <summary>
        /// Sets up a new writer for the provided file. 
        /// </summary>
        /// <param name="writer"> The XmlWriter that is to be used to write the file. </param>
        public PNMLWriter(XmlWriter writer)
        {
            _writer = writer;
        }

        /// <summary>
        /// Starts a new XML document and writes the opening tags for PNML.
        /// </summary>
        public void StartXMLDocument()
        {
            Writer.WriteStartDocument();
            Writer.WriteWhitespace("\n");
            Writer.WriteStartElement("pnml");
            Writer.WriteString("\n");
            Writer.WriteWhitespace(Indent); ++IndentStack;
            Writer.WriteStartElement("net");
            Writer.WriteString("\n");
            ++IndentStack;
        }

        /// <summary>
        /// Adds a new transition to the XML document. StartXMLDocument() must be called before this
        /// method is called.
        /// </summary>
        /// <param name="id"> The id of the transition to write. </param>
        /// <param name="name"> The name of the transition to write. </param>
        /// <param name="xPos"> The x-coordinate of the transition to write. </param>
        /// <param name="yPos"> The y-coordinate of the transition to write. </param>
        public void AddTransition(String id, String name, String xPos, String yPos)
        {
            if (Writer != null)
            {
                Writer.WriteWhitespace(RepeatString(IndentStack));
                Writer.WriteStartElement("transition");
                Writer.WriteAttributeString("id", id);
                Writer.WriteWhitespace("\n");

                Writer.WriteWhitespace(RepeatString(++IndentStack));
                Writer.WriteStartElement("name");
                Writer.WriteWhitespace("\n");
                Writer.WriteWhitespace(RepeatString(++IndentStack));
                Writer.WriteStartElement("value");
                Writer.WriteString(name);
                Writer.WriteEndElement();
                Writer.WriteWhitespace("\n");
                Writer.WriteWhitespace(RepeatString(--IndentStack));
                Writer.WriteEndElement();
                Writer.WriteWhitespace("\n");

                Writer.WriteWhitespace(RepeatString(IndentStack));
                Writer.WriteStartElement("graphics");
                Writer.WriteWhitespace("\n");
                Writer.WriteWhitespace(RepeatString(++IndentStack));
                Writer.WriteStartElement("position");
                Writer.WriteAttributeString("x", xPos);
                Writer.WriteAttributeString("y", yPos);

                Writer.WriteEndElement();
                Writer.WriteWhitespace("\n");
                Writer.WriteWhitespace(RepeatString(--IndentStack));
                Writer.WriteEndElement();
                Writer.WriteWhitespace("\n");

                Writer.WriteWhitespace(RepeatString(--IndentStack));
                Writer.WriteEndElement();
                Writer.WriteWhitespace("\n");
            }
        }

        /// <summary>
        /// Adds a new place to the XML document. StartXMLDocument() must be calles before this
        /// method is called.
        /// </summary>
        /// <param name="id"> The id of the place to write. </param>
        /// <param name="name"> The name of the place to write. </param>
        /// <param name="xPos"> The x-coordinate of the place to write. </param>
        /// <param name="yPos"> The y-coordinate of the place to write. </param>
        /// <param name="tokens"> The amount of tokens on the place to write. </param>
        public void AddPlace(String id, String name, String xPos, String yPos, String tokens)
        {
            Writer.WriteWhitespace(RepeatString(IndentStack));
            Writer.WriteStartElement("place");
            Writer.WriteAttributeString("id", id);
            Writer.WriteWhitespace("\n");

            Writer.WriteWhitespace(RepeatString(++IndentStack));
            Writer.WriteStartElement("name");
            Writer.WriteWhitespace("\n");
            Writer.WriteWhitespace(RepeatString(++IndentStack));
            Writer.WriteStartElement("value");
            Writer.WriteString(name);
            Writer.WriteEndElement();
            Writer.WriteWhitespace("\n");
            Writer.WriteWhitespace(RepeatString(--IndentStack));
            Writer.WriteEndElement();
            Writer.WriteWhitespace("\n");

            Writer.WriteWhitespace(RepeatString(IndentStack));
            Writer.WriteStartElement("initialMarking");
            Writer.WriteWhitespace("\n");
            Writer.WriteWhitespace(RepeatString(++IndentStack));
            Writer.WriteStartElement("token");
            Writer.WriteWhitespace("\n");
            Writer.WriteWhitespace(RepeatString(++IndentStack));
            Writer.WriteStartElement("value");
            Writer.WriteString(tokens);
            Writer.WriteEndElement();
            Writer.WriteWhitespace("\n");
            Writer.WriteWhitespace(RepeatString(--IndentStack));
            Writer.WriteEndElement();
            Writer.WriteWhitespace("\n");
            Writer.WriteWhitespace(RepeatString(--IndentStack));
            Writer.WriteEndElement();
            Writer.WriteWhitespace("\n");

            Writer.WriteWhitespace(RepeatString(IndentStack));
            Writer.WriteStartElement("graphics");
            Writer.WriteWhitespace("\n");
            Writer.WriteWhitespace(RepeatString(++IndentStack));
            Writer.WriteStartElement("position");
            Writer.WriteAttributeString("x", xPos);
            Writer.WriteAttributeString("y", yPos);
            Writer.WriteEndElement();
            Writer.WriteWhitespace("\n");
            Writer.WriteWhitespace(RepeatString(--IndentStack));
            Writer.WriteEndElement();
            Writer.WriteWhitespace("\n");

            Writer.WriteWhitespace(RepeatString(--IndentStack));
            Writer.WriteEndElement();
            Writer.WriteWhitespace("\n");
        }

        /// <summary>
        /// Adds a new arc to the XML document. StartXMLDocument() must be calles before this
        /// method is called.
        /// </summary>
        /// <param name="id"> The id of the arc to write. </param>
        /// <param name="source"> The id of the source of the arc to write. </param>
        /// <param name="target"> The id of the target of the arc to write. </param>
        public void AddArc(String id, String source, String target)
        {
            if (Writer != null)
            {
                Writer.WriteWhitespace(RepeatString(IndentStack));
                Writer.WriteStartElement("arc");
                Writer.WriteAttributeString("id", id);
                Writer.WriteAttributeString("source", source);
                Writer.WriteAttributeString("target", target);
                Writer.WriteEndElement();
                Writer.WriteWhitespace("\n");
            }
        }

        /// <summary>
        /// Finishes an XML document with petrinet data.
        /// </summary>
        public void FinishXMLDocument()
        {
            if (Writer != null)
            {
                Writer.WriteWhitespace(RepeatString(--IndentStack));
                Writer.WriteEndElement();
                Writer.WriteWhitespace("\n");
                Writer.WriteWhitespace(RepeatString(--IndentStack));
                Writer.WriteEndElement();
                Writer.WriteWhitespace("\n");
                Writer.WriteEndDocument();
                Writer.Close();
            }
        }

        /// <summary>
        /// Generates a string that provides whitespace as specified by the amount of repetitions.
        /// </summary>
        /// <param name="repetitions"> Specifies the amount of whitespace to generate. </param>
        /// <returns> A string that provides the specified amount of whitespace. </returns>
        private String RepeatString(int repetitions)
        {
            StringBuilder spaces = new StringBuilder();
            for (int i = 0; i < IndentStack; i++)
            {
                spaces.Append(Indent);
            }
            return spaces.ToString();
        }
    }
}
