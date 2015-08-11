using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using PetriNetModel;

namespace PetriNetEditor
{
    /// <summary>
    /// This class provides logic for a visual presentation of a Node in the petrinet.
    /// </summary>
    public class VisualNode : IVisualNode, INotifyPropertyChanged
    {
        #region fields
        /// <summary> Store for the ElementManager property. </summary>
        private ElementManager _elementManager;

        /// <summary> Store for the SelectionManager property. </summary>
        private SelectionManager _selectionManager;

        /// <summary> Store for the Model property. </summary>
        private IModel _model;

        /// <summary> Store for the Id property. </summary>
        private readonly String _id;

        /// <summary> Store for the XPos property. </summary>
        private double _xPos;

        /// <summary> Store for the YPos property. </summary>
        private double _yPos;

        /// <summary> Store for the XLeftEnd property. </summary>
        private double _xLeftEnd;

        /// <summary> Store for the YTopEnd property. </summary>
        private double _yTopEnd;

        /// <summary> Store for the DrawSize property. </summary>
        private int _drawSize;

        /// <summary> Store for the Selected property. </summary>
        private bool _selected;

        /// <summary> Store for the IsBeyondEdge property. </summary>
        private bool _isBeyondEdge;

        /// <summary> Store for the IsDrawTarget property. </summary>
        private bool _isDrawTarget;

        /// <summary> Store for the IsDragSource property. </summary>
        private bool _isDragSource;

        /// <summary >Store for the IsHightlighted property. </summary>
        private bool _isHighlighted = true;

        /// <summary> Store for the NodeType property. </summary>
        private NodeType _nodeType;

        /// <summary> Store for the TokenCount property. </summary>
        private int _tokenCount;

        /// <summary> Store for the TokenSize property. </summary>
        private double _tokenSize;

        /// <summary> Store for the Enabled property. </summary>
        private bool _enabled = true;
        #endregion

        #region events
        /// <summary> Occurs when a tracked property value changes. </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region properties

        #region private
        /// <summary> 
        /// Gets the selection manager that provides access to select functions. 
        /// </summary>
        private SelectionManager SelectionManager
        {
            get { return _selectionManager; }
        }

        /// <summary>
        /// Gets the Model that allows for manipulation of the petrinet through a VisualNode.
        /// </summary>
        private IModel Model
        {
            get { return _model; }
        }
        #endregion

        #region public
        /// <summary> 
        /// Gets the element manager that enables arc draw and move operations for nodes. 
        /// </summary>
        public ElementManager ElementManager
        {
            get { return _elementManager; }
        }

        /// <summary>
        /// Gets the Id that uniquely identifies this VisualNode.
        /// </summary>
        public String Id
        {
            get { return _id; }
        }

        /// <summary> Gets or sets the x-coordinate of the center of this VisualNode. </summary>
        public double XPos
        {
            get { return _xPos; }
            set 
            {
                _xPos = value;
                OnXPosChanged();
            }
        }

