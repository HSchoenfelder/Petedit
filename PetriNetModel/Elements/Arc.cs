using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    /// <summary>
    /// This class represents an arc in the petrinet.
    /// </summary>
    internal class Arc : IArc
    {
        #region fields
        /// <summary>Store for the Id property. </summary>
        private readonly String _id;

        /// <summary>Store for the Source property. </summary>
        private INode _source;

        /// <summary>Store for the Target property. </summary>
        private INode _target;
        #endregion

        #region properties
        /// <summary>Gets the Id that uniquely identifies this Arc.</summary>
        public String Id
        {
            get { return _id; }
        }

        /// <summary>Gets or sets the source Node of this Arc.</summary>
        internal INode Source
        {
            get { return _source; }
            set { _source = value; }
        }

        /// <summary> Gets or sets the target Node of this Arc.</summary>
        internal INode Target
        {
            get { return _target; }
            set { _target = value; }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the Arc class with the specified source, target and id. 
        /// </summary>
        /// <param name="source">The source node of the new Arc.</param>
        /// <param name="target">The target node of the new Arc.</param>
        /// <param name="id">The id of the new Arc.</param>
        internal Arc(INode source, INode target, String id)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");
            if (id == null)
                throw new ArgumentNullException("id");
            if (source.GetType() == target.GetType())
                throw new InvalidOperationException("Invalid arc between nodes of same type!");
            _source = source;
            _target = target;
            _id = id;
        }
        #endregion

        #region methods
        /// <summary>
        /// Gets the id of the source of this Arc.
        /// </summary>
        /// <returns>The id of the source of this Arc.</returns>
        public String GetSourceId()
        {
            return Source.Id;
        }

        /// <summary>
        /// Gets the id of the target of this Arc.
        /// </summary>
        /// <returns>The id of the target of this Arc.</returns>
        public String GetTargetId()
        {
            return Target.Id;
        }

        /// <summary>
        /// Adds this Arc to the petrinet and sends notification if a state change is caused. 
        /// </summary>
        /// <param name="callback">The method to be called in case a state change is caused.</param>
        public void Add(CallbackDelegates.TransitionStateChanged callback)
        {
            Source.AddSuccessor(Target);
            Source.AddArc(this);
            Target.AddPredecessor(Source);
            Target.AddArc(this);

            if (Source is IPlace)
            {
                // call the provided method if the status of a transition is changed
                ITransition targetTransition = (ITransition)Target;
                if (((IPlace)Source).TokenCount == 0)
                {
                    bool deactivated = true;
                    foreach (INode sourcePlace in targetTransition.Predecessors)
                    {
                        if (((IPlace)sourcePlace).TokenCount == 0 && !sourcePlace.Equals(Source))
                            deactivated = false;
                    }
                    if (deactivated)
                    {
                        targetTransition.Enabled = false;
                        callback(Target.Id, false);
                    }
                }
            }
        }

        /// <summary>
        /// Removes this Arc from the petrinet and sends notification if a state change is caused.
        /// </summary>
        /// <param name="callback">The method to be called in case a state change is caused.</param>
        public void Remove(CallbackDelegates.TransitionStateChanged callback)
        {
            // remove the arc from the petrinet
            Source.RemoveSuccessor(Target);
            Source.RemoveArc(this); 
            Target.RemovePredecessor(Source);
            Target.RemoveArc(this);

            if (Target is ITransition)
            {
                // call the provided method if the status of a transition is changed
                ITransition targetTransition = (ITransition)Target;
                if (((IPlace)Source).TokenCount == 0)
                {
                    bool activated = true;
                    foreach (INode sourcePlace in targetTransition.Predecessors)
                    {
                        if (((IPlace)sourcePlace).TokenCount == 0)
                            activated = false;
                    }
                    if (activated)
                    {
                        targetTransition.Enabled = true;
                        callback(Target.Id, true);
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether the specified object is an Arc and whether it contains the same id as this Arc.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>true if obj is an Arc and contains the same id as this Arc; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj is IArc)
            {
                if ((obj as IArc).Id.Equals(Id))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the hash code for this Arc.
        /// </summary>
        /// <returns>The hash code for this Arc</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
