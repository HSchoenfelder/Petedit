using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetriNetEditor;
using Rhino.Mocks;

namespace PetriNetEditorTests
{
    [TestClass]
    public class MainViewModelTests
    {
        private static MockRepository mockEngine;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            mockEngine = new MockRepository();
        }

        [TestInitialize]
        public void Initialize()
        {
            DependencyFactory.CustomProvider = mockEngine.Stub<IElementProvider>();
            DependencyFactory.CustomSelectionManager = mockEngine.Stub<ISelectionManager>();
            DependencyFactory.CustomUndoManager = mockEngine.Stub<IUndoManagerEx>();
            DependencyFactory.CustomElementManager = mockEngine.Stub<IElementManager>();
            DependencyFactory.CustomElementCreator = mockEngine.Stub<IElementCreator>();
            DependencyFactory.CustomWorkspaceManager = mockEngine.Stub<IWorkspaceManager>();
            DependencyFactory.CustomUndoExecuter = mockEngine.Stub<IUndoExecuter>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SizeFactor_SizeZero_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            MainViewModel mvm = new MainViewModel();

            // act
            mvm.SizeFactor = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SizeFactor_NegativeSize_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            MainViewModel mvm = new MainViewModel();

            // act
            mvm.SizeFactor = -3;
        }

        [TestMethod]
        public void SizeFactor_NewValidPositiveSize_UpdatesUndoExecuter()
        {
            // arrange
            IUndoExecuter ue = mockEngine.DynamicMock<IUndoExecuter>();
            DependencyFactory.CustomUndoExecuter = ue;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.SizeFactor = 2;

            // assert
            ue.AssertWasCalled(s => s.SizeFactor = 2);
        }

        [TestMethod]
        public void SizeFactor_NewValidPositiveSize_ElementManagerNewArrowheadSize()
        {
            // arrange
            IElementManager em = mockEngine.DynamicMock<IElementManager>();
            DependencyFactory.CustomElementManager = em;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.SizeFactor = 2;

            // assert
            em.AssertWasCalled(s => s.ArrowheadSize = Arg<int>.Is.NotNull);
        }

        [TestMethod]
        public void SizeFactor_NewValidPositiveSize_ElementCreatorNewArrowheadSize()
        {
            // arrange
            IElementCreator ec = mockEngine.DynamicMock<IElementCreator>();
            DependencyFactory.CustomElementCreator = ec;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.SizeFactor = 2;

            // assert
            ec.AssertWasCalled(s => s.ArrowheadSize = Arg<int>.Is.Anything);
        }

        [TestMethod]
        public void SizeFactor_NewValidPositiveSize_ElementProviderChangesArrowheadSize()
        {
            // arrange
            IElementProvider ep = mockEngine.DynamicMock<IElementProvider>();
            DependencyFactory.CustomProvider = ep;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.SizeFactor = 2;

            // assert
            ep.AssertWasCalled(s => s.ChangeArrowheadSize(Arg<int>.Is.Anything));
        }

        [TestMethod]
        public void SizeFactor_NewValidPositiveSize_ElementManagerNewDrawSize()
        {
            // arrange
            IElementManager em = mockEngine.DynamicMock<IElementManager>();
            DependencyFactory.CustomElementManager = em;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.SizeFactor = 2;

            // assert
            em.AssertWasCalled(s => s.DrawSize = Arg<int>.Is.NotNull);
        }

        [TestMethod]
        public void SizeFactor_NewValidPositiveSize_ElementCreatorNewDrawSize()
        {
            // arrange
            IElementCreator ec = mockEngine.DynamicMock<IElementCreator>();
            DependencyFactory.CustomElementCreator = ec;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.SizeFactor = 2;

            // assert
            ec.AssertWasCalled(s => s.DrawSize = Arg<int>.Is.Anything);
        }

