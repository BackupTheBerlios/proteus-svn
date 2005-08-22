using Microsoft.VisualStudio.QualityTools.UnitTesting.Framework;
using System;

namespace Proteus.Kernel.Pattern.Test
{
    public class DisposeException : System.ApplicationException
    {
    }

    public class DisposeSingleton : Pattern.Singleton<DisposeSingleton>,IDisposable
    {
        public void Dispose()
        {
            throw new DisposeException();
        }
    }

    public class DefaultSingleton : Pattern.Singleton<DefaultSingleton>
    {
        public bool SomeMethod()
        {
            return true;
        }
    }
    
    /// <summary>
    ///This is a test class for Proteus.Kernel.Pattern.Singleton[SingletonType] and is intended
    ///to contain all Proteus.Kernel.Pattern.Singleton[SingletonType] Unit Tests
    ///</summary>
    [TestClass()]
    public class SingletonTest
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


        /// <summary>
        ///A test case for Dispose ()
        ///</summary>
        [TestMethod()]
        [ExpectedException( typeof(DisposeException) )]
        public void ReleaseTest()
        {
            DefaultSingleton.Release();
            DisposeSingleton firstAccess = DisposeSingleton.Instance;
            DisposeSingleton.Release();
        }

        /// <summary>
        ///A test case for Instance
        ///</summary>
        [TestMethod()]
        public void InstanceTest()
        {
            Assert.IsTrue(DefaultSingleton.Instance.SomeMethod());
        }
    }
}
