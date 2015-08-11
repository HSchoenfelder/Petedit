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
    public class PlaceTests
    {
        private CallbackDelegates.TransitionStateChanged testCallback;
        private bool? callbackValue;
        private IList<String> callbackIds;

        [TestInitialize]
        public void Initialize()
        {
            callbackValue = null;
            callbackIds = new List<String>();
            testCallback = (s, b) => { callbackValue = b; callbackIds.Add(s); };
        }

        [TestMethod]
        public void ChangeTokenCount_NoSuccessors_NoCallback()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");
            testPlace.Successors.Clear();
            int tokenCount = new Random().Next(0, 999);

            // act
            testPlace.ChangeTokenCount(tokenCount, testCallback);

            // assert
            Assert.AreEqual(testPlace.TokenCount, tokenCount, "Incorrect tokenCount was set.");
            Assert.AreEqual(0, callbackIds.Count, "No callback should occur when there are no successors.");
        }

        [TestMethod]
        public void ChangeTokenCount_NonZeroTokensSingleActiveSuccessor_NoCallback()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");
            ITransition successor = new StubTransition("successor_1", new List<INode> { testPlace }, new List<INode> { });
            successor.Enabled = true;
            testPlace.Successors.Clear();
            testPlace.Successors.Add(successor);
            int tokenCount = new Random().Next(1, 999);

            // act
            testPlace.ChangeTokenCount(tokenCount, testCallback);

            // assert
            Assert.AreEqual(testPlace.TokenCount, tokenCount, "Incorrect tokenCount was set.");
            Assert.AreEqual(0, callbackIds.Count, "No callback should occur when successor is already active.");
        }

        [TestMethod]
        public void ChangeTokenCount_ZeroTokensSingleActiveSuccessor_CallbackFalse()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");
            IList<String> successorIds = new List<String> { "successor_1" };
            ITransition successor = new StubTransition(successorIds[0], new List<INode> { testPlace }, new List<INode> { });
            successor.Enabled = true;
            testPlace.Successors.Clear();
            testPlace.Successors.Add(successor);

            // act
            testPlace.ChangeTokenCount(0, testCallback);

            // assert
            Assert.AreEqual(testPlace.TokenCount, 0, "Incorrect tokenCount was set.");
            Assert.AreEqual(false, callbackValue, "Deactivation of Transitions required when successor " +
                                                  "is active and tokens are set to zero.");
            Assert.IsTrue(Compare.UnorderedEqual(successorIds, callbackIds), "Callback called with incorrect ids.");
        }

        [TestMethod]
        public void ChangeTokenCount_ZeroTokensTwoActiveSuccessorsOneInactive_CallbackFalseTwice()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");
            IList<String> successorIds = new List<String> { "successor_1", "successor_2" };
            ITransition successor_1 = new StubTransition(successorIds[0], new List<INode> { testPlace }, new List<INode> { });
            successor_1.Enabled = true;
            ITransition successor_2 = new StubTransition(successorIds[1], new List<INode> { testPlace }, new List<INode> { });
            successor_2.Enabled = true;
            ITransition successor_3 = new StubTransition("successor_3", new List<INode> { testPlace }, new List<INode> { });
            successor_3.Enabled = false;
            testPlace.Successors.Clear();
            testPlace.Successors.Add(successor_1);
            testPlace.Successors.Add(successor_2);
            testPlace.Successors.Add(successor_3);

            // act
            testPlace.ChangeTokenCount(0, testCallback);

            // assert
            Assert.AreEqual(testPlace.TokenCount, 0, "Incorrect tokenCount was set.");
            Assert.AreEqual(false, callbackValue, "Deactivation of Transitions required when successor " +
                                                  "is active and tokens are set to zero.");
            Assert.IsTrue(Compare.UnorderedEqual(successorIds, callbackIds), "Callback called with incorrect ids.");
        }

        [TestMethod]
        public void ChangeTokenCount_NonZeroTokensSingleInactiveSuccessorOneOtherPredecessorZeroTokens_CallbackFalse()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");
            StubPlace otherPredecessor = new StubPlace("otherPredecessor", new List<INode> { }, new List<INode> { });
            otherPredecessor.TokenCount = 0;
            IList<String> successorIds = new List<String> { "successor_1" };
            ITransition successor_1 = new StubTransition(successorIds[0], new List<INode> { testPlace, otherPredecessor }, new List<INode> { });
            successor_1.Enabled = false;
            testPlace.Successors.Clear();
            testPlace.Successors.Add(successor_1);
            int tokenCount = new Random().Next(1, 999);

            // act
            testPlace.ChangeTokenCount(tokenCount, testCallback);

            // assert
            Assert.AreEqual(testPlace.TokenCount, tokenCount, "Incorrect tokenCount was set.");
            Assert.AreEqual(0, callbackIds.Count, "No callback should occur when successor has other predecessor with zero tokens.");
        }

        [TestMethod]
        public void ChangeTokenCount_NonZeroTokensSingleInactiveSuccessorNoOtherPredecessors_CallbackTrue()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");
            IList<String> successorIds = new List<String> { "successor_1" };
            ITransition successor_1 = new StubTransition(successorIds[0], new List<INode> { testPlace }, new List<INode> { });
            successor_1.Enabled = false;
            testPlace.Successors.Clear();
            testPlace.Successors.Add(successor_1);
            int tokenCount = new Random().Next(1, 999);

            // act
            testPlace.ChangeTokenCount(tokenCount, testCallback);

            // assert
            Assert.AreEqual(testPlace.TokenCount, tokenCount, "Incorrect tokenCount was set.");
            Assert.AreEqual(true, callbackValue, "Activation of Transition required when successor is inactive, has no " +
                                                  "other predecessors and tokens are set to non-zero.");
            Assert.IsTrue(Compare.UnorderedEqual(successorIds, callbackIds), "Callback called with incorrect ids.");
        }

        [TestMethod]
        public void ChangeTokenCount_NonZeroTokensSingleInactiveSuccessorOneOtherPredecessorNonZeroTokens_CallbackTrue()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");
            StubPlace otherPredecessor = new StubPlace("otherPredecessor", new List<INode> { }, new List<INode> { });
            otherPredecessor.TokenCount = new Random().Next(1, 999);
            IList<String> successorIds = new List<String> { "successor_1" };
            ITransition successor_1 = new StubTransition(successorIds[0], new List<INode> { testPlace, otherPredecessor }, new List<INode> { });
            successor_1.Enabled = false;
            testPlace.Successors.Clear();
            testPlace.Successors.Add(successor_1);
            int tokenCount = new Random().Next(1, 999);

            // act
            testPlace.ChangeTokenCount(tokenCount, testCallback);

            // assert
            Assert.AreEqual(testPlace.TokenCount, tokenCount, "Incorrect tokenCount was set.");
            Assert.AreEqual(true, callbackValue, "Activation of Transition required when successor is inactive, other predecessors " +
                                                 "have non-zero tokens and tokens are set to non-zero.");
            Assert.IsTrue(Compare.UnorderedEqual(successorIds, callbackIds), "Callback called with incorrect ids.");
        }

        [TestMethod]
        public void ChangeTokenCount_NonZeroTokensThreeInactiveSuccessorsOneActive_CallbackTrueFalseTwice()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");
            IList<String> successorIds = new List<String> { "successor_1", "successor_2" };
            ITransition successor_1 = new StubTransition(successorIds[0], new List<INode> { testPlace }, new List<INode> { });
            successor_1.Enabled = false;
            StubPlace nonZeroTokensPredecessor = new StubPlace("predecessor_1", new List<INode> { }, new List<INode> { });
            nonZeroTokensPredecessor.TokenCount = new Random().Next(1, 999);
            ITransition successor_2 = new StubTransition(successorIds[1], new List<INode> { testPlace, nonZeroTokensPredecessor }, new List<INode> { });
            successor_2.Enabled = false;
            StubPlace zeroTokensPredecessor = new StubPlace("predecessor_2", new List<INode> { }, new List<INode> { });
            zeroTokensPredecessor.TokenCount = 0;
            ITransition successor_3 = new StubTransition("successor_3", new List<INode> { testPlace, zeroTokensPredecessor }, new List<INode> { });
            successor_3.Enabled = false;
            ITransition successor_4 = new StubTransition("successor_4", new List<INode> { testPlace }, new List<INode> { });
            successor_4.Enabled = true;
            int tokenCount = new Random().Next(1, 999);

            testPlace.Successors.Clear();
            testPlace.Successors.Add(successor_1);
            testPlace.Successors.Add(successor_2);
            testPlace.Successors.Add(successor_3);
            testPlace.Successors.Add(successor_4);

            // act
            testPlace.ChangeTokenCount(tokenCount, testCallback);

            // assert
            Assert.AreEqual(testPlace.TokenCount, tokenCount, "Incorrect tokenCount was set.");
            Assert.AreEqual(true, callbackValue, "Activation of Transitions required when there are no other predecessors " +
                                                 "or only prececessors with non-zero tokens.");
            Assert.IsTrue(Compare.UnorderedEqual(successorIds, callbackIds), "Callback called with incorrect ids.");
        }

        [TestMethod]
        public void Equals_CompareToNull_ReturnsFalse()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");

            // act
            bool result = testPlace.Equals(null);

            // assert
            Assert.IsFalse(result, "Comparison with null should always return false.");
        }

        [TestMethod]
        public void Equals_CompareToString_ReturnsFalse()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");

            // act
            bool result = testPlace.Equals("testString");

            // assert
            Assert.IsFalse(result, "Comparison with other type should always return false.");
        }

        [TestMethod]
        public void Equals_CompareToDifferentArc_ReturnsFalse()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");
            Place differentPlace = new Place(0, 0, "differentPlace");

            // act
            bool result = testPlace.Equals(differentPlace);

            // assert
            Assert.IsFalse(result, "Comparison with place with different id should return false.");
        }

        [TestMethod]
        public void Equals_CompareToSamePlace_ReturnsTrue()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");

            // act
            bool result = testPlace.Equals(testPlace);

            // assert
            Assert.IsTrue(result, "Comparison with same place should return true.");
        }

        [TestMethod]
        public void Equals_CompareToPlaceWithSameIdButDifferentTokenCount_ReturnsFalse()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");
            Place differentPlace = new Place(0, 0, "testPlace");
            differentPlace.ChangeTokenCount(5, testCallback);

            // act
            bool result = testPlace.Equals(differentPlace);

            // assert
            Assert.IsFalse(result, "Comparison with place with different tokenCount should return false.");
        }

        [TestMethod]
        public void Equals_CompareToPlaceWithSameIdAndTokenCount_ReturnsTrue()
        {
            // arrange
            Place testPlace = new Place(0, 0, "testPlace");
            testPlace.ChangeTokenCount(4, testCallback);
            Place differentPlace = new Place(0, 0, "testPlace");
            differentPlace.ChangeTokenCount(4, testCallback);

            // act
            bool result = testPlace.Equals(differentPlace);

            // assert
            Assert.IsTrue(result, "Comparison with place with same id and same tokenCount should return true.");
        }
    }
}