        [TestMethod]
        public void SizeFactor_NewValidPositiveSize_ElementProviderChangesDrawSize()
        {
            // arrange
            IElementProvider ep = mockEngine.DynamicMock<IElementProvider>();
            DependencyFactory.CustomProvider = ep;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.SizeFactor = 2;

            // assert
            ep.AssertWasCalled(s => s.ChangeDrawSize(Arg<int>.Is.Anything));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ViewWidth_NegativeViewWidth_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            MainViewModel mvm = new MainViewModel();

            // act
            mvm.ViewWidth = -3;
        }

        [TestMethod]
        public void ViewWidth_NewValidPositiveViewWidth_WorkspaceManagerNewViewWidth()
        {
            // arrange
            IWorkspaceManager wm = mockEngine.DynamicMock<IWorkspaceManager>();
            DependencyFactory.CustomWorkspaceManager = wm;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.ViewWidth = 250;

            // assert
            wm.AssertWasCalled(s => s.ViewWidth = 250);
        }

        [TestMethod]
        public void ViewWidth_NewValidPositiveViewWidth_ElementManagerNewViewWidth()
        {
            // arrange
            IElementManager em = mockEngine.DynamicMock<IElementManager>();
            DependencyFactory.CustomElementManager = em;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.ViewWidth = 250;

            // assert
            em.AssertWasCalled(s => s.ViewWidth = 250);
        }

        [TestMethod]
        public void ViewWidth_NewValidPositiveViewWidth_UndoExecuterNewViewWidth()
        {
            // arrange
            IUndoExecuter ue = mockEngine.DynamicMock<IUndoExecuter>();
            DependencyFactory.CustomUndoExecuter = ue;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.ViewWidth = 250;

            // assert
            ue.AssertWasCalled(s => s.ViewWidth = 250);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ViewHeight_NegativeViewHeight_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            MainViewModel mvm = new MainViewModel();

            // act
            mvm.ViewHeight = -240;
        }

        [TestMethod]
        public void ViewHeight_NewValidPositiveViewHeight_WorkspaceManagerNewViewHeight()
        {
            // arrange
            IWorkspaceManager wm = mockEngine.DynamicMock<IWorkspaceManager>();
            DependencyFactory.CustomWorkspaceManager = wm;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.ViewHeight = 1000;

            // assert
            wm.AssertWasCalled(s => s.ViewHeight = 1000);
        }

        [TestMethod]
        public void ViewHeight_NewValidPositiveViewHeight_ElementManagerNewViewHeight()
        {
            // arrange
            IElementManager em = mockEngine.DynamicMock<IElementManager>();
            DependencyFactory.CustomElementManager = em;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.ViewHeight = 1000;

            // assert
            em.AssertWasCalled(s => s.ViewHeight = 1000);
        }

        [TestMethod]
        public void ViewHeight_NewValidPositiveViewHeight_UndoExecuterNewViewHeight()
        {
            // arrange
            IUndoExecuter ue = mockEngine.DynamicMock<IUndoExecuter>();
            DependencyFactory.CustomUndoExecuter = ue;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.ViewHeight = 1000;

            // assert
            ue.AssertWasCalled(s => s.ViewHeight = 1000);
        }


        [TestMethod]
        public void Selecting_NewSelectingValue_WorkspaceManagerSelectingUpdated()
        {
            // arrange
            IWorkspaceManager wm = mockEngine.DynamicMock<IWorkspaceManager>();
            DependencyFactory.CustomWorkspaceManager = wm;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.Selecting = true;

            // assert
            wm.AssertWasCalled(s => s.Selecting = true);
        }

        [TestMethod]
        public void Selecting_NewSelectingValue_UndoManagerSelectingUpdated()
        {
            // arrange
            IUndoManagerEx um = mockEngine.DynamicMock<IUndoManagerEx>();
            DependencyFactory.CustomUndoManager = um;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.Selecting = true;

            // assert
            um.AssertWasCalled(s => s.Selecting = true);
        }


        [TestMethod]
        public void Drawing_NewSelectingValue_WorkspaceManagerDrawingUpdated()
        {
            // arrange
            IWorkspaceManager wm = mockEngine.DynamicMock<IWorkspaceManager>();
            DependencyFactory.CustomWorkspaceManager = wm;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.Drawing = true;

            // assert
            wm.AssertWasCalled(s => s.Drawing = true);
        }

