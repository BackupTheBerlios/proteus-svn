using System.Collections.Generic;
using Proteus.Kernel.Pattern;
using Microsoft.VisualStudio.QualityTools.UnitTesting.Framework;
using System.IO;

namespace Proteus.Kernel.Test.Pattern
{
    ///<summary>
    /// This is a test class for Proteus.Kernel.Pattern.AbstractFactory[IdType, ProductType] and is intended
    /// to contain all Proteus.Kernel.Pattern.AbstractFactory[IdType, ProductType] Unit Tests
    ///</summary>
    [TestClass()]
    public class AbstractFactoryTest
    {
        private TestContext testContextInstance;

        internal class CustomCreator : IAbstractCreator
        {
            public object Create()
            {
                return new MemoryStream();
            }
        }

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
        /// A test case for Create (IdType)
        ///</summary>
        [TestMethod()]
        public void CreateTest()
        {
            AbstractFactory<int, Stream> factory = new AbstractFactory<int, Stream>();

            factory.Register(0, new CustomCreator());
            factory.Register(1, null);
            Stream newStream = factory.Create(0);
            
            Assert.AreNotEqual(newStream, null);
            Assert.AreEqual(newStream.GetType(), typeof(MemoryStream));
            Assert.AreEqual(factory.Create(1), null);
        }

        ///<summary>
        /// A test case for GetEnumerator ()
        ///</summary>
        [TestMethod()]
        public void GetEnumeratorTest()
        {
            AbstractFactory<int, Stream> factory = new AbstractFactory<int, Stream>();

            factory.Register(0, new CustomCreator());

            foreach (int id in factory)
            {
                Assert.AreEqual(0, id);
            }
        }

        ///<summary>
        /// A test case for Register (IdType, IAbstractCreator)
        ///</summary>
        [TestMethod()]
        public void RegisterTest()
        {
            AbstractFactory<int, Stream> factory = new AbstractFactory<int, Stream>();

            factory.Register(0, new CustomCreator());
            factory.Register(1, new CustomCreator());
            factory.Register(2, null);

            Assert.AreEqual(typeof(MemoryStream), factory.Create(0).GetType());
            Assert.AreEqual(typeof(MemoryStream), factory.Create(1).GetType());
        }

        ///<summary>
        /// A test case for Unregister (IAbstractCreator)
        ///</summary>
        [TestMethod()]
        public void UnregisterTest()
        {
            AbstractFactory<int, Stream> factory = new AbstractFactory<int, Stream>();

            factory.Register(0, new CustomCreator());
            factory.Register(1, new CustomCreator());

            factory.Unregister(0);
            factory.Unregister(1);
            factory.Unregister(3);

            Assert.AreEqual(factory.Create(0), null);
            Assert.AreEqual(factory.Create(1), null);
        }

        ///<summary>
        /// A test case for Unregister (IdType)
        ///</summary>
        [TestMethod()]
        public void UnregisterTest1()
        {
            AbstractFactory<int, Stream> factory = new AbstractFactory<int, Stream>();
            CustomCreator creator = new CustomCreator();

            factory.Register(0, creator );
            factory.Unregister(creator);

            Assert.AreEqual(factory.Create(0), null);
        }

        /// <summary>
        /// A test case for the Item Accessor
        /// </summary>
        [TestMethod()]
        public void ItemTest()
        {
            AbstractFactory<int, Stream> factory = new AbstractFactory<int, Stream>();

            factory.Register(0, new CustomCreator());
            factory.Register(1, new CustomCreator());

            CustomCreator creator = (CustomCreator)factory[0];
            CustomCreator creator2 = (CustomCreator)factory[4];

            Assert.AreNotEqual(creator, null);
            Assert.AreEqual(creator2, null);
        }
    }
}
