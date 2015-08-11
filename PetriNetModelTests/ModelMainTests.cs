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
    public class ModelMainTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ElementFactory.SetCustomArc(null);
            ElementFactory.SetCustomPlace(null);
            ElementFactory.SetCustomTransition(null);
        }

        private void SetUpBasicFactoryDummies()
        {
            StubTransition dummyTrans = new StubTransition("dummyTrans", new List<INode> { }, new List<INode> { });
            ElementFactory.SetCustomTransition(dummyTrans);
            StubPlace dummyPlace = new StubPlace("dummyPlace", new List<INode> { }, new List<INode> { });
            ElementFactory.SetCustomPlace(dummyPlace);
            StubArc dummyArc = new StubArc("dummyArc");
            ElementFactory.SetCustomArc(dummyArc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNode_IdNull_ThrowsArgumentNullException()
        {
            // arrange
            ModelMain testModelMain = new ModelMain();

            // act
            testModelMain.IsNode(null);
        }

        [TestMethod]
        public void IsNode_NonExistentId_ReturnsFalse()
        {
            // arrange
            SetUpBasicFactoryDummies();
            ModelMain testModelMain = new ModelMain();
            testModelMain.AddTransition(0, 0, "transId");
            testModelMain.AddPlace(0, 0, "placeId");
            testModelMain.AddArc("transId", "placeId", "arcId");
            
            // act
            bool result = testModelMain.IsNode("NonExistentId");

            // assert
            Assert.IsFalse(result, "Should return false if id is non-existent.");
        }

        [TestMethod]
        public void IsNode_IdOfArc_ReturnsFalse()
        {
            // arrange
            SetUpBasicFactoryDummies();
            ModelMain testModelMain = new ModelMain();
            testModelMain.AddTransition(0, 0, "transId");
            testModelMain.AddPlace(0, 0, "placeId");
            testModelMain.AddArc("transId", "placeId", "arcId");

            // act
            bool result = testModelMain.IsNode("arcId");

            // assert
            Assert.IsFalse(result, "Should return false if id belongs to Arc.");
        }

        [TestMethod]
        public void IsNode_IdOfNode_ReturnsTrue()
        {
            // arrange
            SetUpBasicFactoryDummies();
            ModelMain testModelMain = new ModelMain();
            testModelMain.AddTransition(0, 0, "transId");
            testModelMain.AddPlace(0, 0, "placeId");
            testModelMain.AddArc("transId", "placeId", "arcId");

            // act
            bool result = testModelMain.IsNode("placeId");

            // assert
            Assert.IsTrue(result, "Should return true if id belongs to Node.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contains_IdNull_ThrowsArgumentNullException()
        {
            // arrange
            ModelMain testModelMain = new ModelMain();

            // act
            testModelMain.Contains(null);
        }

        [TestMethod]
        public void Contains_NonExistentId_ReturnsFalse()
        {
            // arrange
            SetUpBasicFactoryDummies();
            ModelMain testModelMain = new ModelMain();
            testModelMain.AddTransition(0, 0, "transId");
            testModelMain.AddPlace(0, 0, "placeId");
            testModelMain.AddArc("transId", "placeId", "arcId");

            // act
            bool result = testModelMain.Contains("NonExistentId");

            // assert
            Assert.IsFalse(result, "Should return false if element with id does not exist.");
        }

        [TestMethod]
        public void Contains_IdOfNode_ReturnsTrue()
        {
            // arrange
            SetUpBasicFactoryDummies();
            ModelMain testModelMain = new ModelMain();
            testModelMain.AddTransition(0, 0, "transId");
            testModelMain.AddPlace(0, 0, "placeId");
            testModelMain.AddArc("transId", "placeId", "arcId");

            // act
            bool result = testModelMain.Contains("transId");

            // assert
            Assert.IsTrue(result, "Should return true if id belongs to existent Node.");
        }

        [TestMethod]
        public void Contains_IdOfArc_ReturnsTrue()
        {
            // arrange
            SetUpBasicFactoryDummies();
            ModelMain testModelMain = new ModelMain();
            testModelMain.AddTransition(0, 0, "transId");
            testModelMain.AddPlace(0, 0, "placeId");
            testModelMain.AddArc("transId", "placeId", "arcId");

            // act
            bool result = testModelMain.Contains("arcId");

            // assert
            Assert.IsTrue(result, "Should return true if id belongs to existent Arc.");
        }
    }
}
