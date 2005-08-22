using Proteus.Kernel.Pattern;
using Microsoft.VisualStudio.QualityTools.UnitTesting.Framework;
using System.IO;

namespace Proteus.Kernel.Test.Pattern
{
    ///<summary>
    /// This is a test class for Proteus.Kernel.Pattern.ConcreteFactory[IdType, ProductType] and is intended
    /// to contain all Proteus.Kernel.Pattern.ConcreteFactory[IdType, ProductType] Unit Tests
    ///</summary>
    [TestClass()]
    public class ConcreteFactoryTest
    {
        private TestContext testContextInstance;

        ///<summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        ///<summary>
        /// Initialize() is called once during test execution before
        /// test methods in this test class are executed.
        ///</summary>
        [TestInitialize()]
        public void Initialize()
        {
            //  TODO: Add test initialization code
        }

        ///<summary>
        /// Cleanup() is called once during test execution after
        /// test methods in this class have executed unless
        /// this test class' Initialize() method throws an exception.
        ///</summary>
        [TestCleanup()]
        public void Cleanup()
        {
            //  TODO: Add test cleanup code
        }

        ///<summary>
        /// A test case for Register&lt;&gt; (IdType)
        ///</summary>
        [TestMethod()]
        public void RegisterTest()
        {
            ConcreteFactory<int, Stream> factory =
                new ConcreteFactory<int, Stream>();

            factory.Register<MemoryStream>(0);

            Assert.AreEqual(factory.Create(0).GetType(), typeof(MemoryStream));
            Assert.AreEqual(factory.Create(2), null);
        }

        ///<summary>
        /// A test case for Unregister&lt;&gt; ()
        ///</summary>
        [TestMethod()]
        public void UnregisterTest()
        {
            ConcreteFactory<int, Stream> factory =
                new ConcreteFactory<int, Stream>();

            factory.Register<MemoryStream>(0);
            factory.Unregister<MemoryStream>();

            Assert.AreEqual(factory.Create(0), null);
        }
    }
}
