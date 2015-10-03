using PetriNetModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetEditor
{
    /// <summary>
    /// This class provides logic for a visual presentation of the Name of a Node 
    /// in the petrinet.
    /// </summary>
    public class NameField : INotifyPropertyChanged, INameField
    {
        #region fields
        /// <summary> Store for the ElementManager property. </summary>
        private IElementManager _elementManager;

        /// <summary> Store for the Model property. </summary>
        private IModel _model;

        /// <summary> Store for the ParentNode property. </summary>
        private IVisualNode _parentNode;

        /// <summary>Store for the Id property.</summary>
        private readonly String _id;

        /// <summary>Store for the Name property.</summary>
        private String _name;

        /// <summary> Store for the XPos property. </summary>
        private double _xPos;

        /// <summary> Store for the YPos property. </summary>
        private double _yPos;

        /// <summary> Store for the Width property. </summary>
        private double _width;

        /// <summary> Store for the Height property. </summary>
        private double _height;

        /// <summary> Store for the DrawSize property. </summary>
        private int _drawSize;

        /// <summary> Store for the TextFieldActive property. </summary>
        private bool _textFieldActive;

        /// <summary> Store for the NameFieldVisible property. </summary>
        private bool _nameFieldVisible;

        /// <summary> Store for the IsBeyondEdge property. </summary>
        private bool _isBeyondEdge;
        #endregion

        #region events
        /// <summary>Occurs when a tracked property value changes.</summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region properties
        /// <summary> Gets the Model that allows for manipulation of the petrinet through the NameField. </summary>
        private IModel Model
        {
            get { return _model; }
        }
        
        /// <summary> Gets the element manager that enables arc draw and move operations for nodes. </summary>
        public IElementManager ElementManager
        {
            get { return _elementManager; }
        }

        /// <summary> Gets the VisualNode this NameField is attached to. </summary>
        public IVisualNode ParentNode
        {
            get { return _parentNode; }
        }

        /// <summary> Gets the Id that uniquely identifies this NameField. </summary>
        public String Id
        {
            get { return _id; }
        }

        /// <summary> Gets or sets the Name of this NameField. </summary>
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary> Gets or sets the x-coordinate of this NameField. </summary>
        public double XPos
        {
            get { return _xPos; }
            set
            {

                _xPos = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary> Gets or sets the y-coordinate of this NameField.</summary>
        public double YPos
        {
            get { return _yPos; }
            set
            {
                _yPos = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary> Gets or sets the Width of this Namefield. </summary>
        public double Width
        {
            get
            {
                if (NameFieldVisible)
                    return _width;
                else
                    return 0;
            }
            set
            {
                _width = value;
                OnWidthChanged();
                ElementManager.AdjustViewSize(Id, UndoRedoOps.None);
            }
        }

        /// <summary> Gets or sets the Height of this Namefield. </summary>
        public double Height
        {
            get
            {
                if (NameFieldVisible)
                    return _height;
                else
                    return 0;
            }
            set
            {
                _height = value;
                OnHeightChanged();
                ElementManager.AdjustViewSize(Id, UndoRedoOps.None);
            }
        }

        /// <summary> Gets or sets the draw size of the associated node. </summary>
        public int DrawSize
        {
            get { return _drawSize; }
            set
            {
                if (value != _drawSize)
                {
                    _drawSize = value;
                    AdjustNameField();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether the NameField currently has focus.
        /// </summary>
        public bool TextFieldActive
        {
            get { return _textFieldActive; }
            set
            {
                if (value != _textFieldActive)
                {
                    _textFieldActive = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether the NameField is currently visible.
        /// </summary>
        public bool NameFieldVisible
        {
            get { return _nameFieldVisible; }
            set
            {
                if (value != _nameFieldVisible)
                {
                    _nameFieldVisible = value;
                    OnNameFieldVisibleChanged(value);
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether this NameField is currently beyond
        /// the bounds of the visual presentation and requires a scroll operation.
        /// </summary>
        public bool IsBeyondEdge
        {
            get { return _isBeyondEdge; }
            set
            {
                if (value != _isBeyondEdge)
                {
                    _isBeyondEdge = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the NameField class with the specified id, parent node and
        /// references. 
        /// </summary>
        /// <param name="id">The id of the new NameField.</param>
        /// <param name="elementManager">A reference to the ElementManager.</param>
        /// <param name="model">A reference to the Model.</param>
        /// <param name="parentNode">A reference to the parent node of the NameField.</param>
        public NameField(String id, IElementManager elementManager, IModel model, IVisualNode parentNode)
        {
            _id = id;
            _elementManager = elementManager;
            _model = model;
            _parentNode = parentNode;
        }
        #endregion

        #region methods

        #region private
        /// <summary>
        /// Notifies the view that a property of this NameField has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); 
        }

        /// <summary>
        /// Called when the width of the NameField changes. Updates the horizontal position.
        /// </summary>
        private void OnWidthChanged()
        {
            if (Width <= DrawSize)
                XPos = ParentNode.XLeftEnd; 
            else
                XPos = ParentNode.XPos - Width / 2;
        }

        /// <summary>
        /// Called when the height of the NameField changes. Updates the vertical position.
        /// </summary>
        private void OnHeightChanged()
        {
            YPos = ParentNode.YTopEnd - Height;
        }

        /// <summary>
        /// Calles when the visible state of the NameField changes. Updates the horizontal
        /// and vertical position.
        /// </summary>
        /// <param name="visible"></param>
        private void OnNameFieldVisibleChanged(bool visible)
        {
            if (visible)
	        {
		        XPos = ParentNode.XPos - Width / 2;
                YPos = ParentNode.YTopEnd - Height; 
	        }
        }
        #endregion

        #region public
        /// <summary>
        /// Adjust the position of the NameField after the position of its parent node has
        /// changed.
        /// </summary>
        public void AdjustNameField()
        {
            if (Width <= DrawSize)
                XPos = ParentNode.XLeftEnd;
            else
                XPos = ParentNode.XPos - Width / 2;
            YPos = ParentNode.YTopEnd - Height; 
        }

        /// <summary>
        /// Sets a new name to this NameField and writes it to the model.
        /// </summary>
        /// <param name="name">The new name for the NameField.</param>
        public void SetName(String name)
        {
            if (name != null && !name.Equals(""))
                NameFieldVisible = true;
            Name = name;
            Model.ChangeName(Id, name);
        }
        #endregion

        #endregion
    }
}
