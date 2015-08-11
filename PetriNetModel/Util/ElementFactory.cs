using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModel
{
    internal class ElementFactory
    {
        private static IPlace _customPlace = null;
        private static ITransition _customTransition = null;
        private static IArc _customArc = null;

        public static IPlace CreatePlace(int xCoordinate, int yCoordinate, String id)
        {
            if (_customPlace != null)
                return _customPlace;
            return new Place(xCoordinate, yCoordinate, id);
        }

        public static ITransition CreateTransition(int xCoordinate, int yCoordinate, String id)
        {
            if (_customTransition != null)
                return _customTransition;
            return new Transition(xCoordinate, yCoordinate, id);
        }

        public static IArc CreateArc(INode source, INode target, String id)
        {
            if (_customArc != null)
                return _customArc;
            return new Arc(source, target, id);
        }

        public static void SetCustomPlace(IPlace place)
        {
            _customPlace = place;
        }

        public static void SetCustomTransition(ITransition transition)
        {
            _customTransition = transition;
        }

        public static void SetCustomArc(IArc arc)
        {
            _customArc = arc;
        }

        public static void ResetFactory()
        {
            _customPlace = null;
            _customTransition = null;
            _customArc = null;
        }
    }
}
