using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetriNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetModelTests
{
    [TestClass]
    public class TransitionTests
    {
        private CallbackDelegates.TokensChanged testCallback;
        private int? callbackTokens;
        private IList<String> callbackIds;

        [TestInitialize]
        public void Initialize()
        {
            callbackTokens = null;
            callbackIds = new List<String>();
            testCallback = (s, i) => { callbackTokens = i; callbackIds.Add(s); };
        }

        [TestMethod]
        public void TransitionTokens_NoPredecessorsNoSuccessors_NoCallback()
        {
            // arrange
            Transition testTrans = new Transition(0, 0, "testTransition");
            testTrans.Predecessors.Clear();
            testTrans.Successors.Clear();

            // act
            testTrans.TransitionTokens((s, b) => { }, testCallback);

            // assert
            Assert.AreEqual(0, callbackIds.Count, "No callback should occur when there are no successors or predecessors.");
        }

        [TestMethod]
        public void TransitionTokens_NoPredecessorsTwoSuccessors_CallbackTwice()
        {
            // arrange
            Transition testTrans = new Transition(0, 0, "testTransition");
            testTrans.Predecessors.Clear();
            IList<String> successorIds = new List<String> { "successor_1", "successor_2" };
            int tokenCount = new Random().Next(0, 999);
            StubPlace successor_1 = new StubPlace(successorIds[0], new List<INode> { testTrans }, new List<INode> { });
            successor_1.TokenCount = tokenCount;
            StubPlace successor_2 = new StubPlace(successorIds[1], new List<INode> { testTrans }, new List<INode> { });
            successor_2.TokenCount = tokenCount;
            testTrans.Successors.Clear();
            testTrans.Successors.Add(successor_1);
            testTrans.Successors.Add(successor_2);

            // act
            testTrans.TransitionTokens((s, b) => { }, testCallback);

            // assert
            Assert.IsTrue(Compare.UnorderedEqual(successorIds, callbackIds), "Callback called with incorrect ids.");
            Assert.AreEqual(tokenCount + 1, successor_2.TokenCount, "TokenCount not increased correctly.");
        }

        [TestMethod]
        public void TransitionTokens_TwoPredecessorsNonZeroTokensNoSuccessors_CallbackTwice()
        {
            // arrange
            Transition testTrans = new Transition(0, 0, "testTransition");
            testTrans.Successors.Clear();
            IList<String> predecessorIds = new List<String> { "predecessor_1", "predecessor_2" };
            int tokenCount = new Random().Next(1, 999);
            StubPlace predecessor_1 = new StubPlace(predecessorIds[0], new List<INode> { }, new List<INode> { testTrans });
            predecessor_1.TokenCount = tokenCount;
            StubPlace predecessor_2 = new StubPlace(predecessorIds[1], new List<INode> { }, new List<INode> { testTrans });
            predecessor_2.TokenCount = tokenCount;
            testTrans.Predecessors.Clear();
            testTrans.Predecessors.Add(predecessor_1);
            testTrans.Predecessors.Add(predecessor_2);

            // act
            testTrans.TransitionTokens((s, b) => { }, testCallback);

            // assert
            Assert.IsTrue(Compare.UnorderedEqual(predecessorIds, callbackIds), "Callback called with incorrect ids.");
            Assert.AreEqual(tokenCount - 1, predecessor_2.TokenCount, "TokenCount not reduced correctly.");
        }

        [TestMethod]
        public void TransitionTokens_OnePredecessorNonZeroTokensOneSuccessor_CallbackTwice()
        {
            // arrange
            Transition testTrans = new Transition(0, 0, "testTransition");
            IList<String> predSuccIds = new List<String> { "predecessor_1", "successor_1" };
            int tokenCount = new Random().Next(1, 999);
            StubPlace predecessor_1 = new StubPlace(predSuccIds[0], new List<INode> { }, new List<INode> { testTrans });
            predecessor_1.TokenCount = tokenCount;
            testTrans.Predecessors.Clear();
            testTrans.Predecessors.Add(predecessor_1);
            StubPlace successor_1 = new StubPlace(predSuccIds[1], new List<INode> { testTrans }, new List<INode> { });
            successor_1.TokenCount = tokenCount;
            testTrans.Successors.Clear();
            testTrans.Successors.Add(successor_1);

            // act
            testTrans.TransitionTokens((s, b) => { }, testCallback);

            // assert
            Assert.IsTrue(Compare.UnorderedEqual(predSuccIds, callbackIds), "Callback called with incorrect ids.");
            Assert.AreEqual(tokenCount - 1, predecessor_1.TokenCount, "TokenCount not reduced correctly.");
            Assert.AreEqual(tokenCount + 1, successor_1.TokenCount, "TokenCount not increased correctly.");
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void TransitionTokens_OnePredecessorZeroTokens_ThrowsInvalidOperationException()
        {
            // arrange
            Transition testTrans = new Transition(0, 0, "testTransition");
            testTrans.Successors.Clear();
            IList<String> predecessorIds = new List<String> { "predecessor_1" };
            StubPlace predecessor_1 = new StubPlace(predecessorIds[0], new List<INode> { }, new List<INode> { testTrans });
            predecessor_1.TokenCount = 0;
            testTrans.Predecessors.Clear();
            testTrans.Predecessors.Add(predecessor_1);

            // act
            testTrans.TransitionTokens((s, b) => { }, testCallback);
        }
        

        [TestMethod]
        public void InverseTransitionTokens_NoSucessorsNoPredecessors_NoCallback()
        {
            // arrange
            Transition testTrans = new Transition(0, 0, "testTransition");
            testTrans.Predecessors.Clear();
            testTrans.Successors.Clear();

            // act
            testTrans.InverseTransitionTokens((s, b) => { }, testCallback);

            // assert
            Assert.AreEqual(0, callbackIds.Count, "No callback should occur when there are no successors or predecessors.");
        }

        [TestMethod]
        public void InverseTransitionTokens_NoSuccessorsTwoPredecessors_CallbackTwice()
        {
            // arrange
            Transition testTrans = new Transition(0, 0, "testTransition");
            testTrans.Successors.Clear();
            IList<String> predecessorIds = new List<String> { "predecessor_1", "predecessor_2" };
            int tokenCount = new Random().Next(0, 999);
            StubPlace predecessor_1 = new StubPlace(predecessorIds[0], new List<INode> { testTrans }, new List<INode> { });
            predecessor_1.TokenCount = tokenCount;
            StubPlace predecessor_2 = new StubPlace(predecessorIds[1], new List<INode> { testTrans }, new List<INode> { });
            predecessor_2.TokenCount = tokenCount;
            testTrans.Predecessors.Clear();
            testTrans.Predecessors.Add(predecessor_1);
            testTrans.Predecessors.Add(predecessor_2);

            // act
            testTrans.InverseTransitionTokens((s, b) => { }, testCallback);

            // assert
            Assert.IsTrue(Compare.UnorderedEqual(predecessorIds, callbackIds), "Callback called with incorrect ids.");
            Assert.AreEqual(tokenCount + 1, predecessor_2.TokenCount, "TokenCount not increased correctly.");
        }

        [TestMethod]
        public void InverseTransitionTokens_TwoSuccessorsNonZeroTokensNoPredecessors_CallbackTwice()
        {
            // arrange
            Transition testTrans = new Transition(0, 0, "testTransition");
            testTrans.Predecessors.Clear();
            IList<String> successorIds = new List<String> { "successor_1", "successor_2" };
            int tokenCount = new Random().Next(1, 999);
            StubPlace successor_1 = new StubPlace(successorIds[0], new List<INode> { }, new List<INode> { testTrans });
            successor_1.TokenCount = tokenCount;
            StubPlace successor_2 = new StubPlace(successorIds[1], new List<INode> { }, new List<INode> { testTrans });
            successor_2.TokenCount = tokenCount;
            testTrans.Successors.Clear();
            testTrans.Successors.Add(successor_1);
            testTrans.Successors.Add(successor_2);

            // act
            testTrans.InverseTransitionTokens((s, b) => { }, testCallback);

            // assert
            Assert.IsTrue(Compare.UnorderedEqual(successorIds, callbackIds), "Callback called with incorrect ids.");
            Assert.AreEqual(tokenCount - 1, successor_2.TokenCount, "TokenCount not reduced correctly.");
        }

        [TestMethod]
        public void InverseTransitionTokens_OneSuccessorNonZeroTokensOnePredecessor_CallbackTwice()
        {
            // arrange
            Transition testTrans = new Transition(0, 0, "testTransition");
            IList<String> succPredIds = new List<String> { "successor_1", "predecessor_1" };
            int tokenCount = new Random().Next(1, 999);
            StubPlace successor_1 = new StubPlace(succPredIds[0], new List<INode> { }, new List<INode> { testTrans });
            successor_1.TokenCount = tokenCount;
            testTrans.Successors.Clear();
            testTrans.Successors.Add(successor_1);
            StubPlace predecessor_1 = new StubPlace(succPredIds[1], new List<INode> { testTrans }, new List<INode> { });
            predecessor_1.TokenCount = tokenCount;
            testTrans.Predecessors.Clear();
            testTrans.Predecessors.Add(predecessor_1);

            // act
            testTrans.InverseTransitionTokens((s, b) => { }, testCallback);

            // assert
            Assert.IsTrue(Compare.UnorderedEqual(succPredIds, callbackIds), "Callback called with incorrect ids.");
            Assert.AreEqual(tokenCount - 1, successor_1.TokenCount, "TokenCount not reduced correctly.");
            Assert.AreEqual(tokenCount + 1, predecessor_1.TokenCount, "TokenCount not increased correctly.");
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void TransitionTokens_OneSuccessorZeroTokens_ThrowsInvalidOperationException()
        {
            // arrange
            Transition testTrans = new Transition(0, 0, "testTransition");
            testTrans.Predecessors.Clear();
            IList<String> successorIds = new List<String> { "successor_1" };
            StubPlace successor_1 = new StubPlace(successorIds[0], new List<INode> { }, new List<INode> { testTrans });
            successor_1.TokenCount = 0;
            testTrans.Successors.Clear();
            testTrans.Successors.Add(successor_1);

            // act
            testTrans.InverseTransitionTokens((s, b) => { }, testCallback);
        }
    }
}
