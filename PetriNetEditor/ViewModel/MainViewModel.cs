using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PetriNetModel;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace PetriNetEditor
{
    /// <summary>
    /// The main ViewModel class of the application that creates and manages 
    /// the various modules of the ViewModel.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        #region fields
        /// <summary> Store for the BaseSize property. </summary>
        private static readonly int _baseSize = 30;

        /// <summary> Store for the ArrowOffset property. </summary>
        private static readonly int _arrowOffset = 4;

        /// <summary> Store for the SizeFactor property. </summary>
        private int _sizeFactor = 1;

        /// <summary> Store for the DrawSize property. </summary>
        private int _drawSize = 30;

        /// <summary> Store for the ArrowheadSize property. </summary>
        private int _arrowheadSize = 4;

        /// <summary> Store for the Sizes property. </summary>
        private readonly ObservableCollection<int> _sizes = 
            new ObservableCollection<int> { 1, 2, 3, 4 };

        /// <summary> Store for the ViewWidth property. </summary>
        private double _viewWidth;

        /// <summary> Store for the ViewHeight property. </summary>
        private double _viewHeight;

        /// <summary> Store for the Model property. </summary>
        private IModel _model;
        
        /// <summary> Store for the ElementProvider property. </summary>
        private IElementProvider _elementProvider;

        /// <summary> Store for the SelectionManager property. </summary>
        private ISelectionManager _selectionManager;

        /// <summary> Store for the UndoManager property. </summary>
        private IUndoManager _undoManager;

        /// <summary> Store for the ElementCreator property. </summary>
        private IElementCreator _elementCreator;

        /// <summary> Store for the ElementManager property. </summary>
        private IElementManager _elementManager;

        /// <summary> Store for the WorkspaceManager property. </summary>
        private IWorkspaceManager _workspaceManager;
        
        /// <summary> Store for the UndoExecuter property. </summary>
        private IUndoExecuter _undoExecuter;

        /// <summary> Store for the BlockStatChange property. </summary>
        private bool _blockStateChange;

        /// <summary> Store for the SaveFile property. </summary>
        private String _saveFile;

        /// <summary> Store for the Modified property. </summary>
        private bool _modified;

        /// <summary> Store for the Selecting property. </summary>
        private bool _selecting;

        /// <summary> Store for the Drawing property. </summary>
        private bool _drawing;

        /// <summary> Store for the SizeChangeCommand property. </summary>
        private readonly IDelegateCommand _sizeChangeCommand;

        /// <summary> Store for the DeleteNodesCommand property. </summary>
        private readonly IDelegateCommand _deleteNodesCommand;

        /// <summary> Store for the SelectAllCommand property. </summary>
        private readonly IDelegateCommand _selectAllCommand;

        /// <summary> Store for the LoadedCommand property. </summary>
        private readonly IDelegateCommand _loadedCommand;

        /// <summary> Store for the NewFileCommand property. </summary>
        private readonly IDelegateCommand _newFileCommand;

        /// <summary> Store for the LoadFileCommand property. </summary>
        private readonly IDelegateCommand _loadFileCommand;

        /// <summary> Store for the SaveFileCommand property. </summary>
        private readonly IDelegateCommand _saveFileCommand;
        #endregion

        #region events
        /// <summary> Occurs when a tracked property value changes. </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> Occurs when an error needs to be communicated to the view. </summary>
        public event EventHandler<NotificationEventArgs> ErrorNotice;
        #endregion

        #region properties

        #region private
        /// <summary> Gets the base size of all visual nodes within the presentation. </summary>
        private static int BaseSize
        {
            get { return _baseSize; }
        }

        /// <summary>
        /// Gets the base offset for the arrowheads of all visual arcs within the presentation.
        /// </summary>
        private static int ArrowOffset
        {
            get { return _arrowOffset; }
        }

        /// <summary> Gets or sets the current global draw size. </summary>
        private int DrawSize
        {
            get { return _drawSize; }
            set
            {
                if (value != _drawSize)
                {
                    _drawSize = value;
                    OnDrawSizeChanged(value);
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary> Gets or sets the current global arrowhead size. </summary>
        private int ArrowheadSize
        {
            get { return _arrowheadSize; }
            set
            {
                if (value != _arrowheadSize)
                {
                    _arrowheadSize = value;
                    OnArrowheadSizeChanged(value);
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary> Gets the Model that allows for manipulation of the petrinet. </summary>
        private IModel Model
        {
            get { return _model; }
        }

        /// <summary> Gets the selection manager that provides access to select functions. </summary>
        private ISelectionManager SelectionManager
        {
            get { return _selectionManager; }
        }

        /// <summary>
        /// Gets the element creator that allows for the creation of petrinet elements.
        /// operation.
        /// </summary>
        private IElementCreator ElementCreator
        {
            get { return _elementCreator; }
        }

        /// <summary> Gets the undo executer that performs the undo and redo operations. </summary>
        private IUndoExecuter UndoExecuter
        {
            get { return _undoExecuter; }
        }
        #endregion

        #region public
        /// <summary> 
        /// Gets a collection that contains the available sizes for scaling of the visual elements.
        /// </summary>
        public ObservableCollection<int> Sizes
        {
            get { return _sizes; }
        }

        /// <summary>
        /// Gets or sets the factor with which the BaseSize is to be multiplied for scaling.
        /// </summary>
        public int SizeFactor
        {
            get { return _sizeFactor; }
            set
            {
                if (value != _sizeFactor)
                {
                    _sizeFactor = value;
                    OnSizeFactorChanged(value);
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
            set
            {
                _viewWidth = value;
                OnViewWidthChanged(value);
                NotifyPropertyChanged();
            }
        }

        /// <summary> 
        /// Gets or sets the height of the visual presentation of the petrinet. 
        /// </summary>
        public double ViewHeight
        {
            get { return _viewHeight; }
            set
            {
                _viewHeight = value;
                OnViewHeightChanged(value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the element provider that enables access to individual elements of the petrinet.
        /// operation.
        /// </summary>
        public IElementProvider ElementProvider
        {
            get { return _elementProvider; }
        }

        /// <summary> Gets the UndoManager that manages undo and redo operations. </summary>
        public IUndoManager UndoManager
        {
            get { return _undoManager; }
        }

        /// <summary> Gets the element manager that enables arc draw and move operations for nodes. </summary>
        public IElementManager ElementManager
        {
            get { return _elementManager; }
        }

        /// <summary> Gets the workspace manager that enables operations on the open workspace. </summary>
        public IWorkspaceManager WorkspaceManager
        {
            get { return _workspaceManager; }
        }
        
        /// <summary>
        /// Gets or sets a value which indicates whether mode changes are currently allowed.
        /// </summary>
        public bool BlockStateChange
        {
            get { return _blockStateChange; }
            set
            {
                if (_blockStateChange != value)
                {
                    _blockStateChange = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary> 
        /// Gets or sets the name of the file to perform save operations on. 
        /// </summary>
        public String SaveFile
        {
            get { return _saveFile; }
            set
            {
                _saveFile = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary> 
        /// Gets or sets a value which indicates whether modifications have been made since the last save. 
        /// </summary>
        public bool Modified
        {
            get { return UndoManager.UndoStackEmpty ? false : _modified; }
            set
            {
                _modified = value;
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
            set
            {
                if (_selecting != value)
                {
                    _selecting = value;
                    OnSelectingChanged(value);
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value which indicates whether an arc drawing operation is
        /// currently in progress.
        /// </summary>
        public bool Drawing
        {
            get { return _drawing; }
            set
            {
                if (_drawing != value)
                {
                    _drawing = value;
                    OnDrawingChanged(value);
                }
            }
        }

        /// <summary> 
        /// Gets the command that is executed when the size of the presentation area changes.
        /// </summary>
        public IDelegateCommand SizeChangeCommand
        {
            get { return _sizeChangeCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when the visual nodes are to be deleted from the
        /// presentation.
        /// </summary>
        public IDelegateCommand DeleteNodesCommand
        {
            get { return _deleteNodesCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when all visual nodes are to be selected.
        /// </summary>
        public IDelegateCommand SelectAllCommand
        {
            get { return _selectAllCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when LoadedEvent occurs on the presentation area.
        /// </summary>
        public IDelegateCommand LoadedCommand
        {
            get { return _loadedCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when the drawing area is cleared.
        /// </summary>
        public IDelegateCommand NewFileCommand
        {
            get { return _newFileCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when a file is opened.
        /// </summary>
        public IDelegateCommand LoadFileCommand
        {
            get { return _loadFileCommand; }
        }

        /// <summary>
        /// Gets the command that is executed when saving to file.
        /// </summary>
        public IDelegateCommand SaveFileCommand
        {
            get { return _saveFileCommand; }
        }
        #endregion

        #endregion

        #region constructors
        /// <summary>
        /// Initalizes the ViewModel.
        /// </summary>
        public MainViewModel()
        {
            // initialize model
            ModelFactory mf = new ModelFactory();
            _model = mf.CreateModel();

            // module initializations
            DependencyFactory df = new DependencyFactory();
            _elementProvider = df.CreateProvider();
            _selectionManager = df.CreateSelectionManager(ElementProvider, Model);
            _undoManager = df.CreateUndoManager();
            _elementManager = df.CreateElementManager(ElementProvider, SelectionManager, (IUndoManagerEx)UndoManager, Model, DrawSize, ArrowheadSize);
            _elementCreator = df.CreateElementCreator(ElementProvider, SelectionManager, ElementManager, Model, DrawSize, ArrowheadSize);
            _workspaceManager = df.CreateWorkspaceManager(ElementProvider, (IUndoManagerEx)UndoManager, SelectionManager, ElementCreator, Model);
            _undoExecuter = df.CreateUndoExecuter(Model, ElementProvider, SelectionManager, (IUndoManager)UndoManager, ElementCreator, ElementManager);
            UndoManager.UndoTarget = UndoExecuter;

            // event subscriptions
            SelectionManager.ReevaluateCommandState += ReevaluateAllCommands;
            UndoExecuter.ReevaluateCommandState += ReevaluateAllCommands;
            UndoExecuter.Modified += SetModified;
            UndoExecuter.ViewSizeChanged += SetViewSize;
            UndoExecuter.SizeFactorChanged += SetSizeFactor;
            WorkspaceManager.Modified += SetModified;
            WorkspaceManager.ReevaluateCommandState += ReevaluateAllCommands;
            WorkspaceManager.SelectingStateChanged += SetSelecting;
            ElementManager.Modified += SetModified;
            ElementManager.ReevaluateCommandState += ReevaluateAllCommands;
            ElementManager.BlockStateChanged += SetBlockStateChange;
            ElementManager.ViewSizeChanged += SetViewSize;
            ElementManager.DrawingStateChanged += SetDrawing;
            Model.TokensChangedEvent += ElementManager.HandleModelTokensChanged;
            Model.TransitionStateChangedEvent += ElementManager.HandleModelTransitionStateChanged;

            // connect command handlers
            CommandFactory commandFactory = new CommandFactory();
            _sizeChangeCommand = commandFactory.Create<int>(CommandTypes.SizeChangeCommand, HandleSizeChange);
            _deleteNodesCommand = commandFactory.Create<String>(CommandTypes.DeleteNodesCommand, HandleDeleteNodes, CanDeleteNodes);
            _selectAllCommand = commandFactory.Create<String>(CommandTypes.SelectAllCommand, HandleSelectAll, CanSelectAll);
            _loadedCommand = commandFactory.Create<NPoint>(CommandTypes.LoadedCommand, HandleLoaded);
            _newFileCommand = commandFactory.Create<String>(CommandTypes.NewFileCommand, HandleFileNew);
            _loadFileCommand = commandFactory.Create<String>(CommandTypes.LoadFileCommand, HandleFileLoad);
            _saveFileCommand = commandFactory.Create<String>(CommandTypes.SaveFileCommand, HandleFileSave);
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
        /// Called when the SizeFactor for global scaling changes. Recalculates the global 
        /// DrawSize and ArrowheadSize and updates all modules that need it with the new SizeFactor.
        /// </summary>
        /// <param name="newSizeFactor"></param>
        private void OnSizeFactorChanged(int newSizeFactor)
        {
            ArrowheadSize = _arrowOffset * newSizeFactor;
            DrawSize = BaseSize * newSizeFactor;
            UndoExecuter.SizeFactor = newSizeFactor;
        }

        /// <summary>
        /// Called when the global DrawSize changes. Distributes the new draw size to all modules
        /// that need it and adjusts the size of the presentation area.
        /// </summary>
        /// <param name="newDrawSize">The new global draw size.</param>
        private void OnDrawSizeChanged(int newDrawSize)
        {
            ElementManager.DrawSize = newDrawSize;
            ElementCreator.DrawSize = newDrawSize;
            for (int i = 0; i < ElementProvider.ArcsCount; i++)
            {
                IVisualArc arc = ElementProvider.GetArc(i);
                arc.DrawSize = newDrawSize;
            }
            for (int i = 0; i < ElementProvider.NodesCount; i++)
            {
                IVisualNode node = ElementProvider.GetNode(i);
                node.DrawSize = newDrawSize;
                ElementManager.AdjustViewSize(node.Id, UndoRedoOps.ChangeSize);
            }
            for (int i = 0; i < ElementProvider.NameFieldsCount; i++)
            {
                INameField nField = ElementProvider.GetNameField(i);
                nField.DrawSize = newDrawSize;
            }
        }

        /// <summary>
        /// Called when the global ArrowheadSize changes. Distributes the new arrowhead size to all
        /// modules that need it.
        /// </summary>
        /// <param name="newArrowheadSize">The new global arrowhead size.</param>
        private void OnArrowheadSizeChanged(int newArrowheadSize)
        {
            ElementManager.ArrowheadSize = newArrowheadSize;
            ElementCreator.ArrowheadSize = newArrowheadSize;
            for (int i = 0; i < ElementProvider.ArcsCount; i++)
            {
                IVisualArc arc = ElementProvider.GetArc(i);
                arc.ArrowheadSize = newArrowheadSize;
            }
        }

        /// <summary>
        /// Called when the width of the presentation area changes. Distributes the new width to all 
        /// modules that need it.
        /// </summary>
        /// <param name="newSizeFactor"></param>
        private void OnViewWidthChanged(double newViewWidth)
        {
            WorkspaceManager.ViewWidth = newViewWidth;
            ElementManager.ViewWidth = newViewWidth;
            UndoExecuter.ViewWidth = newViewWidth;
        }

        /// <summary>
        /// Called when the height of the presentation area changes. Distributes the new height to all 
        /// modules that need it.
        /// </summary>
        /// <param name="newSizeFactor"></param>
        private void OnViewHeightChanged(double newViewHeight)
        {
            WorkspaceManager.ViewHeight = newViewHeight;
            ElementManager.ViewHeight = newViewHeight;
            UndoExecuter.ViewHeight = newViewHeight;
        }

        /// <summary>
        /// Called when the selecting state changes. Distributes the new selecting state to all 
        /// modules that need it.
        /// </summary>
        /// <param name="newSSelecting"></param>
        private void OnSelectingChanged(bool newSelecting)
        {
            WorkspaceManager.Selecting = newSelecting;
            UndoManager.Selecting = newSelecting;
        }

        /// <summary>
        /// Called when the drawing state changes. Distributes the new drawing state to all 
        /// modules that need it.
        /// </summary>
        /// <param name="newSizeDrawing"></param>
        private void OnDrawingChanged(bool newDrawing)
        {
            WorkspaceManager.Drawing = newDrawing;
            UndoManager.Drawing = newDrawing;
        }

        /// <summary>
        /// Handles the SizeChangeCommand. Sets the new size factor.
        /// </summary>
        /// <param name="newSize">The new global size factor.</param>
        private void HandleSizeChange(int newSize)
        {
            if (SizeFactor != newSize)
            {
                UndoManager.AddSizeChangeUndoParams(SizeFactor, ViewWidth, ViewHeight);
                SizeFactor = newSize;
                UndoManager.PushSizeChangeUndo();
                UndoManager.ClearRedoStack();
                Modified = true;
            }
        }

        /// <summary>
        /// Handles the LoadedCommand. Sets the new width and height of the presentation area.
        /// </summary>
        /// <param name="p">A point containing the new width and height of the presentation area.
        /// </param>
        private void HandleLoaded(NPoint p)
        {
            ViewWidth = p.X;
            ViewHeight = p.Y;
        }

        /// <summary>
        /// Handles the DeleteNodesCommand. Deletes all automatically selected visual arcs as 
        /// well as all selected visual nodes and arcs from the presentation and the model.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void HandleDeleteNodes(String parameter)
        {
            IList<String> autoSelectedArcs = SelectionManager.GetAutoSelectedArcs();
            IList<String> selectedItems = SelectionManager.GetSelectedItems();
            SelectionManager.ClearAutoSelectedArcs();
            SelectionManager.ClearSelectedItems();
            foreach (String arcId in autoSelectedArcs)
            {
                // save arc info for undo operation
                String[] nodes = Model.GetNodesForArc(arcId);
                String sourceId = Model.IsSource(arcId, nodes[0]) ? nodes[0] : nodes[1];
                String targetId = Model.IsSource(arcId, nodes[0]) ? nodes[1] : nodes[0];
                if (ElementProvider.GetArc(arcId).Selected)
                    UndoManager.AddDeleteUndoArcInfo(arcId, sourceId, targetId, true);
                else
                    UndoManager.AddDeleteUndoArcInfo(arcId, sourceId, targetId, false);
                // remove arc
                ElementManager.RemoveBrother(arcId);
                Model.RemoveArc(arcId);
                ElementProvider.RemoveArc(arcId);
            }
            foreach (String elementId in selectedItems)
            {
                if (Model.IsNode(elementId))
                {
                    // save node info for undo operation
                    int? tokenCount = ElementProvider.GetNode(elementId).NodeType == NodeType.Place ?
                                      (int?)Model.GetTokenCount(elementId) : null;
                    UndoManager.AddDeleteUndoNodeInfo(elementId, Model.GetName(elementId), (int)Model.GetCoordinates(elementId).X,
                                            (int)Model.GetCoordinates(elementId).Y, tokenCount);
                    // remove node
                    Model.RemoveNode(elementId);
                    ElementProvider.RemoveNode(elementId);
                    // remove associated name field
                    ElementProvider.RemoveNameField(elementId); 
                }
                else
                {
                    // if a selected arc has already been deleted as an automatically selected
                    // arc, do not try to remove it again
                    if (Model.Contains(elementId))
                    {
                        // save arc info for undo operation
                        String[] nodes = Model.GetNodesForArc(elementId);
                        String sourceId = Model.IsSource(elementId, nodes[0]) ? nodes[0] : nodes[1];
                        String targetId = Model.IsSource(elementId, nodes[0]) ? nodes[1] : nodes[0];
                        UndoManager.AddDeleteUndoArcInfo(elementId, sourceId, targetId, true);
                        // remove arc
                        ElementManager.RemoveBrother(elementId);
                        Model.RemoveArc(elementId);
                        ElementProvider.RemoveArc(elementId);
                    }
                }
            }
            ElementManager.DragSourceId = null;
            DeleteNodesCommand.RaiseCanExecuteChanged();
            SelectAllCommand.RaiseCanExecuteChanged();
            UndoManager.PushDeleteUndo();
            UndoManager.ClearRedoStack();
            Modified = true;
        }

        /// <summary>
        /// Determines whether the DeleteNodesCommand is enabled or disabled.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>true if there are selected items and no act draw or rect
        /// select operation is in progress; otherwise false.</returns>
        private bool CanDeleteNodes(String parameter)
        {
            if (Drawing == false && Selecting == false)
                return SelectionManager.SelectedItemsCount >= 1;
            else
                return false;
        }

        /// <summary>
        /// Handles the SelectAllCommand. Selects all visual arcs and nodes. 
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void HandleSelectAll(String parameter)
        {
            UndoManager.AddSelectUndoIds(SelectionManager.GetSelectedItems());
            for (int i = 0; i < ElementProvider.NodesCount; i++)
            {
                String nodeId = ElementProvider.GetNodeId(i);
                if (!SelectionManager.SelectionContains(nodeId))
                    SelectionManager.SelectElement(nodeId); 
            }
            for (int i = 0; i < ElementProvider.ArcsCount; i++)
            {
                String arcId = ElementProvider.GetArcId(i);
                if (!SelectionManager.SelectionContains(arcId))
                    SelectionManager.SelectElement(arcId);
            }
            UndoManager.PushSelectUndo();
            UndoManager.ClearRedoStack();
            DeleteNodesCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Determines whether the SelectAllCommand is enabled or disabled.
        /// </summary>
        /// <param name="parameter"> The command parameter. </param>
        /// <returns>true if there are visual nodes; otherwise false.</returns>
        private bool CanSelectAll(String parameter)
        {
            if (ElementProvider.NodesCount > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Handles the NewFileCommand. Clears the workspace and reinitializes the model.
        /// </summary>
        /// <param name="parameter"> The command parameter. </param>
        private void HandleFileNew(String parameter)
        {
            Model.Reinitialize();
            Reinitialize();
            SaveFile = null;
        }

        /// <summary>
        /// Handles the LoadFileCommand. Loads the petrinet from the provided file.
        /// </summary>
        /// <param name="filename"> The name of the file to load the petrinet from. </param>
        private void HandleFileLoad(String filename)
        {
            Model.Reinitialize();
            Reinitialize();
            SaveFile = filename;
            PNMLAccessorFactory accessorFactory = new PNMLAccessorFactory();
            IPNMLParser parser = accessorFactory.CreateParser(filename, ElementCreator);
            try
            {
                parser.Parse();
            }
            catch (Exception e)
            {
                Model.Reinitialize();
                Reinitialize();
                SaveFile = null;
                if (ErrorNotice != null)
                    ErrorNotice(this, new NotificationEventArgs(e.Message));
            }
            finally
            {
                parser.CloseParser();
            }
        }

        /// <summary>
        /// Handles the SaveFileCommand. Saves the petrinet to the specified file.
        /// </summary>
        /// <param name="filename"> The name of the file to save the petrinet to.</param>
        private void HandleFileSave(String filename)
        {
            if (filename != null)
                SaveFile = filename;
            using (XmlWriter xmlWriter = XmlWriter.Create(SaveFile))
            {
                PNMLAccessorFactory accessorFactory = new PNMLAccessorFactory();
                IPNMLWriter pnmlWriter = accessorFactory.CreateWriter(xmlWriter);
                pnmlWriter.StartXMLDocument();
                for (int i = 0; i < ElementProvider.NodesCount; i++)
                {
                    String nodeId = ElementProvider.GetNodeId(i);
                    if (Model.IsPlace(nodeId))
                        pnmlWriter.AddPlace(nodeId, Model.GetName(nodeId), Model.GetCoordinates(nodeId).X.ToString(),
                                            Model.GetCoordinates(nodeId).Y.ToString(), Model.GetTokenCount(nodeId).ToString());
                    else
                        pnmlWriter.AddTransition(nodeId, Model.GetName(nodeId), Model.GetCoordinates(nodeId).X.ToString(),
                                                 Model.GetCoordinates(nodeId).Y.ToString());
                }
                for (int i = 0; i < ElementProvider.ArcsCount; i++)
                {
                    String arcId = ElementProvider.GetArcId(i);
                    String[] nodes = Model.GetNodesForArc(arcId);
                    String sourceId = Model.IsSource(arcId, nodes[0]) ? nodes[0] : nodes[1];
                    String targetId = Model.IsSource(arcId, nodes[0]) ? nodes[1] : nodes[0];
                    pnmlWriter.AddArc(arcId, sourceId, targetId);
                }
                pnmlWriter.FinishXMLDocument();
            }
            Modified = false;
        }

        /// <summary>
        /// Reinitializes the visual presentation of the petrinet.
        /// </summary>
        private void Reinitialize()
        {
            SelectionManager.ClearSelectedItems();
            SelectionManager.ClearAutoSelectedArcs();
            ElementProvider.ClearNameFields();
            ElementProvider.ClearNodes();
            ElementProvider.ClearArcs();
            UndoManager.ClearUndoStack();
            UndoManager.ClearRedoStack();
        }

        /// <summary>
        /// Handles the ReevaluateCommandState event. Reevaluates the can execute state of all 
        /// commands on the main ViewModel.
        /// </summary>
        /// <param name="sender">The module that raised the event.</param>
        /// <param name="e">The EventArgs.</param>
        private void ReevaluateAllCommands(object sender, EventArgs e)
        {
            DeleteNodesCommand.RaiseCanExecuteChanged();
            SelectAllCommand.RaiseCanExecuteChanged();
            
        }

        /// <summary>
        /// Handles the Modified event. Sets the modified flag to true.
        /// </summary>
        /// <param name="sender">The module that raised the event.</param>
        /// <param name="e">The EventArgs.</param>
        private void SetModified(object sender, EventArgs e)
        {
            Modified = true;
        }

        /// <summary>
        /// Handles the BlockStateChanged event. Sets the block state change flag to
        /// the respective value.
        /// </summary>
        /// <param name="sender">The module that raised the event.</param>
        /// <param name="e">The EventArgs containing the new state of the flag.</param>
        private void SetBlockStateChange(object sender, StateChangedEventArgs e)
        {
            BlockStateChange = e.State;
        }

        /// <summary>
        /// Handles the SelectingStateChanged event. Sets the selecting flag to
        /// the respective value.
        /// </summary>
        /// <param name="sender">The module that raised the event.</param>
        /// <param name="e">The EventArgs containing the new state of the flag.</param>
        private void SetSelecting(object sender, StateChangedEventArgs e)
        {
            Selecting = e.State;
        }

        /// <summary>
        /// Handles the DrawingStateChanged event. Sets the drawing flag to 
        /// the respective value.
        /// </summary>
        /// <param name="sender">The module that raised the event.</param>
        /// <param name="e">The EventArgs containing the new state of the flag.</param>
        private void SetDrawing(object sender, StateChangedEventArgs e)
        {
            Drawing = e.State;
        }

        /// <summary>
        /// Handles the ViewSizeChanged event. Sets the new view width and
        /// view height respectively.
        /// </summary>
        /// <param name="sender">The module that raised the event.</param>
        /// <param name="e">The EventArgs containing the new view width and view height</param>
        private void SetViewSize(object sender, ViewSizeChangedEventArgs e)
        {
            ViewWidth = e.ViewWidth;
            ViewHeight = e.ViewHeight;
        }

        /// <summary>
        /// Handles the SizeFactorChanged event. Sets the new size factor.
        /// </summary>
        /// <param name="sender">The module that raised the event.</param>
        /// <param name="e">The EventArgs containing the new size factor.</param>
        private void SetSizeFactor(object sender, SizeFactorChangedEventArgs e)
        {
            SizeFactor = e.SizeFactor;
        }
        #endregion
    }
}
