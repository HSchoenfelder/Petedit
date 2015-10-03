using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PetriNetModel;

namespace PetriNetEditor
{
    /// <summary>
    /// This class provides logic for the manipulation of the workspace area of the petrinet.
    /// </summary>
    public class WorkspaceManager : INotifyPropertyChanged
    {
        #region fields
        /// <summary> Store for the Model property. </summary>
        private IModel _model;

        /// <summary> Store for the ElementProvider property. </summary>
        private IElementProvider _elementProvider;

        /// <summary> Store for the SelectionManager property. </summary>
        private ISelectionManager _selectionManager;

        /// <summary> Store for the UndoManager property. </summary>
        private IUndoManagerEx _undoManager;

        /// <summary> Store for the ElementCreator property. </summary>
        private ElementCreator _elementCreator;

        /// <summary> Store for the DrawMode property. </summary>
        private DrawMode _drawMode = DrawMode.Drawplace;
        
        /// <summary> Store for the RectBeyondEdge property. </summary>
        private Corners _rectBeyondEdge;

        /// <summary> Store for the Selecting property. </summary>
        private bool _selecting;

        /// <summary> Store for the Drawing property. </summary>
        private bool _drawing;

        /// <summary> Store for the SelectStart property. </summary>
        private Point _selectStart;

        /// <summary> Store for the SelectRectX property. </summary>
        private double _selectRectX;

        /// <summary> Store for the SelectRectY property. </summary>
        private double _selectRectY;

        /// <summary> Store for the SelectRectWidth property. </summary>
        private double _selectRectWidth;

        /// <summary> Store for the SelectRectHeight property. </summary>
        private double _selectRectHeight;

        /// <summary>Store for the ViewWidth property.</summary>
        private double _viewWidth;

        /// <summary>Store for the ViewHeight property.</summary>
        private double _viewHeight;

        /// <summary> Store for the RectSelectedNodes property. </summary>
        private readonly IList<String> _rectSelectedNodes;

        /// <summary> Store for the DrawModeChangeCommand property. </summary>
        private readonly IDelegateCommand _drawModeChangeCommand;

        /// <summary> Store for the MouseLeftButtonDownCommand property. </summary>
        private readonly IDelegateCommand _mouseLeftButtonDownCommand;

        /// <summary> Store for the SelectRectMouseMoveCommand property. </summary>
        private readonly IDelegateCommand _selectRectMouseMoveCommand;

        /// <summary> Store for the MouseLeftButtonUpCommand property. </summary>
        private readonly IDelegateCommand _mouseLeftButtonUpCommand;
        #endregion

        #region delegates
        public delegate void SelectingStateChangedEventHandler(object source, StateChangedEventArgs e);
        #endregion

        #region events
        /// <summary> Occurs when a tracked property value changes. </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> Occurs when a modification is made to the model. </summary>
        public event EventHandler Modified;

        /// <summary> Occurs when a reevaluation of command state is required. </summary>
        public event EventHandler ReevaluateCommandState;

        /// <summary> Occurs when the selecting state changed. </summary>
        public event SelectingStateChangedEventHandler SelectingStateChanged;
        #endregion

        #region properties

        #region private
        /// <summary> Gets the Model that allows for manipulation of the petrinet. </summary>
        private IModel Model
        {
            get { return _model; }
        }

        /// <summary>
        /// Gets the element provider that enables access to individual elements of the petrinet.
        /// operation.
        /// </summary>
        private IElementProvider ElementProvider
        {
            get { return _elementProvider; }
        }

        /// <summary> Gets the selection manager that provides access to select functions. </summary>
        private ISelectionManager SelectionManager
        {
            get { return _selectionManager; }
        }

        /// <summary> Gets the UndoManager that manages undo and redo operations. </summary>
        private IUndoManagerEx UndoManager
        {
            get { return _undoManager; }
        }

        /// <summary>
        /// Gets the element creator that allows for the creation of petrinet elements.
        /// operation.
        /// </summary>
        private ElementCreator ElementCreator
        {
            get { return _elementCreator; }
        }

