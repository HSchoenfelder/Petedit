using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace PetriNetEditor
{
    /// <summary>
    /// This class implements a PNML Parser.
    /// </summary>
    public class PNMLParser : IPNMLParser
    {
        /// <summary> Store for the ElementCreator property. </summary>
        private IElementCreator _elementCreator;

        /// <summary> Store for the Filename property. </summary>
        private String _filename;

        /// <summary> Store for the Reader property. </summary>
        private XmlReader _reader;

        /// <summary> Store for the Ids property. </summary>
        private IList<String> _ids;

        /// <summary> Store for the Id property. </summary>
        private String _id;

        /// <summary> Store for the Name property. </summary>
        private String _name;

        /// <summary> Store for the Tokens property. </summary>
        private String _tokens;

        /// <summary> Store for the IsToken property. </summary>
        private bool _isToken;

        /// <summary> Store for the IsName property. </summary>
        private bool _isName;

        /// <summary> Store for the IsValue property. </summary>
        private bool _isValue;

        /// <summary> Store for the IsTransition property. </summary>
        private bool _isTransition;

        /// <summary> Store for the IsPlace property. </summary>
        private bool _isPlace;

        /// <summary> Gets the ElementCreator that creates the elements read from the PNML file. </summary>
        private IElementCreator ElementCreator
        {
            get { return _elementCreator; }
        }

        /// <summary> Gets the name of the file to parse. </summary>
        private String Filename
        {
            get { return _filename; }
        }

        /// <summary> Gets or sets the underlying XmlReader. </summary>
        private XmlReader Reader
        {
            get { return _reader; }
            set { _reader = value; }
        }

        /// <summary> 
        /// Gets a list that holds the ids of all previously read elements.
        /// </summary>
        private IList<String> Ids
        {
            get { return _ids; }
        }

        /// <summary> 
        /// Gets or sets the id of the current element.
        /// </summary>
        private String Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary> 
        /// Gets or sets the name of the current element.
        /// </summary>
        private String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary> 
        /// Gets or sets the token count of the current element.
        /// </summary>
        private String Tokens
        {
            get { return _tokens; }
            set { _tokens = value; }
        }

        /// <summary> 
        /// Gets or sets a value which indicates whether the current node is a token node.
        /// </summary>
        private bool IsToken
        {
            get { return _isToken; }
            set { _isToken = value; }
        }

        /// <summary> 
        /// Gets or sets a value which indicates whether the current node is a name node.
        /// </summary>
        private bool IsName
        {
            get { return _isName; }
            set { _isName = value; }
        }

        /// <summary> 
        /// Gets or sets a value which indicates whether the current node is a value node.
        /// </summary>
        private bool IsValue
        {
            get { return _isValue; }
            set { _isValue = value; }
        }

        /// <summary> 
        /// Gets or sets a value which indicates whether the current node is a transition node.
        /// </summary>
        private bool IsTransition
        {
            get { return _isTransition; }
            set { _isTransition = value; }
        }

        /// <summary> 
        /// Gets or sets a value which indicates whether the current node is a place node.
        /// </summary>
        private bool IsPlace
        {
            get { return _isPlace; }
            set { _isPlace = value; }
        }

        /// <summary>
        /// Sets up a new parser for the provided file. 
        /// </summary>
        /// <param name="filename"> The filename of the file to parse. </param>
        public PNMLParser(String filename, IElementCreator elementCreator)
        {
            _elementCreator = elementCreator;
            _filename = filename;
            _ids = new List<String>();
        }

        /// <summary>
        /// Reads the XML file and delegates the extracted XML elements to the respective
        /// methods.
        /// </summary>
        public void Parse()
        {
            using (StreamReader sr = File.OpenText(Filename))
            {
                // initialize
                Reader = XmlReader.Create(sr);
                Reader.MoveToContent();
                if (!Reader.Name.ToLower().Equals("pnml"))
                    throw new InvalidPNMLException(Path.GetFileName(Filename));
                // parse
                while (Reader.Read())
                {
                    switch (Reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            HandleStartElement(Reader.Name);
                            break;
                        case XmlNodeType.EndElement:
                            HandleEndElement(Reader.Name);
                            break;
                        case XmlNodeType.Text:
                            if (IsValue && Id != null)
                                HandleValue(Reader.Value);
                            break;
                        case XmlNodeType.Whitespace:
                            break;
                        default:
                            Reader.Close();
                            break;
                    }
                } 
            }
        }

        /// <summary>
        /// Handles the start of a new XML element by checking the name of the element
        /// and delegating to the corresponding method.
        /// </summary>
        /// <param name="element"> The name of the start element. </param>
        private void HandleStartElement(String element)
        {
            String name = element.ToLower();
            if (name.Equals("transition"))
                HandleTransition();
            else if (name.Equals("place"))
                HandlePlace();
            else if (name.Equals("arc"))
                HandleArc();
            else if (name.Equals("name"))
                IsName = true;
            else if (name.Equals("position"))
                HandlePosition();
            else if (name.Equals("token"))
                IsToken = true;
            else if (name.Equals("value"))
                IsValue = true;
        }

        /// <summary>
        /// Handles the end of an XML element by checking the name of the element and
        /// delegating to the corresponding method.
        /// </summary>
        /// <param name="element"> The name of the end element. </param>
        private void HandleEndElement(String element)
        {
            String name = element.ToLower();
            if (name.Equals("token"))
                IsToken = false;
            else if (name.Equals("name"))
                IsName = false;
            else if (name.Equals("value"))
                IsValue = false;
            else if (name.Equals("transition"))
                FinalizeNode();
            else if (name.Equals("place"))
                FinalizeNode();
        }

        /// <summary>
        /// Handles the text inside of a value element.
        /// </summary>
        /// <param name="value"> The text inside the value element. </param>
        private void HandleValue(String value)
        {
            if (IsName)
                Name = value;
            else if (IsToken)
                Tokens = value;
        }

        /// <summary>
        /// Handles a position element.
        /// </summary>
        private void HandlePosition()
        {
            String x = null;
            String y = null;
            if (Reader.HasAttributes)
            {
                while (Reader.MoveToNextAttribute())
                {
                    if (Reader.Name.ToLower().Equals("x"))
                        x = Reader.Value;
                    if (Reader.Name.ToLower().Equals("y"))
                        y = Reader.Value;
                }
                Reader.MoveToElement();
            }
            if (x != null && y != null && Id != null)
            {
                if (IsTransition)
                    NewTransition(Id, x, y);
                if (IsPlace)
                    NewPlace(Id, x, y);
            }
        }

        /// <summary>
        /// Handles a transition element.
        /// </summary>
        private void HandleTransition()
        {
            String transitionId = null;
            if (Reader.HasAttributes)
            {
                while (Reader.MoveToNextAttribute())
                {
                    if (Reader.Name.ToLower().Equals("id"))
                    {
                        transitionId = Reader.Value;
                        if (Ids.Contains(transitionId))
                            throw new DuplicateIdException();
                        Ids.Add(transitionId);
                    }
                }
                Reader.MoveToElement();
            }
            if (transitionId != null)
            {
                IsTransition = true;
                Id = transitionId;
            }
            else
            {
                IsTransition = false;
                Id = null;
            }
        }

        /// <summary>
        /// Handles a place element.
        /// </summary>
        private void HandlePlace()
        {
            String placeId = null;
            if (Reader.HasAttributes)
            {
                while (Reader.MoveToNextAttribute())
                {
                    if (Reader.Name.ToLower().Equals("id"))
                    {
                        placeId = Reader.Value;
                        if (Ids.Contains(placeId))
                            throw new DuplicateIdException();
                        Ids.Add(placeId);
                    }
                }
                Reader.MoveToElement();
            }
            if (placeId != null)
            {
                IsPlace = true;
                Id = placeId;
            }
            else
            {
                IsPlace = false;
                Id = null;
            }
        }

        /// <summary>
        /// Handles an arc element.
        /// </summary>
        private void HandleArc()
        {
            String arcId = null;
            String source = null;
            String target = null;
            if (Reader.HasAttributes)
            {
                while (Reader.MoveToNextAttribute())
                {
                    if (Reader.Name.ToLower().Equals("id"))
                    {
                        arcId = Reader.Value;
                        if (Ids.Contains(arcId))
                            throw new DuplicateIdException();
                        Ids.Add(arcId);
                    }
                    else if (Reader.Name.ToLower().Equals("source"))
                        source = Reader.Value;
                    else if (Reader.Name.ToLower().Equals("target"))
                        target = Reader.Value;
                }
                Reader.MoveToElement();
            }
            if (arcId != null && source != null && target != null)
                NewArc(arcId, source, target);
            // arc id does not need to be remembered
            Id = null;
        }

        /// <summary>
        /// Finalizes a node element.
        /// </summary>
        private void FinalizeNode()
        {
            if (Name != null)
                SetName(Id, Name);
            if (Tokens != null)
                SetTokens(Id, Tokens);
            Name = null;
            Tokens = null;
            IsTransition = false;
            IsPlace = false;
        }

        /// <summary>
        /// Closes the XML Parser. Should be used to properly close the parser after an
        /// error that has been handled outside of the parser.
        /// </summary>
        public void CloseParser()
        {
            if (Reader != null)
                Reader.Close();
        }

        /// <summary>
        /// Creates a loaded Transition.
        /// </summary>
        /// <param name="id"> The id of the Transition. </param>
        /// <param name="xCoord"> The x-coordinate of the Transition. </param>
        /// <param name="yCoord"> The y-coordinate of the Transition. </param>
        private void NewTransition(String id, String xCoord, String yCoord)
        {
            ElementCreator.CreateTrans(new Point(Int32.Parse(xCoord), Int32.Parse(yCoord)), id);
        }

        /// <summary>
        /// Creates a loaded place.
        /// </summary>
        /// <param name="id"> The id of the Place. </param>
        /// <param name="xCoord"> The x-coordinate of the Place. </param>
        /// <param name="yCoord"> The y-coordinate of the Place. </param>
        private void NewPlace(String id, String xCoord, String yCoord)
        {
            ElementCreator.CreatePlace(new Point(Int32.Parse(xCoord), Int32.Parse(yCoord)), id);
        }

        /// <summary>
        /// Creates a loaded arc.
        /// </summary>
        /// <param name="id"> The id of the Arc. </param>
        /// <param name="source"> The source id of the Arc. </param>
        /// <param name="target"> The target id of the Arc. </param>
        private void NewArc(String id, String source, String target)
        {
            ElementCreator.CreateArc(id, source, target, false);
        }

        /// <summary>
        /// Sets the name of a loaded node.
        /// </summary>
        /// <param name="id"> The id of the node. </param>
        /// <param name="name"> The name of the node. </param>
        private void SetName(String id, String name)
        {
            ElementCreator.SetNodeName(id, name);
        }

        /// <summary>
        /// Sets the token count of a loaded place.
        /// </summary>
        /// <param name="id"> The id of the place. </param>
        /// <param name="tokens"> The token count of the place. </param>
        private void SetTokens(String id, String tokens)
        {
            ElementCreator.SetPlaceTokens(id, Int32.Parse(tokens));
        }
    }
}
