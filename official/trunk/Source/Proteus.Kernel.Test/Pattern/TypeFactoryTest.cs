using Proteus.Kernel.Pattern;
using Microsoft.VisualStudio.QualityTools.UnitTesting.Framework;
using System.IO;

namespace Proteus.Kernel.Test
{
    ///<summary>
    /// This is a test class for Proteus.Kernel.Pattern.TypeFactory[ProductType] and is intended
    /// to contain all Proteus.Kernel.Pattern.TypeFactory[ProductType] Unit Tests
    ///</summary>
    [TestClass()]
    public class TypeFactoryTest
    {
        private TestContext testContextInstance;
        
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        /// <summary>
        ///Initialize() is called once during test execution before
        ///test methods in this test class are executed.
        ///</summary>
        [TestInitialize()]
        public void Initialize()
        {
            //  TODO: Add test initialization code
        }

        /// <summary>
        ///Cleanup() is called once during test execution after
        ///test methods in this class have executed unless
        ///this test class' Initialize() method throws an exception.
        ///</summary>
        [TestCleanup()]
        public void Cleanup()
        {
            //  TODO: Add test cleanup code
        }

        ///<summary>
        /// A test case for Register()
        ///</summary>
        [TestMethod()]
        public void RegisterTest()
        {
            TypeFactory<Stream> factory = new TypeFactory<Stream>();
            factory.Register<MemoryStream>();

            Stream newStream = factory.Create("System.IO.MemoryStream");

            Assert.AreNotEqual(newStream, null);
            Assert.AreEqual(newStream.GetType(), typeof(MemoryStream));

        }

        /// <summary>
        /// A test case for Unregister()
        /// </summary>
        [TestMethod()]
        public void UnregisterTest()
        {
            TypeFactory<Stream> factory = new TypeFactory<Stream>();
            factory.Register<MemoryStream>();
            factory.Unregister<MemoryStream>();

            Assert.AreEqual(factory.Create("System.IO.MemoryStream"), null);
        }
    }
}