        /// <summary>
        /// Gets the list that manages the VisualNodes that are selected during a rectangle
        /// select operation via their ids.
        /// </summary>
        private IList<String> RectSelectedNodes
        {
            get { return _rectSelectedNodes; }
        }
        #endregion

        #region public
        /// <summary>
        /// Gets or sets the current DrawMode. Allows switching between place, transition and
        /// rect select mode.
        /// </summary>
        public DrawMode DrawMode
        {
            get { return _drawMode; }
            set
            {
                if (_drawMode != value)
                {
                    _drawMode = value;
                    NotifyPropertyChanged();
                }
            }
        }
                
        /// <summary>
        /// Gets or sets the corner of the selecting rectangle that is currently beyond
        /// the bounds of the visual presentation and requires a scroll operation.
        /// </summary>
        public Corners RectBeyondEdge
        {
            get { return _rectBeyondEdge; }
            set
            {
                _rectBeyondEdge = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether a rectangle select operation is
        /// currently in progress.
        /// </summary>
        public bool Selecting
        {
            get { return _selecting; }
            set { _selecting = value; }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether an arc drawing operation is
        /// currently in progress.
        /// </summary>
        public bool Drawing
        {
            get { return _drawing; }
            set { _drawing = value; }
        }

        /// <summary>
        /// Gets or sets the starting point of the current rectangle select operation.
        /// </summary>
        private Point SelectStart
        {
            get { return _selectStart; }
            set { _selectStart = value; }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the top left point of the selection rectangle during
        /// a rectangle select operation.
        /// </summary>
        public double SelectRectX
        {
            get { return _selectRectX; }
            set
            {
                if (_selectRectX != value)
                {
                    _selectRectX = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the top left point of the selection rectangle during
        /// a rectangle select operation.
        /// </summary>
        public double SelectRectY
        {
            get { return _selectRectY; }
            set
            {
                if (_selectRectY != value)
                {
                    _selectRectY = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the selection rectangle during a rectangle select operation.
        /// </summary>
        public double SelectRectWidth
        {
            get { return _selectRectWidth; }
            set
            {
                if (_selectRectWidth != value)
                {
                    _selectRectWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the selection rectangle during a rectangle select operation.
        /// </summary>
        public double SelectRectHeight
        {
            get { return _selectRectHeight; }
            set
            {
                if (_selectRectHeight != value)
                {
                    _selectRectHeight = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary> 
        /// Gets or sets the width of the visual presentation of the petrinet. 
        /// </summary>
        public double ViewWidth
        {
            get { return _viewWidth; }
            set { _viewWidth = value; }
        }

        /// <summary> 
        /// Gets or sets the height of the visual presentation of the petrinet. 
        /// </summary>
        public double ViewHeight
        {
            get { return _viewHeight; }
            set { _viewHeight = value; }
        }

        /// <summary> Gets the command that is executed when the draw mode changes. </summary>
        public IDelegateCommand DrawModeChangeCommand
        {
            get { return _drawModeChangeCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when the MouseLeftButtonDown event occurs on the 
        /// presentation area.
        /// </summary>
        public IDelegateCommand MouseLeftButtonDownCommand
        {
            get { return _mouseLeftButtonDownCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when the MouseMove event occurs on the presentation 
        /// area.
        /// </summary>
        public IDelegateCommand SelectRectMouseMoveCommand
        {
            get { return _selectRectMouseMoveCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when the MouseLeftButtonUp event occurs on the 
        /// presentation area.
        /// </summary>
        public IDelegateCommand MouseLeftButtonUpCommand
        {
            get { return _mouseLeftButtonUpCommand; }
        }
        #endregion

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new WorkspaceManager with the specified references.
        /// </summary>
        /// <param name="elementProvider">Reference to the element provider.</param>
        /// <param name="undoManager">Reference to the undo manager.</param>
        /// <param name="selectionManager">Reference to the selection manager.</param>
        /// <param name="elementCreator">Reference to the element creator.</param>
        /// <param name="elementManager">Reference to the element manager.</param>
        /// <param name="model">Reference to the model of the petrinet.</param>
        public WorkspaceManager(IElementProvider elementProvider, IUndoManagerEx undoManager, ISelectionManager selectionManager,
                                ElementCreator elementCreator, IModel model)
        {
            _elementProvider = elementProvider;
            _undoManager = undoManager;
            _selectionManager = selectionManager;
            _elementCreator = elementCreator;
            _model = model;
            _rectSelectedNodes = new List<String>();

            CommandFactory commandFactory = new CommandFactory();
            _drawModeChangeCommand = commandFactory.Create<DrawMode>(HandleDrawModeChange);
            _mouseLeftButtonDownCommand = commandFactory.Create<Point, bool>(HandleMouseLeftButtonDown);
            _selectRectMouseMoveCommand = commandFactory.Create<Point>(HandleSelectRectMouseMove);
            _mouseLeftButtonUpCommand = commandFactory.Create<Point>(HandleMouseLeftButtonUp);
        }
        #endregion

        #region methods
        /// <summary>
        /// Notifies the view that a property of this ViewModel has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Handles the DrawModeChangeCommand. Sets the new draw mode.
        /// </summary>
        /// <param name="newMode">The new draw mode.</param>
        private void HandleDrawModeChange(DrawMode newMode)
        {
            DrawMode = newMode;
        }

        /// <summary>
        /// Handles the MouseLeftButtonDownCommand. Initializes the selection rectangle if in 
        /// select mode.
        /// </summary>
        /// <param name="coords">The coordinates of the MouseLeftButtonDownEvent.</param>
        /// <param name="alternate">A value that indicates whether a modifier key was pressed.</param>
        private void HandleMouseLeftButtonDown(Point coords, bool alternate)
        {
            if (DrawMode == DrawMode.Select)
                PrepareSelectRect(coords, alternate);
        }

        /// <summary>
        /// Handles the SelectRectMouseMoveCommand. Updates the visual presentation of the selecting 
        /// rectangle according to the provided coordinates.
        /// </summary>
        /// <param name="pos">The position of the mouse pointer after the move.</param>
        private void HandleSelectRectMouseMove(Point pos)
        {
            if (DrawMode == DrawMode.Select && Selecting)
                UpdateSelectRect(pos);
        }

        /// <summary>
        /// Handles the MouseLeftButtonUpCommand. Creates a place, a transition or finalizes the 
        /// rectangle selection according to the current draw mode. If an arc draw operation has
        /// been in progress, it is terminated.
        /// </summary>
        /// <param name="coords">The coordinates of the MouseLeftButtonUpEvent.</param>
        private void HandleMouseLeftButtonUp(Point coords)
        {
            // if not drawing arc
            if (!Drawing)
            {
                switch (DrawMode)
                {
                    case DrawMode.Drawplace:
                        UndoManager.AddCreateUndoViewSize(ViewWidth, ViewHeight);
                        UndoManager.AddCreateUndoId(ElementCreator.CreatePlace(coords));
                        UndoManager.PushCreateUndo();
                        UndoManager.ClearRedoStack();
                        Modified(this, new EventArgs());
                        ReevaluateCommandState(this, new EventArgs());
                        //SelectAllCommand.RaiseCanExecuteChanged();
                        break;
                    case DrawMode.Drawtrans:
                        UndoManager.AddCreateUndoViewSize(ViewWidth, ViewHeight);
                        UndoManager.AddCreateUndoId(ElementCreator.CreateTrans(coords));
                        UndoManager.PushCreateUndo();
                        UndoManager.ClearRedoStack();
                        Modified(this, new EventArgs());
                        ReevaluateCommandState(this, new EventArgs());
                        //SelectAllCommand.RaiseCanExecuteChanged();
                        break;
                    case DrawMode.Select:
                        FinalizeSelection();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Performs initialization for a rectangle select operation.
        /// </summary>
        /// <param name="coords">The coordinates of the starting point of the rectangle select 
        /// operation.</param>
        /// <param name="alternate">A value that indicates whether visual nodes should be
        /// added to the existing selection or if a new selection should be started.</param>
        private void PrepareSelectRect(Point coords, bool alternate)
        {
            SelectStart = new Point(coords.X, coords.Y);
            SelectRectX = SelectStart.X;
            SelectRectY = SelectStart.Y;
            if (!alternate)
                SelectionManager.ClearSelectedItems();
            SelectingStateChanged(this, new StateChangedEventArgs(true));
            ReevaluateCommandState(this, new EventArgs());
            UndoManager.UndoCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Updates the selection rectangle with new target coordinates.
        /// </summary>
        /// <param name="coords">The new target coordinates for the selection rectangle.</param>
        private void UpdateSelectRect(Point coords)
        {
            // adjust coordinates to presentation area boundaries
            coords.X = coords.X > ViewWidth - 1 ? ViewWidth - 1 : coords.X;
            coords.Y = coords.Y > ViewHeight - 1 ? ViewHeight - 1 : coords.Y;
            coords.X = coords.X < 1 ? 1 : coords.X;
            coords.Y = coords.Y < 1 ? 1 : coords.Y;

            double selectWidth = coords.X - SelectStart.X;
            double selectHeight = coords.Y - SelectStart.Y;
            // if drawing in bottom right direction
            if (selectWidth >= 0 && selectHeight >= 0)
            {
                SelectRectX = SelectStart.X;
                SelectRectY = SelectStart.Y;
                SelectRectWidth = selectWidth;
                SelectRectHeight = selectHeight;
                RectBeyondEdge = Corners.BottomRight;
            }
            // if drawing in bottom left direction
            if (selectWidth < 0 && selectHeight >= 0)
            {
                SelectRectX = coords.X;
                SelectRectY = SelectStart.Y;
                SelectRectWidth = -selectWidth;
                SelectRectHeight = selectHeight;
                RectBeyondEdge = Corners.BottomLeft;
            }
            // if drawing in top right direction
            if (selectWidth >= 0 && selectHeight < 0)
            {
                SelectRectX = SelectStart.X;
                SelectRectY = coords.Y;
                SelectRectWidth = selectWidth;
                SelectRectHeight = -selectHeight;
                RectBeyondEdge = Corners.TopRight;
            }
            // if drawing in top left direction
            if (selectWidth < 0 && selectHeight < 0)
            {
                SelectRectX = coords.X;
                SelectRectY = coords.Y;
                SelectRectWidth = -selectWidth;
                SelectRectHeight = -selectHeight;
                RectBeyondEdge = Corners.TopLeft;
            }
            DisplayRectSelection();
        }

        /// <summary>
        /// Adjust selection state of visual nodes and arcs according to the ongoing rectangle selection.
        /// </summary>
        private void DisplayRectSelection()
        {
            for (int i = 0; i < ElementProvider.NodesCount; i++)
            {
                IVisualNode node = ElementProvider.GetNode(i);
                if (node.IsContained(SelectRectX, SelectRectY, SelectRectWidth, SelectRectHeight))
                {
                    if (!node.Selected)
                    {
                        RectSelectedNodes.Add(node.Id);
                        node.Selected = true;
                    }
                }
                else
                {
                    if (RectSelectedNodes.Contains(node.Id))
                    {
                        node.Selected = false;
                        RectSelectedNodes.Remove(node.Id);
                    }
                }
            }
        }

        /// <summary>
        /// Finalizes the current rectangle selection.
        /// </summary>
        private void FinalizeSelection()
        {
            SelectRectWidth = 0;
            SelectRectHeight = 0;
            if (RectSelectedNodes.Count > 0)
                UndoManager.AddSelectUndoIds(SelectionManager.GetSelectedItems());
            SelectionManager.AddRange(RectSelectedNodes);
            if (RectSelectedNodes.Count > 0)
            {
                UndoManager.PushSelectUndo();
                UndoManager.ClearRedoStack();
            }
            RectSelectedNodes.Clear();
            SelectingStateChanged(this, new StateChangedEventArgs(false));
            ReevaluateCommandState(this, new EventArgs());
            UndoManager.UndoCommand.RaiseCanExecuteChanged();
        }
        #endregion
    }
}