        /// <summary> Gets or sets the y-coordinate of the center of this VisualNode. </summary>
        public double YPos
        {
            get { return _yPos; }
            set 
            { 
                _yPos = value;
                OnYPosChanged();
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the rightmost point of this VisualNode.
        /// </summary>
        public double XLeftEnd
        {
            get { return _xLeftEnd; }
            set
            {
                _xLeftEnd = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the topmost point of this VisualNode.
        /// </summary>
        public double YTopEnd
        {
            get { return _yTopEnd; }
            set
            {
                _yTopEnd = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary> Gets or sets the draw size of this VisualNode. </summary>
        public int DrawSize
        {
            get { return _drawSize; }
            set
            {
                if (value != _drawSize)
                {
                    int prevSize = _drawSize;
                    _drawSize = value;
                    OnDrawSizeChanged();
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected state of this VisualNode.
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (value != _selected)
                {
                    _selected = value;
                    OnSelectedChanged(value);
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether this VisualNode is currently beyond
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

        /// <summary>
        /// Gets or sets a value which indicates whether this VisualNode is currently the target 
        /// of a draw operation.
        /// </summary>
        public bool IsDrawTarget
        {
            get { return _isDrawTarget; }
            set
            {
                if (value != _isDrawTarget)
                {
                    _isDrawTarget = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether this VisualNode is currently the source 
        /// of a drag operation.
        /// </summary>
        public bool IsDragSource
        {
            get { return _isDragSource; }
            set
            {
                if (value != _isDragSource)
                {
                    _isDragSource = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether this VisualNode is currently manually highlighted.
        /// </summary>
        public bool IsHighlighted
        {
            get { return _isHighlighted; }
            set
            {
                if (value != _isHighlighted)
                {
                    _isHighlighted = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary> Gets the type of this VisualNode. </summary>
        public NodeType NodeType
        {
            get { return _nodeType; }
        }

        /// <summary>
        /// Gets or sets the current amount of tokens on this VisualNode. The amount of tokens that a
        /// VisualNode can take is limited to 999.
        /// </summary>
        public int TokenCount
        {
            get { return _tokenCount; }
            set
            {
                if (value != _tokenCount)
                {
                    if (value < 0)
                        _tokenCount = 0;
                    else if (value > 999)
                        _tokenCount = 999;
                    else
                        _tokenCount = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the visual presentation of a single token on this VisualNode.
        /// </summary>
        public double TokenSize
        {
            get { return _tokenSize; }
            set
            {
                if (value != _tokenSize)
                {
                    _tokenSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether this VisualNode is currently enabled.
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set 
            {
                if (value != _enabled)
                {
                    _enabled = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the VisualNode class with the specified coordinates, id, draw size 
        /// and type, using the specified references. 
        /// </summary>
        /// <param name="xPos">The x-coordinate of the center of the new VisualNode.</param>
        /// <param name="yPos">The y-coordinate of the center of the new VisualNode.</param>
        /// <param name="id">The id of the new VisualNode.</param>
        /// <param name="drawSize">The draw size of the new VisualNode.</param>
        /// <param name="type">The NodeType of the new VisualNode.</param>
        /// <param name="selectionManager">A reference to the SelectionManager.</param>
        /// <param name="elementManager">A reference to the ElementManager.</param>
        /// <param name="model">A reference to the Model.</param>
        public VisualNode(double xPos, double yPos, String id, int drawSize, NodeType type, SelectionManager selectionManager, 
                          ElementManager elementManager, IModel model)
        {
            // initialize fields
            _selectionManager = selectionManager;
            _elementManager = elementManager;
            _model = model;
            if (xPos < 0)
                throw new ArgumentOutOfRangeException("xPos", xPos, "negative coordinate");
            if (yPos < 0)
                throw new ArgumentOutOfRangeException("yPos", yPos, "negative coordinate");
            _xPos = xPos;
            _yPos = yPos;
            _xLeftEnd = xPos - drawSize / 2;
            _yTopEnd = yPos - drawSize / 2;
            _tokenSize = drawSize / 5;
            _id = id;
            _drawSize = drawSize;
            _nodeType = type;
            _tokenCount = 0;
        }
        #endregion

        #region methods

        #region private
        /// <summary>
        /// Notifies the view that a property of this VisualNode has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Called when the DrawSize of this VisualNode changes. Recalculates the positioning properties
        /// based on the new value.
        /// </summary>
        private void OnDrawSizeChanged()
        {
            OnXPosChanged();
            OnYPosChanged();
            TokenSize = DrawSize / 5;
        }
               
        /// <summary>
        /// Called when the position of this VisualNode changes. Repositions all connected arcs.
        /// </summary>
        private void OnPosChanged()
        {
            foreach (String arcId in Model.GetArcsForNode(Id))
                ElementManager.RepositionArc(arcId, Id, XPos, YPos);
        }

        /// <summary>
        /// Called when the x-coordinate of the center of this VisualNode changes. Adjusts arc positioning
        /// and recalculates the leftmost point.
        /// </summary>
        private void OnXPosChanged()
        {
            OnPosChanged();
            XLeftEnd = RecalculateX(XPos);
        }

        /// <summary>
        /// Called when the y-coordinate of the center of this VisualNode changes. Adjusts arc positioning
        /// and recalculates the topmost point.
        /// </summary>
        private void OnYPosChanged()
        {
            OnPosChanged();
            YTopEnd = RecalculateY(YPos);
        }

        /// <summary>
        /// Called when the Selected state of this VisualNode changes. Changes selection states of connected
        /// arcs accordingly.
        /// </summary>
        /// <param name="selected">The new Selected state of this VisualNode.</param>
        private void OnSelectedChanged(bool selected)
        {
            if (selected)
            {
                foreach (String arcId in Model.GetArcsForNode(Id))
                    SelectionManager.AddAutoSelectedArc(arcId);
            }
            else
            {
                // if connected node is not selected either, turn auto-selection of arc off
                foreach (String arcId in Model.GetArcsForNode(Id))
                    SelectionManager.TryRemoveAutoSelectedArc(arcId, Model.GetConnectedNode(Id, arcId));
            }
        }

        /// <summary>
        /// Recalculates the new leftmost point. To be called after XPos property has changed.
        /// </summary>
        /// <param name="xPos">The x-coordinate of the center of this VisualNode from which to recalculate
        /// the leftmost point.</param>
        /// <returns>The x-coordinate of the leftmost point of this VisualNode.</returns>
        private double RecalculateX(double xPos)
        {
            return xPos - DrawSize / 2;
        }

        /// <summary>
        /// Recalculates the new topmost point. To be called after XPos property has changed.
        /// </summary>
        /// <param name="yPos">The y-coordinate of the center of this VisualNode from which to recalculate
        /// the topmost point.</param>
        /// <returns>The y-coordinate of the topmost point of this VisualNode.</returns>
        private double RecalculateY(double yPos)
        {
            return yPos - DrawSize / 2;
        }
        #endregion

        #region public
        /// <summary>
        /// Sets a new token value to this VisualNodes and writes it to the model.
        /// </summary>
        /// <param name="tokens">The new token value.</param>
        public void SetTokens(int tokens)
        {
            TokenCount = tokens;
            Model.ChangeTokens(Id, tokens);
        }

        /// <summary>
        /// Determines whether the specified point is contained within the visual presentation of this
        /// VisualNode.
        /// </summary>
        /// <param name="p">The point for which to perform the containment check.</param>
        /// <returns>true if the point is contained within the visual presentation of this VisualNode;
        /// otherwise false.</returns>
        public bool IsContained(Point p)
        {
            switch (NodeType)
            {
                case NodeType.Place:
                    if (Calculations.GetDistance(new Point(XPos, YPos), p) <= DrawSize / 2)
                        return true;
                    return false;
                case NodeType.Transition:
                    if (Math.Abs(p.X - XPos) <= DrawSize / 2 && Math.Abs(p.Y - YPos) <= DrawSize / 2)
                        return true;
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determines whether the center of the visual presentation of this VisualNode is contained 
        /// within the rectangle defined by the specified parameters.
        /// </summary>
        /// <param name="x">The x-coordinate of the top left point of the rectangle.</param>
        /// <param name="y">The y-coordinate of the top left point of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <returns>true if the visual presentation of the VisualNode is contained within the 
        /// rectangle; otherwise false.</returns>
        public bool IsContained(double x, double y, double width, double height)
        {
            if (XPos > x && XPos < x + width && YPos > y && YPos < y + height)
                return true;
            else
                return false;
        }
        #endregion

        #endregion
    }
}
