using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetriNetEditor
{
    /// <summary>
    /// This class provides methods to perform undo and redo operations on the petrinet.
    /// </summary>
    public class UndoExecuter
    {
        #region fields
        /// <summary> Store for the Model property. </summary>
        private IModel _model;

        /// <summary> Store for the ElementProvider property. </summary>
        private ElementProvider _elementProvider;

        /// <summary> Store for the SelectionManager property. </summary>
        private SelectionManager _selectionManager;
        
        /// <summary> Store for the UndoManager property. </summary>
        private UndoManager _undoManager;

        /// <summary> Store for the ElementCreator property. </summary>
        private ElementCreator _elementCreator;

        /// <summary> Store for the ElementManager property. </summary>
        private ElementManager _elementManager;

        /// <summary> Store for the SizeFactor property. </summary>
        private int _sizeFactor = 1;

        /// <summary> Store for the ViewWidth property. </summary>
        private double _viewWidth;

        /// <summary> Store for the ViewHeight property. </summary>
        private double _viewHeight;

        /// <summary> Store for the Drawing property. </summary>
        private bool _drawing;

        /// <summary> Store for the Selecting property. </summary>
        private bool _selecting;
        #endregion

        #region delegates
        public delegate void ViewSizeChangedEventHandler(object source, ViewSizeChangedEventArgs e);

        public delegate void SizeFactorChangedEventHandler(object source, SizeFactorChangedEventArgs e);
        #endregion

        #region events
        /// <summary> Occurs when a reevaluation of command state is required. </summary>
        public event EventHandler ReevaluateCommandState;

        /// <summary> Occurs when a modification is made to the model. </summary>
        public event EventHandler Modified;

        /// <summary> Occurs when a modification is made to ViewWidth or ViewHeight. </summary>
        public event ViewSizeChangedEventHandler ViewSizeChanged;

        /// <summary> Occurs when a modification is made to SizeFactor. </summary>
        public event SizeFactorChangedEventHandler SizeFactorChanged;
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
        private ElementProvider ElementProvider
        {
            get { return _elementProvider; }
        }

        /// <summary> Gets the selection manager that provides access to select functions. </summary>
        private SelectionManager SelectionManager
        {
            get { return _selectionManager; }
        }

        /// <summary> Gets the UndoManager that manages undo and redo operations. </summary>
        public UndoManager UndoManager
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

        /// <summary> Gets the element manager that enables arc draw and move operations for nodes. </summary>
        private ElementManager ElementManager
        {
            get { return _elementManager; }
        }
        #endregion

        #region public
        /// <summary>
        /// Gets or sets the factor with which the BaseSize is to be multiplied for scaling.
        /// </summary>
        public int SizeFactor
        {
            get { return _sizeFactor; }
            set { _sizeFactor = value; }
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

        /// <summary> 
        /// Gets or sets the a value that indicates whether a drawing operation is currently in progress. 
        /// </summary>
        public bool Drawing
        {
            get { return _drawing; }
            set { _drawing = value; }
        }

        /// <summary> 
        /// Gets or sets the a value that indicates whether a selecting operation is currently in progress. 
        /// </summary>
        public bool Selecting
        {
            get { return _selecting; }
            set { _selecting = value; }
        }
        #endregion

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new UndoExecuter with the specified references.
        /// </summary>
        /// <param name="model">Reference to the model of the petrinet.</param>
        /// <param name="elementProvider">Reference to the element provider.</param>
        /// <param name="selectionManager">Reference to the selection manager.</param>
        /// <param name="undoManager">Reference to the undo manager.</param>
        /// <param name="elementCreator">Reference to the element creator.</param>
        /// <param name="elementManager">Reference to the element manager.</param>
        public UndoExecuter(IModel model, ElementProvider elementProvider, SelectionManager selectionManager,
                            UndoManager undoManager, ElementCreator elementCreator, ElementManager elementManager)
        {
            _model = model;
            _elementProvider = elementProvider;
            _selectionManager = selectionManager;
            _undoManager = undoManager;
            _elementCreator = elementCreator;
            _elementManager = elementManager;
        }
        #endregion

        #region methods

        #region private
        /// <summary>
        /// Shifts all nodes in the petrinet to the left for the specified amount.
        /// </summary>
        /// <param name="leftShift">The amount for which to shift all nodes to the left.</param>
        private void ShiftAllLeft(double leftShift)
        {
            for (int i = 0; i < ElementProvider.NodesCount; i++)
            {
                IVisualNode node = ElementProvider.GetNode(i);
                node.XPos += leftShift;
                Model.ChangePosition(node.Id, (int)node.XPos, (int)node.YPos);
                ElementProvider.GetNameField(node.Id).AdjustNameField();
            }
        }

        /// <summary>
        /// Shifts all nodes in the petrinet up for the specified amount.
        /// </summary>
        /// <param name="leftShift">The amount for which to shift all nodes up.</param>
        private void ShiftAllUp(double upShift)
        {
            for (int i = 0; i < ElementProvider.NodesCount; i++)
            {
                IVisualNode node = ElementProvider.GetNode(i);
                node.YPos += upShift;
                Model.ChangePosition(node.Id, (int)node.XPos, (int)node.YPos);
                ElementProvider.GetNameField(node.Id).AdjustNameField();
            }
        }
        #endregion

        #region public
        /// <summary>
        /// Undoes a move operation.
        /// </summary>
        /// <param name="ids">A list with the ids of the visual elements that were moved.</param>
        /// <param name="offset">The move offset.</param>
        /// <param name="viewSize">The view size before the move.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction</param>
        public void UndoMove(IList<String> ids, Point offset, Point viewSize, Point shift)
        {
            UndoManager.CreateMoveRedoParams(-offset.X, -offset.Y, -shift.X, -shift.Y, ViewWidth, ViewHeight);
            // undo move
            foreach (String id in ids)
            {
                UndoManager.AddMoveRedoId(id);
                ElementProvider.GetNode(id).XPos -= offset.X;
                ElementProvider.GetNode(id).YPos -= offset.Y;
                ElementProvider.GetNameField(id).AdjustNameField(); 
                Model.ChangePosition(id, (int)ElementProvider.GetNode(id).XPos, (int)ElementProvider.GetNode(id).YPos);
            }
            // undo shift
            for (int i = 0; i < ElementProvider.NodesCount; i++)
            {
                IVisualNode node = ElementProvider.GetNode(i);
                if (!ids.Contains(node.Id))
                {
                    node.XPos -= shift.X;
                    node.YPos -= shift.Y;
                    ElementProvider.GetNameField(node.Id).AdjustNameField(); 
                    Model.ChangePosition(node.Id, (int)node.XPos, (int)node.YPos);
                }
            }
            UndoManager.PushMoveRedo();
            ViewSizeChanged(this, new ViewSizeChangedEventArgs(viewSize.X, viewSize.Y));
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Redoes a move operation.
        /// </summary>
        /// <param name="ids">A list with the ids of the visual elements that were moved.</param>
        /// <param name="offset">The move offset.</param>
        /// <param name="viewSize">The view size before the move.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction.</param>
        public void RedoMove(IList<String> ids, Point offset, Point viewSize, Point shift)
        {
            UndoManager.CreateMoveUndoParams(-offset.X, -offset.Y, -shift.X, -shift.Y, ViewWidth, ViewHeight);
            // redo move
            foreach (String id in ids)
            {
                UndoManager.AddMoveUndoId(id);
                ElementProvider.GetNode(id).XPos -= offset.X;
                ElementProvider.GetNode(id).YPos -= offset.Y;
                ElementProvider.GetNameField(id).AdjustNameField(); 
                Model.ChangePosition(id, (int)ElementProvider.GetNode(id).XPos, (int)ElementProvider.GetNode(id).YPos);
            }
            // redo shift
            for (int i = 0; i < ElementProvider.NodesCount; i++)
            {
                IVisualNode node = ElementProvider.GetNode(i);
                if (!ids.Contains(node.Id))
                {
                    node.XPos -= shift.X;
                    node.YPos -= shift.Y;
                    ElementProvider.GetNameField(node.Id).AdjustNameField(); 
                    Model.ChangePosition(node.Id, (int)node.XPos, (int)node.YPos);
                }
            }
            UndoManager.PushMoveUndo();
            ViewSizeChanged(this, new ViewSizeChangedEventArgs(viewSize.X, viewSize.Y));
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Undoes a name change operation.
        /// </summary>
        /// <param name="id">The id of the node that had its name changed.</param>
        /// <param name="name">The previous name of the node.</param>
        /// <param name="viewSize">The view size before the name change.</param>
        /// <param name="nfSize">The size of the name field before the name change.</param>
        /// <param name="nodePos">The position of the node before the name change.</param>
        public void UndoNameChange(String id, String name, Point viewSize, Point nfSize, Point nodePos)
        {
            IVisualNode node = ElementProvider.GetNode(id);
            INameField nField = ElementProvider.GetNameField(id);
            UndoManager.AddNameChangeRedoParams(node.Id, nField.Name, ViewWidth, ViewHeight,
                                                nField.Width, nField.Height, node.XPos, node.YPos);
            // undo name change
            nField.Name = name;
            if (name != null)
            {
                Model.ChangeName(id, name);
                nField.NameFieldVisible = true;
                nField.Width = nfSize.X;
                nField.Height = nfSize.Y;
            }
            else
            {
                nField.NameFieldVisible = false;
                nField.Width = 0;
                nField.Height = 0;
                Model.ChangeName(id, "");
            }
            // undo shift
            if (Math.Abs(nodePos.X - node.XPos) >= 1)
                ShiftAllLeft(nodePos.X - node.XPos);
            if (Math.Abs(nodePos.Y - node.YPos) >= 1)
                ShiftAllUp(nodePos.Y - node.YPos);
            ViewSizeChanged(this, new ViewSizeChangedEventArgs(viewSize.X, viewSize.Y));
            Modified(this, new EventArgs());
            UndoManager.PushNameChangeRedo();
        }

        /// <summary>
        /// Redoes a name change operation.
        /// </summary>
        /// <param name="id">The id of the node that had its name changed.</param>
        /// <param name="name">The previous name of the node.</param>
        /// <param name="viewSize">The view size before the name change.</param>
        /// <param name="nfSize">The size of the name field before the name change.</param>
        /// <param name="nodePos">The position of the node before the name change.</param>
        public void RedoNameChange(String id, String name, Point viewSize, Point nfSize, Point nodePos)
        {
            IVisualNode node = ElementProvider.GetNode(id);
            INameField nField = ElementProvider.GetNameField(id);
            UndoManager.AddNameChangeUndoParams(node.Id, nField.Name, ViewWidth, ViewHeight,
                                                nField.Width, nField.Height, node.XPos, node.YPos);
            // redo name change
            nField.Name = name;
            if (name != null)
            {
                Model.ChangeName(id, name);
                nField.NameFieldVisible = true;
                nField.Width = nfSize.X;
                nField.Height = nfSize.Y;
            }
            else
            {
                nField.NameFieldVisible = false;
                nField.Width = 0;
                nField.Height = 0;
                Model.ChangeName(id, "");
            }
            // redo shift
            if (Math.Abs(nodePos.X - node.XPos) >= 1)
                ShiftAllLeft(nodePos.X - node.XPos);
            if (Math.Abs(nodePos.Y - node.YPos) >= 1)
                ShiftAllUp(nodePos.Y - node.YPos);
            ViewSizeChanged(this, new ViewSizeChangedEventArgs(viewSize.X, viewSize.Y));
            Modified(this, new EventArgs());
            UndoManager.PushNameChangeUndo();
        }

        /// <summary>
        /// Undoes a select operation.
        /// </summary>
        /// <param name="selectIds">A list with the ids of the visual elements that whose selection
        /// state was changed.</param>
        public void UndoSelect(List<String> selectIds)
        {
            UndoManager.AddSelectRedoIds(SelectionManager.GetSelectedItems());
            SelectionManager.ClearSelectedItems();
            if (selectIds != null)
            {
                foreach (String id in selectIds)
                    SelectionManager.SelectElement(id);
            }
            UndoManager.PushSelectRedo();
        }

        /// <summary>
        /// Redoes a select operation.
        /// </summary>
        /// <param name="selectIds">A list with the ids of the visual elements that whose selection
        /// state was changed.</param>
        public void RedoSelect(List<String> selectIds)
        {
            UndoManager.AddSelectUndoIds(SelectionManager.GetSelectedItems());
            SelectionManager.ClearSelectedItems();
            if (selectIds != null)
            {
                foreach (String id in selectIds)
                    SelectionManager.SelectElement(id);
            }
            UndoManager.PushSelectUndo();
        }

        /// <summary>
        /// Undoes a delete operation.
        /// </summary>
        /// <param name="nodes">A list that contains node info for the deleted nodes.</param>
        /// <param name="arcs">A list that contains arc info for the deleted arcs.</param>
        public void UndoDelete(List<NodeInfo> nodes, List<ArcInfo> arcs)
        {
            if (nodes != null)
            {
                foreach (NodeInfo node in nodes)
                {
                    UndoManager.AddDeleteRedoId(node.Id);
                    if (node.TokenCount != null)
                    {
                        ElementCreator.CreatePlace(new Point(node.X, node.Y), node.Id);
                        ElementProvider.GetNode(node.Id).SetTokens((int)node.TokenCount);
                    }
                    else
                        ElementCreator.CreateTrans(new Point(node.X, node.Y), node.Id);
                    if (node.Name != null)
                    {
                        INameField nField = ElementProvider.GetNameField(node.Id);
                        nField.DrawSize = ElementManager.DrawSize;
                        nField.SetName(node.Name);
                    }
                    SelectionManager.SelectElement(node.Id);
                }
            }

            if (arcs != null)
            {
                foreach (ArcInfo arc in arcs)
                {
                    UndoManager.AddDeleteRedoId(arc.Id);
                    ElementCreator.CreateArc(arc.Id, arc.SourceId, arc.TargetId, arc.Selected);
                }
            }
            ReevaluateCommandState(this, new EventArgs());
            UndoManager.PushDeleteRedo();
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Redoes a delete operation.
        /// </summary>
        /// <param name="ids">A list that a list with the ids of the elements that were deleted.</param>
        public void RedoDelete(List<String> ids)
        {
            SelectionManager.ClearAutoSelectedArcs();
            SelectionManager.ClearSelectedItems();
            // remove arcs
            foreach (String id in ids)
            {
                if (!Model.IsNode(id))
                {
                    String[] nodes = Model.GetNodesForArc(id);
                    String sourceId = Model.IsSource(id, nodes[0]) ? nodes[0] : nodes[1];
                    String targetId = Model.IsSource(id, nodes[0]) ? nodes[1] : nodes[0];
                    UndoManager.AddDeleteUndoArcInfo(id, sourceId, targetId, ElementProvider.GetArc(id).Selected);
                    ElementManager.RemoveBrother(id);
                    ElementProvider.RemoveArc(id);
                    Model.RemoveArc(id);
                }
            }
            // remove nodes
            foreach (String id in ids)
            {
                if (Model.IsNode(id))
                {
                    UndoManager.AddDeleteUndoNodeInfo(id, Model.GetName(id),
                                           (int)Model.GetCoordinates(id).X, (int)Model.GetCoordinates(id).Y,
                                           Model.IsPlace(id) ? (int?)Model.GetTokenCount(id) : null);
                    ElementProvider.RemoveNode(id);
                    ElementProvider.RemoveNameField(id);
                    Model.RemoveNode(id);
                }
            }
            ReevaluateCommandState(this, new EventArgs());
            UndoManager.PushDeleteUndo();
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Undoes a create operation.
        /// </summary>
        /// <param name="id">The id of the element that was created.</param>
        /// <param name="viewSize">The view size before the element was created.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction.</param>
        public void UndoCreate(String id, Point viewSize, Point shift)
        {
            UndoManager.AddCreateRedoParams(ViewWidth, ViewHeight, shift.X, shift.Y);
            // undo create
            if (Model.IsNode(id))
            {
                UndoManager.AddCreateRedoNodeInfo(id, Model.GetName(id),
                                                 (int)Model.GetCoordinates(id).X, (int)Model.GetCoordinates(id).Y,
                                                 Model.IsPlace(id) ? (int?)Model.GetTokenCount(id) : null);
                ElementProvider.RemoveNode(id);
                ElementProvider.RemoveNameField(id);
                Model.RemoveNode(id);
            }
            else
            {
                String[] nodes = Model.GetNodesForArc(id);
                String sourceId = Model.IsSource(id, nodes[0]) ? nodes[0] : nodes[1];
                String targetId = Model.IsSource(id, nodes[0]) ? nodes[1] : nodes[0];
                UndoManager.AddCreateRedoArcInfo(id, sourceId, targetId, false);
                UndoManager.AddCreateRedoParams(ViewWidth, ViewHeight, shift.X, shift.Y);
                ElementManager.RemoveBrother(id);
                ElementProvider.RemoveArc(id);
                Model.RemoveArc(id);
            }
            // undo shift
            if (shift.X != 0)
                ShiftAllLeft(-shift.X);
            if (shift.Y != 0)
                ShiftAllUp(-shift.Y);
            ViewSizeChanged(this, new ViewSizeChangedEventArgs(viewSize.X, viewSize.Y));
            ReevaluateCommandState(this, new EventArgs());
            UndoManager.PushCreateRedo();
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Redoes a create operation.
        /// </summary>
        /// <param name="node">Node info about the node that is to be restored.</param>
        /// <param name="arc">Arc info about the arc that is to be restored.</param>
        /// <param name="viewSize">The view size before the element was removed.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction.</param>
        public void RedoCreate(NodeInfo node, ArcInfo arc, Point viewSize, Point shift)
        {
            // redo create
            if (node != null)
            {
                UndoManager.AddCreateUndoId(node.Id);
                UndoManager.AddCreateUndoViewSize(ViewWidth, ViewHeight);
                UndoManager.CreateRightShift = shift.X;
                UndoManager.CreateDownShift = shift.Y;
                if (node.TokenCount != null)
                {
                    ElementCreator.CreatePlace(new Point(node.X, node.Y), node.Id);
                    ElementProvider.GetNode(node.Id).SetTokens((int)node.TokenCount);
                }
                else
                    ElementCreator.CreateTrans(new Point(node.X, node.Y), node.Id);
                if (node.Name != null)
                {
                    INameField nField = ElementProvider.GetNameField(node.Id);
                    nField.DrawSize = ElementManager.DrawSize;
                    nField.SetName(node.Name);
                }
            }
            if (arc != null)
            {
                UndoManager.AddCreateUndoId(arc.Id);
                UndoManager.AddCreateUndoViewSize(ViewWidth, ViewHeight);
                ElementCreator.CreateArc(arc.Id, arc.SourceId, arc.TargetId, arc.Selected);
            }
            // redo shift
            if (shift.X > 0 || shift.Y > 0)
            {
                for (int i = 0; i < ElementProvider.NodesCount; i++)
                {
                    IVisualNode vNode = ElementProvider.GetNode(i);
                    if (!vNode.Id.Equals(node.Id))
                    {
                        vNode.XPos += shift.X;
                        vNode.YPos += shift.Y;
                        ElementProvider.GetNameField(node.Id).AdjustNameField();
                        Model.ChangePosition(vNode.Id, (int)vNode.XPos, (int)vNode.YPos);
                    }
                } 
            }
            ViewSizeChanged(this, new ViewSizeChangedEventArgs(viewSize.X, viewSize.Y));
            ReevaluateCommandState(this, new EventArgs());
            UndoManager.PushCreateUndo();
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Undoes a token change operation.
        /// </summary>
        /// <param name="id">The id of the node for which the token count has been changed.</param>
        /// <param name="tokenCount">The previous token count of the node.</param>
        public void UndoTokenChange(String id, int tokenCount)
        {
            UndoManager.AddTokenChangeRedoParams(id, Model.GetTokenCount(id));
            ElementProvider.GetNode(id).SetTokens(tokenCount);
            UndoManager.PushTokenChangeRedo();
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Redoes a token change operation.
        /// </summary>
        /// <param name="id">The id of the node for which the token count has been changed.</param>
        /// <param name="tokenCount">The previous token count of the node.</param>
        public void RedoTokenChange(String id, int tokenCount)
        {
            UndoManager.AddTokenChangeUndoParams(id, Model.GetTokenCount(id));
            ElementProvider.GetNode(id).SetTokens(tokenCount);
            UndoManager.PushTokenChangeUndo();
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Undoes a transition operation.
        /// </summary>
        /// <param name="id">The id of the node on which the transition has been performed.</param>
        public void UndoTransition(String id)
        {
            UndoManager.AddTransitionRedoId(id);
            Model.InverseTransition(id);
            UndoManager.PushTransitionRedo();
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Redoes a transition operation.
        /// </summary>
        /// <param name="id">The id of the node on which the transition has been performed.</param>
        public void RedoTransition(String id)
        {
            UndoManager.AddTransitionUndoId(id);
            Model.PerformTransition(id);
            UndoManager.PushTransitionUndo();
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Undoes a size change operation.
        /// </summary>
        /// <param name="sizeFactor">The size factor before the change.</param>
        /// <param name="viewSize">The view size before the change.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction.</param>
        public void UndoSizeChange(int sizeFactor, Point viewSize, Point shift)
        {
            if (sizeFactor > SizeFactor)
                UndoManager.AddSizeChangeRedoParams(SizeFactor, ViewWidth, ViewHeight, shift.X, shift.Y);
            else
                UndoManager.AddSizeChangeRedoParams(SizeFactor, ViewWidth, ViewHeight, 0, 0);
            SizeFactor = sizeFactor;
            SizeFactorChanged(this, new SizeFactorChangedEventArgs(SizeFactor));
            if (shift.X != 0)
                ShiftAllLeft(-shift.X);
            if (shift.Y != 0)
                ShiftAllUp(-shift.Y);
            ViewSizeChanged(this, new ViewSizeChangedEventArgs(viewSize.X, viewSize.Y));
            UndoManager.PushSizeChangeRedo();
            Modified(this, new EventArgs());
        }

        /// <summary>
        /// Redoes a size change operation.
        /// </summary>
        /// <param name="sizeFactor">The size factor before the change.</param>
        /// <param name="viewSize">The view size before the change.</param>
        /// <param name="shift">The amount by which elements had to be shifted in x- and y-direction.</param>
        public void RedoSizeChange(int sizeFactor, Point viewSize, Point shift)
        {
            UndoManager.AddSizeChangeUndoParams(SizeFactor, ViewWidth, ViewHeight);
            if (sizeFactor > SizeFactor)
            {
                UndoManager.SizeChangeRightShift = shift.X;
                UndoManager.SizeChangeDownShift = shift.Y; 
            }
            SizeFactor = sizeFactor;
            SizeFactorChanged(this, new SizeFactorChangedEventArgs(SizeFactor));
            SizeFactor = sizeFactor;
            if (shift.X != 0)
                ShiftAllLeft(-shift.X);
            if (shift.Y != 0)
                ShiftAllUp(-shift.Y);
            ViewSizeChanged(this, new ViewSizeChangedEventArgs(viewSize.X, viewSize.Y));
            SizeFactorChanged(this, new SizeFactorChangedEventArgs(SizeFactor));
            UndoManager.PushSizeChangeUndo();
            Modified(this, new EventArgs());
        }
        #endregion

        #endregion
    }
}
