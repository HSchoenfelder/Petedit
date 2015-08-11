using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModelTests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NodeConstructor_NegativeXCoordinate_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            int x = new Random().Next(int.MinValue, -1);
            int y = new Random().Next(0, 999);

            // act
            ConcreteNode testNode = new ConcreteNode(x, y, "testNode");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NodeConstructor_NegativeYCoordinate_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            int x = new Random().Next(0, 999);
            int y = new Random().Next(int.MinValue, -1);

            // act
            ConcreteNode testNode = new ConcreteNode(x, y, "testNode");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NodeConstructor_IdNull_ThrowsArgumentNullException()
        {
            // arrange
            int x = new Random().Next(0, 999);
            int y = new Random().Next(0, 999);

            // act
            ConcreteNode testNode = new ConcreteNode(x, y, null);
        }

        [TestMethod]
        public void NodeConstructor_AllLegalParameters_ConstructsValidNode()
        {
            // arrange
            int x = new Random().Next(0, 999);
            int y = new Random().Next(0, 999);

            // act
            ConcreteNode testNode = new ConcreteNode(x, y, "testNode");

            // assert
            Assert.IsNotNull(testNode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddArc_AddedArcNull_ThrowsArgumentNullException()
        {
            // arrange
            ConcreteNode testNode = new ConcreteNode(0, 0, "testNode");

            // act
            testNode.AddArc(null);
        }

        [TestMethod]
        public void AddArc_ArcAddedFirstTime_ArcAddedToArcs()
        {
            // arrange
            ConcreteNode testNode = new ConcreteNode(0, 0, "testNode");
            StubArc addedArc = new StubArc("arc");

            // act
            testNode.AddArc(addedArc);

            // assert
            Assert.IsTrue(testNode.Arcs.Contains(addedArc));
        }

        [TestMethod]
        public void AddArc_ArcAddedTwice_SecondArcNotAddedToArcs()
        {
            // arrange
            ConcreteNode testNode = new ConcreteNode(0, 0, "testNode");
            StubArc addedArc = new StubArc("arc");

            // act
            testNode.AddArc(addedArc);
            testNode.AddArc(addedArc);

            // assert
            Assert.AreEqual(1, testNode.Arcs.Where(arc => arc.Equals(addedArc)).Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddPredecessor_AddedPredecessorNull_ThrowsArgumentNullException()
        {
            // arrange
            ConcreteNode testNode = new ConcreteNode(0, 0, "testNode");

            // act
            testNode.AddPredecessor(null);
        }

        [TestMethod]
        public void AddPredecessor_PredecessorAddedFirstTime_PredecessorAddedToPredecessors()
        {
            // arrange
            ConcreteNode testNode = new ConcreteNode(0, 0, "testNode");
            StubNode predecessor = new StubNode();

            // act
            testNode.AddPredecessor(predecessor);

            // assert
            Assert.IsTrue(testNode.Predecessors.Contains(predecessor));
        }

        [TestMethod]
        public void AddPredecessor_PredecessorAddedTwice_SecondPredecessorNotAddedToPredecessors()
        {
            // arrange
            ConcreteNode testNode = new ConcreteNode(0, 0, "testNode");
            StubNode predecessor = new StubNode();

            // act
            testNode.AddPredecessor(predecessor);
            testNode.AddPredecessor(predecessor);

            // assert
            Assert.AreEqual(1, testNode.Predecessors.Where(pred => pred.Equals(predecessor)).Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddSuccessor_AddedSuccessorNull_ThrowsArgumentNullException()
        {
            // arrange
            ConcreteNode testNode = new ConcreteNode(0, 0, "testNode");

            // act
            testNode.AddSuccessor(null);
        }

        [TestMethod]
        public void AddSuccessor_SuccessorAddedFirstTime_SuccessorAddedToSuccessors()
        {
            // arrange
            ConcreteNode testNode = new ConcreteNode(0, 0, "testNode");
            StubNode successor = new StubNode();

            // act
            testNode.AddSuccessor(successor);

            // assert
            Assert.IsTrue(testNode.Successors.Contains(successor));
        }

        [TestMethod]
        public void AddSuccessor_SuccessorAddedTwice_SecondSuccessorNotAddedToSuccessors()
        {
            // arrange
            ConcreteNode testNode = new ConcreteNode(0, 0, "testNode");
            StubNode successor = new StubNode();

            // act
            testNode.AddSuccessor(successor);
            testNode.AddSuccessor(successor);

            // assert
            Assert.AreEqual(1, testNode.Successors.Where(pred => pred.Equals(successor)).Count());
        }
    }
}