        [TestMethod]
        public void Drawing_NewDrawingValue_UndoManagerDrawingUpdated()
        {
            // arrange
            IUndoManagerEx um = mockEngine.DynamicMock<IUndoManagerEx>();
            DependencyFactory.CustomUndoManager = um;
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            mvm.Drawing = true;

            // assert
            um.AssertWasCalled(s => s.Drawing = true);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SizeChangeCommand_SizeZero_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            StubCommand sizeChangeCommand = new StubCommand(CommandTypes.SizeChangeCommand);
            CommandFactory.AddCustomCommand(sizeChangeCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            sizeChangeCommand.Execute(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SizeChangeCommand_NegativeSize_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            StubCommand sizeChangeCommand = new StubCommand(CommandTypes.SizeChangeCommand);
            CommandFactory.AddCustomCommand(sizeChangeCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            sizeChangeCommand.Execute(-4);
        }

        [TestMethod]
        public void SizeChangeCommand_SameValidPositiveSize_SizeStaysSame()
        {
            // arrange
            StubCommand sizeChangeCommand = new StubCommand(CommandTypes.SizeChangeCommand);
            CommandFactory.AddCustomCommand(sizeChangeCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            sizeChangeCommand.Execute(1);

            // assert
            Assert.AreEqual<int>(1, mvm.SizeFactor, "The size changed");
        }

        [TestMethod]
        public void SizeChangeCommand_SameValidPositiveSize_NoUndoOpPushed()
        {
            IUndoManagerEx um = mockEngine.StrictMock<IUndoManagerEx>();
            DependencyFactory.CustomUndoManager = um;
            StubCommand sizeChangeCommand = new StubCommand(CommandTypes.SizeChangeCommand);
            CommandFactory.AddCustomCommand(sizeChangeCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            sizeChangeCommand.Execute(1);

            // assert
            um.AssertWasNotCalled(s => s.PushSizeChangeUndo());
        }

        [TestMethod]
        public void SizeChangeCommand_SameValidPositiveSize_RedoStackNotCleared()
        {
            IUndoManagerEx um = mockEngine.StrictMock<IUndoManagerEx>();
            DependencyFactory.CustomUndoManager = um;
            StubCommand sizeChangeCommand = new StubCommand(CommandTypes.SizeChangeCommand);
            CommandFactory.AddCustomCommand(sizeChangeCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            sizeChangeCommand.Execute(1);

            // assert
            um.AssertWasNotCalled(s => s.ClearRedoStack());
        }

        [TestMethod]
        public void SizeChangeCommand_SameValidPositiveSize_SaveFlagNotSet()
        {
            StubCommand sizeChangeCommand = new StubCommand(CommandTypes.SizeChangeCommand);
            CommandFactory.AddCustomCommand(sizeChangeCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            sizeChangeCommand.Execute(1);

            // assert
            Assert.IsFalse(mvm.Modified, "Save flag has been set");
        }

        [TestMethod]
        public void SizeChangeCommand_NewValidPositiveSize_SetsNewSize()
        {
            // arrange
            StubCommand sizeChangeCommand = new StubCommand(CommandTypes.SizeChangeCommand);
            CommandFactory.AddCustomCommand(sizeChangeCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            sizeChangeCommand.Execute(3);

            // assert
            Assert.AreEqual<int>(3, mvm.SizeFactor, "The size in MainViewModel differs from the size that was set");
        }

        [TestMethod]
        public void SizeChangeCommand_NewValidPositiveSize_PushesUndoOp()
        {
            // arrange
            IUndoManagerEx um = mockEngine.DynamicMock<IUndoManagerEx>();
            DependencyFactory.CustomUndoManager = um;
            StubCommand sizeChangeCommand = new StubCommand(CommandTypes.SizeChangeCommand);
            CommandFactory.AddCustomCommand(sizeChangeCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            sizeChangeCommand.Execute(2);

            // assert
            um.AssertWasCalled(s => s.PushSizeChangeUndo());
        }

        [TestMethod]
        public void SizeChangeCommand_NewValidPositiveSize_ClearsRedoStack()
        {
            // arrange
            IUndoManagerEx um = mockEngine.DynamicMock<IUndoManagerEx>();
            DependencyFactory.CustomUndoManager = um;
            StubCommand sizeChangeCommand = new StubCommand(CommandTypes.SizeChangeCommand);
            CommandFactory.AddCustomCommand(sizeChangeCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            sizeChangeCommand.Execute(2);

            // assert
            um.AssertWasCalled(s => s.ClearRedoStack());
        }

        [TestMethod]
        public void SizeChangeCommand_SameValidPositiveSize_SaveFlagSet()
        {
            StubCommand sizeChangeCommand = new StubCommand(CommandTypes.SizeChangeCommand);
            CommandFactory.AddCustomCommand(sizeChangeCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            sizeChangeCommand.Execute(4);

            // assert
            Assert.IsTrue(mvm.Modified, "Save flag has not been set");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LoadedCommand_NegativeViewWidth_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            StubCommand loadedCommand = new StubCommand(CommandTypes.LoadedCommand);
            CommandFactory.AddCustomCommand(loadedCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            loadedCommand.Execute(new Point(-20, 300));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LoadedCommand_NegativeViewHeight_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            StubCommand loadedCommand = new StubCommand(CommandTypes.LoadedCommand);
            CommandFactory.AddCustomCommand(loadedCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            loadedCommand.Execute(new Point(210, -300));
        }

        [TestMethod]
        public void LoadedCommand_NewWidthAndHeight_WidthUpdated()
        {
            // arrange
            StubCommand loadedCommand = new StubCommand(CommandTypes.LoadedCommand);
            CommandFactory.AddCustomCommand(loadedCommand);
            MainViewModel mvm = new MainViewModel();
            
            // act
            loadedCommand.Execute(new Point(50, 235));

            // assert
            Assert.AreEqual<double>(50, mvm.ViewWidth, "ViewWidth was not updated correctly");
        }

        [TestMethod]
        public void LoadedCommand_NewWidthAndHeight_HeightUpdated()
        {
            // arrange
            StubCommand loadedCommand = new StubCommand(CommandTypes.LoadedCommand);
            CommandFactory.AddCustomCommand(loadedCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            loadedCommand.Execute(new Point(50, 235));

            // assert
            Assert.AreEqual<double>(235, mvm.ViewHeight, "ViewHeight was not updated correctly");
        }
        

        [TestMethod]
        public void DeleteNodesCommand_SingleNodePresent_NodeDeletedFromModel()
        {
            // arrange
            StubModel testModel = new StubModel();
            testModel.AddPlace(0, 0, "testNode");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.SelectElement("testNode");
            DependencyFactory.CustomSelectionManager = testSm;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            deleteNodesCommand.Execute(null);

            // assert
            Assert.IsFalse(testModel.Contains("testNode"), "Node was not deleted from model");
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleNodePresent_NodeDeletedFromElementProvider()
        {
            // arrange
            StubModel testModel = new StubModel();
            testModel.AddPlace(0, 0, "testNode");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.SelectElement("testNode");
            DependencyFactory.CustomSelectionManager = testSm;
            IElementProvider ep = mockEngine.DynamicMock<IElementProvider>();
            DependencyFactory.CustomProvider = ep;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            deleteNodesCommand.Execute(null);

            // assert
            ep.AssertWasCalled(s => s.RemoveNode("testNode"));
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleNodePresent_NameFieldDeletedFromElementProvider()
        {
            // arrange
            StubModel testModel = new StubModel();
            testModel.AddPlace(0, 0, "testNode");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.SelectElement("testNode");
            DependencyFactory.CustomSelectionManager = testSm;
            IElementProvider ep = mockEngine.DynamicMock<IElementProvider>();
            DependencyFactory.CustomProvider = ep;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            deleteNodesCommand.Execute(null);

            // assert
            ep.AssertWasCalled(s => s.RemoveNameField("testNode"));
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleAutoSelectedArcPresent_ArcDeletedFromElementProvider()
        {
            // arrange
            StubModel testModel = new StubModel();
            testModel.AddArc("testSource", "testTarget", "testArc");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.AddAutoSelectedArc("testArc");
            DependencyFactory.CustomSelectionManager = testSm;
            IElementProvider ep = mockEngine.DynamicMock<IElementProvider>();
            DependencyFactory.CustomProvider = ep;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            deleteNodesCommand.Execute(null);

            // assert
            ep.AssertWasCalled(s => s.RemoveArc("testArc"));
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleAutoSelectedArcPresent_ArcDeletedFromModel()
        {
            // arrange
            StubModel testModel = new StubModel();
            testModel.AddArc("testSource", "testTarget", "testArc");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.AddAutoSelectedArc("testArc");
            DependencyFactory.CustomSelectionManager = testSm;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            deleteNodesCommand.Execute(null);

            // assert
            Assert.IsFalse(testModel.Contains("testArc"), "Arc was not deleted from model");
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleAutoSelectedArcPresent_ArcRemovedAsBrotherArc()
        {
            // arrange
            StubModel testModel = new StubModel();
            testModel.AddArc("testSource", "testTarget", "testArc");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.AddAutoSelectedArc("testArc");
            DependencyFactory.CustomSelectionManager = testSm;
            IElementManager em = mockEngine.DynamicMock<IElementManager>();
            DependencyFactory.CustomElementManager = em;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            deleteNodesCommand.Execute(null);

            // assert
            em.AssertWasCalled(s => s.RemoveBrother("testArc"));
        }
        
        [TestMethod]
        public void DeleteNodesCommand_SingleSelectedArcPresent_ArcDeletedFromElementProvider()
        {
            // arrange
            StubModel testModel = new StubModel();
            testModel.AddArc("testSource", "testTarget", "testArc");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.SelectElement("testArc");
            DependencyFactory.CustomSelectionManager = testSm;
            IElementProvider ep = mockEngine.DynamicMock<IElementProvider>();
            DependencyFactory.CustomProvider = ep;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            deleteNodesCommand.Execute(null);

            // assert
            ep.AssertWasCalled(s => s.RemoveArc("testArc"));
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleSelectedArcPresent_ArcDeletedFromModel()
        {
            // arrange
            StubModel testModel = new StubModel();
            testModel.AddArc("testSource", "testTarget", "testArc");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.SelectElement("testArc");
            DependencyFactory.CustomSelectionManager = testSm;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            deleteNodesCommand.Execute(null);

            // assert
            Assert.IsFalse(testModel.Contains("testArc"), "Arc was not deleted from model");
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleSelectedArcPresent_ArcRemovedAsBrotherArc()
        {
            // arrange
            StubModel testModel = new StubModel();
            testModel.AddArc("testSource", "testTarget", "testArc");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.SelectElement("testArc");
            DependencyFactory.CustomSelectionManager = testSm;
            IElementManager em = mockEngine.DynamicMock<IElementManager>();
            DependencyFactory.CustomElementManager = em;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            deleteNodesCommand.Execute(null);

            // assert
            em.AssertWasCalled(s => s.RemoveBrother("testArc"));
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleNodeDeleted_PushesUndoOp()
        {
            StubModel testModel = new StubModel();
            testModel.AddPlace(0, 0, "testNode");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.SelectElement("testNode");
            DependencyFactory.CustomSelectionManager = testSm;
            IUndoManagerEx um = mockEngine.DynamicMock<IUndoManagerEx>();
            DependencyFactory.CustomUndoManager = um;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            deleteNodesCommand.Execute(null);

            // assert
            um.AssertWasCalled(s => s.PushDeleteUndo());
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleAutoSelectedArcDeleted_ClearsRedoStack()
        {
            StubModel testModel = new StubModel();
            testModel.AddArc("testSource", "testTarget", "testArc");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.AddAutoSelectedArc("testArc");
            DependencyFactory.CustomSelectionManager = testSm;
            IUndoManagerEx um = mockEngine.DynamicMock<IUndoManagerEx>();
            DependencyFactory.CustomUndoManager = um;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            mockEngine.ReplayAll();
            deleteNodesCommand.Execute(null);

            // assert
            um.AssertWasCalled(s => s.ClearRedoStack());
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleSelectedArcDeleted_SaveFlagSet()
        {
            StubModel testModel = new StubModel();
            testModel.AddArc("testSource", "testTarget", "testArc");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.SelectElement("testArc");
            DependencyFactory.CustomSelectionManager = testSm;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            deleteNodesCommand.Execute(null);

            // assert
            Assert.IsTrue(mvm.Modified, "Save flag has not been set");
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleNodeDeleted_DeleteNodesCommandUpdated()
        {
            StubModel testModel = new StubModel();
            testModel.AddPlace(0, 0, "testNode");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.SelectElement("testNode");
            DependencyFactory.CustomSelectionManager = testSm;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            CommandFactory.AddCustomCommand(new StubCommand(CommandTypes.SelectAllCommand));
            MainViewModel mvm = new MainViewModel();

            // act
            deleteNodesCommand.Execute(null);

            // assert
            Assert.IsTrue(deleteNodesCommand.CanExecuteChanged, "DeleteNodesCommand has not been updated");
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleAutoSelectedArcDeleted_SelectAllCommandUpdated()
        {
            StubModel testModel = new StubModel();
            testModel.AddArc("testSource", "testTarget", "testArc");
            ModelFactory.CustomModel = testModel;
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.AddAutoSelectedArc("testArc");
            DependencyFactory.CustomSelectionManager = testSm;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            StubCommand selectAllCommand = new StubCommand(CommandTypes.SelectAllCommand);
            CommandFactory.AddCustomCommand(selectAllCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            deleteNodesCommand.Execute(null);

            // assert
            Assert.IsTrue(selectAllCommand.CanExecuteChanged, "DeleteNodesCommand has not been updated");
        }

        [TestMethod]
        public void DeleteNodesCommand_NothingSelected_CommandCannotExecute()
        {
            // arrange
            StubSelectionManager testSm = new StubSelectionManager();
            DependencyFactory.CustomSelectionManager = testSm;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            bool canExecute = deleteNodesCommand.CanExecute(null);

            // assert
            Assert.IsFalse(canExecute, "The command can execute");
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleNodeSelected_CommandCanExecute()
        {
            // arrange
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.SelectElement("testNode");
            DependencyFactory.CustomSelectionManager = testSm;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            bool canExecute = deleteNodesCommand.CanExecute(null);

            // assert
            Assert.IsTrue(canExecute, "The command cannot execute");
        }

        [TestMethod]
        public void DeleteNodesCommand_SingleArcSelected_CommandCanExecute()
        {
            // arrange
            StubSelectionManager testSm = new StubSelectionManager();
            testSm.SelectElement("testArc");
            DependencyFactory.CustomSelectionManager = testSm;
            StubCommand deleteNodesCommand = new StubCommand(CommandTypes.DeleteNodesCommand);
            CommandFactory.AddCustomCommand(deleteNodesCommand);
            MainViewModel mvm = new MainViewModel();

            // act
            bool canExecute = deleteNodesCommand.CanExecute(null);

            // assert
            Assert.IsTrue(canExecute, "The command cannot execute");
        }


        [TestCleanup]
        public void Cleanup()
        {
            CommandFactory.ResetCustomCommands();
            ModelFactory.CustomModel = null;
            DependencyFactory.CustomProvider = null;
            DependencyFactory.CustomSelectionManager = null;
            DependencyFactory.CustomUndoManager = null;
            DependencyFactory.CustomElementManager = null;
            DependencyFactory.CustomElementCreator = null;
            DependencyFactory.CustomWorkspaceManager = null;
            DependencyFactory.CustomUndoExecuter = null;
        }
    }
}
