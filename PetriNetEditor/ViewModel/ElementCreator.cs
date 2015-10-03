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
    /// This class provides logic for the creation of visual elements in the petrinet.
    /// </summary>
    public class ElementCreator : IElementCreator
    {
        #region fields
        /// <summary> Store for the ElementProvider property. </summary>
        private IElementProvider _elementProvider;

        /// <summary> Store for the SelectionManager property. </summary>
        private ISelectionManager _selectionManager;

        /// <summary> Store for the ElementManager property. </summary>
        private ElementManager _elementManager;

        /// <summary> Store for the Model property. </summary>
        private IModel _model;

        /// <summary> Store for the DrawSize property. </summary>
        private int _drawSize;

        /// <summary> Store for the ArrowheadSize property. </summary>
        private int _arrowheadSize;
        #endregion

        #region properties

        #region private
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

        /// <summary> Gets the element manager that enables arc draw and move operations for nodes. </summary>
        private ElementManager ElementManager
        {
            get { return _elementManager; }
        }

        /// <summary> Gets the Model that allows for manipulation of the petrinet. </summary>
        private IModel Model
        {
            get { return _model; }
        }
        #endregion

        #region public
        /// <summary> Gets or sets the current global draw size. </summary>
        public int DrawSize
        {
            get { return _drawSize; }
            set { _drawSize = value; }
        }

        /// <summary> Gets or sets the current global arrowhead size. </summary>
        public int ArrowheadSize
        {
            get { return _arrowheadSize; }
            set { _arrowheadSize = value; }
        }
        #endregion

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new ElementCreator with the specified references and the specified initial drawsize and
        /// arrowheadSize.
        /// </summary>
        /// <param name="elementProvider">Reference to the element provider.</param>
        /// <param name="selectionManager">Reference to the selection manager.</param>
        /// <param name="undoManager">Reference to the undo manager.</param>
        /// <param name="elementManager">Reference to the element manager.</param>
        /// <param name="model">Reference to the model of the petrinet.</param>
        /// <param name="drawSize">The initial drawsize.</param>
        /// <param name="arrowheadSize">The initial arrowhead size.</param>
        public ElementCreator(IElementProvider elementProvider, ISelectionManager selectionManager, 
                              ElementManager elementManager, IModel model, int drawSize, int arrowheadSize)
        {
            _elementProvider = elementProvider;
            _selectionManager = selectionManager;
            _elementManager = elementManager;
            _model = model;
            _drawSize = drawSize;
            _arrowheadSize = arrowheadSize;
        }
        #endregion

        #region methods
        /// <summary>
        /// Creates a new visual node with the specified id, adds it to the presentation and adds it to the model 
        /// as a place. Adjusts the size of the presentation area and scrolls according to the new visual nodes 
        /// position.
        /// </summary>
        /// <param name="pos">The position on which the new place is to be created.</param>
        /// <param name="id">The id of the place to be created.</param>
        /// <returns>The id of the created place.</returns>
        public String CreatePlace(Point pos, String id)
        {
            if (id == null)
                id = Guid.NewGuid().ToString();
            Model.AddPlace((int)pos.X, (int)pos.Y, id);
            VisualNode newPlace = new VisualNode(pos.X, pos.Y, id, DrawSize, NodeType.Place, SelectionManager, 
                                                 ElementManager, Model);
            ElementProvider.AddNode(newPlace);
            ElementProvider.AddNameField(new NameField(id, ElementManager, Model, newPlace));
            ElementManager.AdjustViewSize(id, UndoRedoOps.Create);
            newPlace.IsBeyondEdge = true;
            return id;
        }

        /// <summary>
        /// Creates a new visual node, adds it to the presentation and adds it to the model as a place.
        /// Adjusts the size of the presentation area and scrolls according to the new visual nodes position.
        /// </summary>
        /// <param name="pos">The position on which the new place is to be created.</param>
        /// <returns>The id of the created transition.</returns>
        public String CreatePlace(Point pos)
        {
            return CreatePlace(pos, null);
        }

        /// <summary>
        /// Creates a new visual node with the specified id, adds it to the presentation and adds it to the model 
        /// as a transition. Adjusts the size of the presentation area and scrolls according to the new visual 
        /// nodes position.
        /// </summary>
        /// <param name="pos">The position on which the new transition is to be created.</param>
        /// <param name="id">The id of the transition to be created.</param>
        /// /// <returns>The id of the created transition.</returns>
        public String CreateTrans(Point pos, String id)
        {
            if (id == null)
                id = Guid.NewGuid().ToString();
            Model.AddTransition((int)pos.X, (int)pos.Y, id);
            VisualNode newTrans = new VisualNode(pos.X, pos.Y, id, DrawSize, NodeType.Transition, SelectionManager, 
                                                 ElementManager, Model);
            ElementProvider.AddNode(newTrans);
            //if (!ElementProvider.NameFieldExists(id))
                ElementProvider.AddNameField(new NameField(id, ElementManager, Model, newTrans)); 
            ElementManager.AdjustViewSize(id, UndoRedoOps.Create);
            newTrans.IsBeyondEdge = true;
            return id;
        }

        /// <summary>
        /// Creates a new visual node, adds it to the presentation and adds it to the model as a transition.
        /// Adjusts the size of the presentation area and scrolls according to the new visual nodes position.
        /// </summary>
        /// <param name="pos">The position on which the new transition is to be created.</param>
        /// <returns>The id of the created place.</returns>
        public String CreateTrans(Point pos)
        {
            return CreateTrans(pos, null);
        }

        /// <summary>
        /// Creates a new visual arc, adds it to the presentation and adds it to the model as an arc.
        /// </summary>
        /// <param name="id"> The id of the arc to be created. </param>
        /// <param name="sourceId"> The id of the source node of the arc. </param>
        /// <param name="targetId"> The id of the target node of the arc. </param>
        /// <param name="selected"> This value indicates whether the arc is to be created in selected state. </param>
        public void CreateArc(String id, String sourceId, String targetId, bool selected)
        {
            VisualArc arc = new VisualArc(DrawSize, ArrowheadSize, ElementManager, Model);
            Model.AddArc(sourceId, targetId, id);
            arc.SourceType = Model.IsPlace(sourceId) ? NodeType.Place : NodeType.Transition;
            arc.PositionArc(Model.GetCoordinates(sourceId), Model.GetCoordinates(targetId));
            arc.Id = id;
            ElementProvider.AddArc(arc);
            ElementManager.TryCreateBrother(id);
            arc.IsValid = true;
            arc.Visible = true;
            SelectionManager.UpdateAutoSelection(id);
            if (selected)
                SelectionManager.SelectElement(id);
        }

        /// <summary>
        /// Sets a name for the node with the specified id and updates the model with the new name.
        /// </summary>
        /// <param name="id">The id of the node for which to set the name.</param>
        /// <param name="name">The new name of the node.</param>
        public void SetNodeName(String id, String name)
        {
            ElementProvider.GetNameField(id).SetName(name);
        }

        /// <summary>
        /// Sets a token value for the place with the specified id and updates the model with the n
        /// ew value.
        /// </summary>
        /// <param name="id">The id of the node for which to set the token value.</param>
        /// <param name="tokens">The new token value.</param>
        public void SetPlaceTokens(String id, int tokens)
        {
            ElementProvider.GetNode(id).SetTokens(tokens);
        }
        #endregion
    }
}
