using PetriNetModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PetriNetEditor
{
    /// <summary>
    /// This class provides logic for a visual presentation of an Arc in the petrinet.
    /// </summary>
    public class VisualArc : IDrawingArc, IVisualArc, INotifyPropertyChanged, ICloneable
    {
        #region fields
        /// <summary> Store for the Model property. </summary>
        private IModel _model;

        /// <summary> Store for the ElementManager property. </summary>
        private ElementManager _elementManager;

        /// <summary> Store for the Id property. </summary>
        private String _id;

        /// <summary> Store for the XPos property. </summary>
        private double _xPos;

        /// <summary> Store for the YPos property. </summary>
        private double _yPos;

        /// <summary> Store for the Width property. </summary>
        private double _width;

        /// <summary> Store for the Height property. </summary>
        private double _height;

        /// <summary> Store for the HeadPoints property. </summary>
        private PointCollection _headPoints;

        /// <summary> Store for the Rotation property. </summary>
        private RotateTransform _rotation;

        /// <summary> Store for the Brother property. </summary>
        private String _brotherId;

        /// <summary> Store for the SourceType property. </summary>
        private NodeType _sourceType;

        /// <summary> Store for the Valid property. </summary>
        private bool _valid;

        /// <summary> Store for the Visible property. </summary>
        private bool _visible;

        /// <summary> Store for the DrawSize property. </summary>
        private int _drawSize;

        /// <summary> Store for the ArrowheadSize property. </summary>
        private int _arrowheadSize;

        /// <summary> Store for the Selected property. </summary>
        private bool _selected;

        /// <summary> Store for the AutoSelected property. </summary>
        private bool _autoselected;

        /// <summary> Store for the IsBeyondEdge property. </summary>
        private bool _isBeyondEdge;

        /// <summary> Store for the SourceNodePos property. </summary>
        private NPoint _sourceNodePos;

        /// <summary> Store for the TargetNodePos property. </summary>
        private NPoint _targetNodePos;

        /// <summary> Store for the ActualSource property. </summary>
        private NPoint _actualSource;

        /// <summary> Store for the ActualTarget property. </summary>
        private NPoint _actualTarget;
        #endregion

        #region events
        /// <summary> Occurs when a tracked property value changes .</summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region properties

        #region private
        /// <summary> Gets the Model that allows for manipulation of the petrinet. </summary>
        private IModel Model
        {
            get { return _model; }
        }
        #endregion

        #region public
        /// <summary>
        /// Gets the ElementManager that provides functionality for managing visual nodes and arcs.
        /// </summary>
        public ElementManager ElementManager
        {
            get { return _elementManager; }
        }

        /// <summary> Gets the Id that uniquely identifies this VisualArc. </summary>
        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of this VisualArc in graphical presentation.
        /// </summary>
        public double XPos
        {
            get { return _xPos; }
            set
            {
                if (value != _xPos)
                {
                    _xPos = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of this VisualArc in graphical presentation.
        /// </summary>
        public double YPos
        {
            get { return _yPos; }
            set
            {
                if (value != _yPos)
                {
                    _yPos = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Width of this VisualArc in graphical presentation.
        /// </summary>
        public double Width
        {
            get { return _width; }
            set
            {
                if (value != _width)
                {
                    _width = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Height of this VisualArc in graphical presentation.
        /// </summary>
        public double Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary> 
        /// Gets or sets the collection of points that constitute the arrowhead in the graphical presentation.
        /// </summary>
        public PointCollection HeadPoints
        {
            get { return _headPoints; }
            set
            {
                if (value != _headPoints)
                {
                    _headPoints = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the rotation of this VisualArc in the graphical presentation.
        /// </summary>
        public RotateTransform Rotation
        {
            get { return _rotation; }
            set
            {
                if (value != _rotation)
                {
                    _rotation = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the id of the brother of this VisualArc in case of a double arc between two nodes.
        /// </summary>
        public String BrotherId
        {
            get { return _brotherId; }
            set
            {
                if (value != _brotherId)
                {
                    _brotherId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary> Gets or sets the type of the source node of this VisualArc. </summary>
        public NodeType SourceType
        {
            get { return _sourceType; }
            set { _sourceType = value; }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether this VisualArc is valid and ready for display.
        /// </summary>
        public bool IsValid
        {
            get { return _valid; }
            set { _valid = value; }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether this VisualArc is currently visible in the
        /// graphical presentation.
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (value != _visible)
                {
                    _visible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary> Gets or sets the draw size of this VisualArc. </summary>
        public int DrawSize
        {
            get { return _drawSize; }
            set
            {
                if (value != _drawSize)
                {
                    _drawSize = value;
                    OnSourceNodePosChanged(SourceNodePos);
                    OnTargetNodePosChanged(TargetNodePos);
                }
            }
        }

        /// <summary> Gets or sets the size of the arrowhead of this VisualArc. </summary>
        public int ArrowheadSize
        {
            get { return _arrowheadSize; }
            set
            {
                if (value != _arrowheadSize)
                {
                    _arrowheadSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether this VisualArc is currently selected in 
        /// the graphical presentation.
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (value != _selected)
                {
                    _selected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether this VisualArc is currently selected in
        /// the graphical presentation because an adjacent node is selected.
        /// </summary>
        public bool AutoSelected
        {
            get { return _autoselected; }
            set
            {
                if (value != _autoselected)
                {
                    _autoselected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether this VisualArc is currently beyond
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
        /// Gets or sets the coordinates of the center of the source of this VisualArc.
        /// </summary>
        public NPoint SourceNodePos
        {
            get { return _sourceNodePos; }
            set
            {
                if (value != _sourceNodePos)
                {
                    _sourceNodePos = value;
                    OnSourceNodePosChanged(value);
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the coordinates of the center of the target of this VisualArc.
        /// </summary>
        public NPoint TargetNodePos
        {
            get { return _targetNodePos; }
            set
            {
                if (value != _targetNodePos)
                {
                    _targetNodePos = value;
                    OnTargetNodePosChanged(value);
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the coordinates of the actual starting point of this VisualArc.
        /// </summary>
        public NPoint ActualSource 
        {
            get { return _actualSource; }
            set
            {
                if (value != _actualSource)
                {
                    _actualSource = value;
                    OnActualSourceChanged(value);
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the coordinates of the actual end point of this VisualArc.
        /// </summary>
        public NPoint ActualTarget 
        {
            get { return _actualTarget; }
            set
            {
                if (value != _actualTarget)
                {
                    _actualTarget = value;
                    OnActualTargetChanged(value);
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the VisualArc class with the specified draw size and arrowhead size,
        /// using the specified ElementManager. 
        /// </summary>
        /// <param name="drawsize">The size at which the VisualArc is drawn.</param>
        /// <param name="arrowheadSize">The size at which the arrowhead of the VisualArc is drawn.</param>
        /// <param name="elementManager">A reference to the ElementManager.</param>
        /// <param name="model">A reference to the model of the petrinet.</param>
        public VisualArc(int drawsize, int arrowheadSize, ElementManager elementManager, IModel model)
        {
            _model = model;
            _drawSize = drawsize;
            _arrowheadSize = arrowheadSize;
            _elementManager = elementManager;
        }
        #endregion

        #region methods

        #region private
        /// <summary>
        /// Notifies the view that a property of this VisualArc has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Called when a change of SourceNodePos occurs. Recalculates ActualSource and ActualTarget based
        /// on the new value.
        /// </summary>
        /// <param name="newSourceNodePos">The new SourceNodePos.</param>
        private void OnSourceNodePosChanged(NPoint newSourceNodePos)
        {
            if (IsValid)
            {
                switch (_sourceType)
                {
                    case NodeType.Place:
                        ActualSource = CalculatePlaceIntersection(newSourceNodePos, TargetNodePos);
                        ActualTarget = CalculateTransIntersection(TargetNodePos, newSourceNodePos);
                        break;
                    case NodeType.Transition:
                        ActualSource = CalculateTransIntersection(newSourceNodePos, TargetNodePos);
                        ActualTarget = CalculatePlaceIntersection(TargetNodePos, newSourceNodePos);
                        break;
                    default:
                        break;
                }
                // calculate minimum distance between node presentations for the VisualArc to remain visible 
                double minimumDistance = Calculations.GetDistance(newSourceNodePos, ActualSource) +
                                                    Calculations.GetDistance(TargetNodePos, ActualTarget);
                double distance = Calculations.GetDistance(newSourceNodePos, TargetNodePos);
                if (distance < minimumDistance)
                    Visible = false;
                else
                    Visible = true; 
            }
        }

        /// <summary>
        /// Called when a change of TargetNodePos occurs. Recalculates ActualTarget and ActualSource based
        /// on the new value.
        /// </summary>
        /// <param name="newTargetNodePos">The new TargetNodePos.</param>
        private void OnTargetNodePosChanged(NPoint newTargetNodePos)
        {
            if (IsValid)
            {
                switch (_sourceType)
                {
                    case NodeType.Place:
                        ActualTarget = CalculateTransIntersection(newTargetNodePos, SourceNodePos);
                        ActualSource = CalculatePlaceIntersection(SourceNodePos, newTargetNodePos);
                        break;
                    case NodeType.Transition:
                        ActualTarget = CalculatePlaceIntersection(newTargetNodePos, SourceNodePos);
                        ActualSource = CalculateTransIntersection(SourceNodePos, newTargetNodePos);
                        break;
                    default:
                        break;
                }
                // calculate minimum distance between node presentations for the VisualArc to remain visible 
                double minimumDistance = Calculations.GetDistance(SourceNodePos, ActualSource) +
                                                    Calculations.GetDistance(newTargetNodePos, ActualTarget);
                double distance = Calculations.GetDistance(SourceNodePos, newTargetNodePos);
                if (distance < minimumDistance)
                    Visible = false;
                else
                    Visible = true; 
            }
        }

        /// <summary>
        /// Called when a change of ActualSource occurs. Recalculates ActualTarget based on the new value.
        /// </summary>
        /// <param name="newActualSource">The new ActualSource.</param>
        private void OnActualSourceChanged(NPoint newActualSource)
        {
            // only perform recalculation if no source has been set previously
            if (SourceNodePos == null)
            {
                switch (_sourceType)
                {
                    case NodeType.Place:
                        ActualTarget = CalculateTransIntersection(TargetNodePos, newActualSource);
                        break;
                    case NodeType.Transition:
                        ActualTarget = CalculatePlaceIntersection(TargetNodePos, newActualSource);
                        break;
                    default:
                        break;
                }
            }
            AdjustPositioning();
        }

        /// <summary>
        /// Called when a change of ActualTarget occurs. Recalculates ActualSource based on the new value.
        /// </summary>
        /// <param name="newActualTarget">The new ActualTarget.</param>
        private void OnActualTargetChanged(NPoint newActualTarget)
        {
            // only perform recalculation if no target has been set previously
            if (TargetNodePos == null)
            {
                switch (_sourceType)
                {
                    case NodeType.Place:
                        ActualSource = CalculatePlaceIntersection(SourceNodePos, newActualTarget);
                        break;
                    case NodeType.Transition:
                        ActualSource = CalculateTransIntersection(SourceNodePos, newActualTarget);
                        break;
                    default:
                        break;
                }
            }
            AdjustPositioning();
        }

        /// <summary>
        /// Calculates the intersection of the line defined by the two specified points with a
        /// circle that has its center in p1 and whose diameter is given by the current value
        /// of DrawSize.
        /// </summary>
        /// <param name="p1">The starting point of the line and center of the circle.</param>
        /// <param name="p2">The end point of the line.</param>
        /// <returns>The point at which the circle intersects with the line.</returns>
        private Point CalculatePlaceIntersection(Point p1, Point p2)
        {
            double xDiff = p2.X - p1.X;
            double yDiff = p2.Y - p1.Y;
            double angle = Math.Atan2(yDiff, xDiff);
            double xResult = p1.X + DrawSize / 2.0 * Math.Cos(angle);
            double yResult = p1.Y + DrawSize / 2.0 * Math.Sin(angle);
            return new Point(xResult, yResult);
        }

        /// <summary>
        /// Calculates the intersection of the line defined by the two specified points with a
        /// square that has its center in p1 and whose width and height is given by the current 
        /// value of DrawSize.
        /// </summary>
        /// <param name="p1">The starting point of the line and center of the square.</param>
        /// <param name="p2">The end point of the line.</param>
        /// <returns>The point at which the square intersects with the line.</returns>
        private Point CalculateTransIntersection(Point p1, Point p2)
        {
            double radius = DrawSize / 2.0;
            double u = Math.Max(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y));
            double x = (p2.X - p1.X) / u * radius;
            double y = (p2.Y - p1.Y) / u * radius;
            return new Point(p1.X + x, p1.Y + y);            
        }

        /// <summary>
        /// Readjusts all positioning properties after ActualSource or ActualTarget have been
        /// recalculated.
        /// </summary>
        private void AdjustPositioning()
        {
            XPos = ActualSource.X;
            YPos = ActualSource.Y - ArrowheadSize - 1;
            Width = Calculations.GetDistance(ActualSource, ActualTarget);
            Height = ArrowheadSize * 2 + 1;
            Rotation = new RotateTransform(Calculations.GetAngle(ActualSource, ActualTarget), 0, ArrowheadSize + 1);
            if (Width >= ArrowheadSize * 4 || BrotherId == null)
                HeadPoints = new PointCollection { new Point(Width, ArrowheadSize),
                                                   new Point(Width - ArrowheadSize * 2, 0),
                                                   new Point(Width - ArrowheadSize * 2, ArrowheadSize * 2 + 1)};
            else
                HeadPoints = new PointCollection { new Point(Width, ArrowheadSize),
                                                   new Point(Width / 2, 0),
                                                   new Point(Width / 2, ArrowheadSize * 2 + 2)};
        }

        
        #endregion

        #region public
        /// <summary>
        /// Creates a new VisualArc that is a deep copy of the current VisualArc.
        /// </summary>
        /// <returns>A new object that is a copy of this VisualArc.</returns>
        public object Clone()
        {
            VisualArc newArc = (VisualArc)this.MemberwiseClone();
            newArc.HeadPoints = new PointCollection(HeadPoints);
            newArc.Rotation = new RotateTransform(Calculations.GetAngle(ActualSource, ActualTarget), 0, ArrowheadSize + 1);
            
            return newArc;
        }

        /// <summary>
        /// Positions the visual presentation of the arc in the petrinet after is has been created.
        /// </summary>
        /// <param name="sourceNodePos">The coordinates of the center of the source node of the arc.</param>
        /// <param name="targetNodePos">The coordinates of the center of the target node of the arc.</param>
        public void PositionArc(Point sourceNodePos, Point targetNodePos)
        {
            TargetNodePos = targetNodePos;
            switch (SourceType)
            {
                case NodeType.Place:
                    ActualSource = CalculatePlaceIntersection(sourceNodePos, targetNodePos);
                    break;
                case NodeType.Transition:
                    ActualSource = CalculateTransIntersection(sourceNodePos, targetNodePos);
                    break;
                default:
                    break;
            }
            SourceNodePos = sourceNodePos;
        }
        #endregion

        #endregion
    }
}
