using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetriNetModel;
using System.Collections.Generic;

namespace PetriNetModelTests
{
    [TestClass]
    public class ArcTests
    {
        private CallbackDelegates.TransitionStateChanged testCallback;
        private bool? callbackCalled;
        private String callbackId;

        [TestInitialize]
        public void Initialize()
        {
            callbackCalled = null;
            testCallback = (s, b) => { callbackCalled = b; callbackId = s; }; 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArcConstructor_SourceNull_ThrowsArgumentNullException()
        {
            // arrange
            StubPlace targetPlace = new StubPlace("targetPlace", new List<INode> { }, new List<INode> { });

            // act
            Arc testArc = new Arc(null, targetPlace, "testArc");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArcConstructor_TargetNull_ThrowsArgumentNullException()
        {
            // arrange
            StubPlace sourcePlace = new StubPlace("sourcePlace", new List<INode> { }, new List<INode> { });

            // act
            Arc testArc = new Arc(sourcePlace, null, "testArc");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArcConstructor_IdNull_ThrowsArgumentNullException()
        {
            // arrange
            StubPlace sourcePlace = new StubPlace("sourcePlace", new List<INode> { }, new List<INode> { });
            StubTransition targetTrans = new StubTransition("targetTrans", new List<INode> { }, new List<INode> { });

            // act
            Arc testArc = new Arc(sourcePlace, targetTrans, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ArcConstructor_SourcePlaceTargetPlace_ThrowsInvalidOperationException()
        {
            // arrange
            StubPlace sourcePlace = new StubPlace("sourcePlace", new List<INode> { }, new List<INode> { });
            StubPlace targetPlace = new StubPlace("targetPlace", new List<INode> { }, new List<INode> { });

            // act
            Arc testArc = new Arc(sourcePlace, targetPlace, "testArc");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ArcConstructor_SourceTransitionTargetTransition_ThrowsInvalidOperationException()
        {
            // arrange
            StubTransition sourceTrans = new StubTransition("sourceTrans", new List<INode> { }, new List<INode> { });
            StubTransition targetTrans = new StubTransition("targetTrans", new List<INode> { }, new List<INode> { });

            // act
            Arc testArc = new Arc(sourceTrans, targetTrans, "testArc");
        }

        [TestMethod]
        public void ArcConstructor_AllLegalParameters_ConstructsValidArc()
        {
            // arrange
            StubPlace sourcePlace = new StubPlace("sourcePlace", new List<INode> { }, new List<INode> { });
            StubTransition targetTrans = new StubTransition("targetTrans", new List<INode> { }, new List<INode> { });

            // act
            Arc testArc = new Arc(sourcePlace, targetTrans, "testArc");

            // assert
            Assert.IsNotNull(testArc);
        }

        [TestMethod]
        public void Add_SourceTransTargetPlace_NoCallback()
        {
            // arrange
            StubTransition source = new StubTransition("testSource", new List<INode> { }, new List<INode> { });
            StubPlace target = new StubPlace("testTarget", new List<INode> { }, new List<INode> { });
            Arc testArc = new Arc(source, target, "testArc");
            source.Arcs = new List<IArc> { testArc };
            target.Arcs = new List<IArc> { testArc };

            // act
            testArc.Add(testCallback);

            // assert
            Assert.IsNull(callbackCalled, "No callback should occur when source is Transition.");
        }

        [TestMethod]
        public void Add_SourcePlaceTokensTargetTrans_NoCallback()
        {
            // arrange
            StubPlace source = new StubPlace("testSource", new List<INode> { }, new List<INode> { });
            source.TokenCount = new Random().Next(1, 999);
            StubTransition target = new StubTransition("testTarget", new List<INode> { }, new List<INode> { });
            Arc testArc = new Arc(source, target, "testArc");
            source.Arcs = new List<IArc> { testArc };
            target.Arcs = new List<IArc> { testArc };

            // act
            testArc.Add(testCallback);

            // assert
            Assert.IsNull(callbackCalled, "No callback should occur when source has tokens.");
        }

        [TestMethod]
        public void Add_SourcePlaceNoTokensOnlyIncomingArcOnTarget_CallbackFalse()
        {
            // arrange
            StubPlace source = new StubPlace("testSource", new List<INode> { }, new List<INode> { });
            source.TokenCount = 0;
            StubTransition target = new StubTransition("testTarget", new List<INode> { }, new List<INode> { });
            Arc testArc = new Arc(source, target, "testArc");
            source.Arcs = new List<IArc> { testArc };
            target.Arcs = new List<IArc> { testArc };

            // act
            testArc.Add(testCallback);

            // assert
            Assert.AreEqual(false, callbackCalled, "Callback required when arc is only incoming arc on target" +
                                                   "and source is place without tokens.");
            Assert.AreEqual("testTarget", callbackId, "Target id should be used in callback.");
        }

        [TestMethod]
        public void Add_SourcePlaceNoTokensSecondIncomingArcOnTargetSourceNoTokens_NoCallback()
        {
            // arrange
            StubPlace source = new StubPlace("testSource", new List<INode> { }, new List<INode> { });
            source.TokenCount = 0;
            StubPlace secondPredecessor = new StubPlace("testSecondPredecessor", new List<INode> { }, new List<INode> { });
            secondPredecessor.TokenCount = 0;
            StubTransition target = new StubTransition("testTarget", new List<INode> { secondPredecessor }, new List<INode> { });
            Arc testArc = new Arc(source, target, "testArc");
            source.Arcs = new List<IArc> { testArc };
            target.Arcs = new List<IArc> { testArc };

            // act
            testArc.Add(testCallback);

            // assert
            Assert.IsNull(callbackCalled, "No callback should occur when target already has predecessor with no tokens.");
        }

        [TestMethod]
        public void Add_SourcePlaceNoTokensSecondIncomingArcOnTargetSourceTokens_CallbackFalse()
        {
            // arrange
            StubPlace source = new StubPlace("testSource", new List<INode> { }, new List<INode> { });
            source.TokenCount = 0;
            StubPlace secondPredecessor = new StubPlace("testSecondPredecessor", new List<INode> { }, new List<INode> { });
            secondPredecessor.TokenCount = new Random().Next(1, 999);
            StubTransition target = new StubTransition("testTarget", new List<INode> { secondPredecessor }, new List<INode> { });
            Arc testArc = new Arc(source, target, "testArc");
            source.Arcs = new List<IArc> { testArc };
            target.Arcs = new List<IArc> { testArc };

            // act
            testArc.Add(testCallback);

            // assert
            Assert.AreEqual(false, callbackCalled, "Callback required when all other predecessors of target have tokens.");
            Assert.AreEqual("testTarget", callbackId, "Target id should be used in callback.");
        }

        [TestMethod]
        public void Remove_SourceTransTargetPlace_NoCallback()
        {
            // arrange
            StubTransition source = new StubTransition("testSource", new List<INode> { }, new List<INode> { });
            StubPlace target = new StubPlace("testTarget", new List<INode> { }, new List<INode> { });
            Arc testArc = new Arc(source, target, "testArc");
            source.Arcs = new List<IArc> { testArc };
            target.Arcs = new List<IArc> { testArc };
            
            // act
            testArc.Remove(testCallback);
            
            // assert
            Assert.IsNull(callbackCalled, "No callback should occur when target is Place.");
        }

        [TestMethod]
        public void Remove_SourcePlaceTokensTargetTrans_NoCallback()
        {
            // arrange
            StubPlace source = new StubPlace("testSource", new List<INode> { }, new List<INode> { });
            source.TokenCount = new Random().Next(1, 999);
            StubTransition target = new StubTransition("testTarget", new List<INode> { }, new List<INode> { });
            Arc testArc = new Arc(source, target, "testArc");
            source.Arcs = new List<IArc> { testArc };
            target.Arcs = new List<IArc> { testArc };

            // act
            testArc.Remove(testCallback);

            // assert
            Assert.IsNull(callbackCalled, "No callback should occur when source has tokens.");
        }

        [TestMethod]
        public void Remove_SourcePlaceNoTokensOnlyIncomingArcOnTarget_CallbackTrue()
        {
            // arrange
            StubPlace source = new StubPlace("testSource", new List<INode> { }, new List<INode> { });
            source.TokenCount = 0;
            StubTransition target = new StubTransition("testTarget", new List<INode> { }, new List<INode> { });
            Arc testArc = new Arc(source, target, "testArc");
            source.Arcs = new List<IArc> { testArc };
            target.Arcs = new List<IArc> { testArc };

            // act
            testArc.Remove(testCallback);

            // assert
            Assert.AreEqual(true, callbackCalled, "Callback required when arc is only incoming arc on target" +
                                                  "and source is place without tokens.");
            Assert.AreEqual("testTarget", callbackId, "Target id should be used in callback.");
        }

        [TestMethod]
        public void Remove_SourcePlaceNoTokensSecondIncomingArcOnTargetSourceNoTokens_NoCallback()
        {
            // arrange
            StubPlace source = new StubPlace("testSource", new List<INode> { }, new List<INode> { });
            source.TokenCount = 0;
            StubPlace secondPredecessor = new StubPlace("testSecondPredecessor", new List<INode> { }, new List<INode> { });
            secondPredecessor.TokenCount = 0;
            StubTransition target = new StubTransition("testTarget", new List<INode> { secondPredecessor }, new List<INode> { });
            Arc testArc = new Arc(source, target, "testArc");
            source.Arcs = new List<IArc> { testArc };
            target.Arcs = new List<IArc> { testArc };

            // act
            testArc.Remove(testCallback);

            // assert
            Assert.IsNull(callbackCalled, "No callback should occur when target already has predecessor with no tokens.");
        }

        [TestMethod]
        public void Remove_SourcePlaceNoTokensSecondIncomingArcOnTargetSourceTokens_CallbackTrue()
        {
            // arrange
            StubPlace source = new StubPlace("testSource", new List<INode> { }, new List<INode> { });
            source.TokenCount = 0;
            StubPlace secondPredecessor = new StubPlace("testSecondPredecessor", new List<INode> { }, new List<INode> { });
            secondPredecessor.TokenCount = new Random().Next(1, 999);
            StubTransition target = new StubTransition("testTarget", new List<INode> { secondPredecessor }, new List<INode> { });
            Arc testArc = new Arc(source, target, "testArc");
            source.Arcs = new List<IArc> { testArc };
            target.Arcs = new List<IArc> { testArc };

            // act
            testArc.Remove(testCallback);

            // assert
            Assert.AreEqual(true, callbackCalled, "Callback required when all other predecessors of target have tokens.");
            Assert.AreEqual("testTarget", callbackId, "Target id should be used in callback.");
        }



        [TestMethod]
        public void Equals_CompareToNull_ReturnsFalse()
        {
            // arrange
            StubPlace sourcePlace = new StubPlace("sourcePlace", new List<INode> { }, new List<INode> { });
            StubTransition targetTrans = new StubTransition("targetTrans", new List<INode> { }, new List<INode> { });
            Arc testArc = new Arc(sourcePlace, targetTrans, "testArc");

            // act
            bool result = testArc.Equals(null);

            // assert
            Assert.IsFalse(result, "Comparison with null should always return false.");
        }

        [TestMethod]
        public void Equals_CompareToString_ReturnsFalse()
        {
            // arrange
            StubPlace sourcePlace = new StubPlace("sourcePlace", new List<INode> { }, new List<INode> { });
            StubTransition targetTrans = new StubTransition("targetTrans", new List<INode> { }, new List<INode> { });
            Arc testArc = new Arc(sourcePlace, targetTrans, "testArc");

            // act
            bool result = testArc.Equals("testString");

            // assert
            Assert.IsFalse(result, "Comparison with other type should always return false.");
        }

        [TestMethod]
        public void Equals_CompareToDifferentArc_ReturnsFalse()
        {
            // arrange
            StubPlace sourcePlace = new StubPlace("sourcePlace", new List<INode> { }, new List<INode> { });
            StubTransition targetTrans = new StubTransition("targetTrans", new List<INode> { }, new List<INode> { });
            Arc testArc = new Arc(sourcePlace, targetTrans, "testArc");
            Arc differentArc = new Arc(sourcePlace, targetTrans, "differentArc");

            // act
            bool result = testArc.Equals(differentArc);

            // assert
            Assert.IsFalse(result, "Comparison with arc with different id should return false.");
        }

        [TestMethod]
        public void Equals_CompareToSameArc_ReturnsTrue()
        {
            // arrange
            StubPlace sourcePlace = new StubPlace("sourcePlace", new List<INode> { }, new List<INode> { });
            StubTransition targetTrans = new StubTransition("targetTrans", new List<INode> { }, new List<INode> { });
            Arc testArc = new Arc(sourcePlace, targetTrans, "testArc");

            // act
            bool result = testArc.Equals(testArc);

            // assert
            Assert.IsTrue(result, "Comparison with same arc should return true.");
        }

        [TestMethod]
        public void Equals_CompareToArcWithSameId_ReturnsTrue()
        {
            // arrange
            StubPlace sourcePlace = new StubPlace("sourcePlace", new List<INode> { }, new List<INode> { });
            StubTransition targetTrans = new StubTransition("targetTrans", new List<INode> { }, new List<INode> { });
            Arc testArc = new Arc(sourcePlace, targetTrans, "testArc");
            Arc differentArc = new Arc(sourcePlace, targetTrans, "testArc");

            // act
            bool result = testArc.Equals(differentArc);

            // assert
            Assert.IsTrue(result, "Comparison with arc with same id should return true.");
        }
    }
}
