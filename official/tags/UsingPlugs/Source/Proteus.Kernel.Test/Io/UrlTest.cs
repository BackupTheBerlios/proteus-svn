using Microsoft.VisualStudio.QualityTools.UnitTesting.Framework;
namespace Proteus.Kernel.Io.Test
{
    /// <summary>
    ///This is a test class for Proteus.Kernel.Io.Url and is intended
    ///to contain all Proteus.Kernel.Io.Url Unit Tests
    ///</summary>
    [TestClass()]
    public class UrlTest
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
        ///A test case for GetDirectory (string)
        ///</summary>
        [TestMethod()]
        public void GetDirectoryTest()
        {
            Assert.AreEqual("/home/test/", Url.GetDirectory("/home/test/test.gif"));
            Assert.AreEqual("home/test/", Url.GetDirectory("home/test/test.gif"));
        }

        /// <summary>
        ///A test case for GetExtension (string)
        ///</summary>
        [TestMethod()]
        public void GetExtensionTest()
        {
            Assert.AreEqual("gif", Url.GetExtension("/home/test/test.gif"));
            Assert.AreEqual("gif", Url.GetExtension("test.gif"));
            Assert.AreEqual("gif", Url.GetExtension("home/test/test.gif"));
        }

        /// <summary>
        ///A test case for GetFilename (string)
        ///</summary>
        [TestMethod()]
        public void GetFilenameTest()
        {
            Assert.AreEqual("test.gif",Url.GetFilename("/home/test/test.gif"));
            Assert.AreEqual("test.gif",Url.GetFilename("test.gif"));
            Assert.AreEqual("test.gif", Url.GetFilename("home/test/test.gif"));
        }

        /// <summary>
        ///A test case for IsAbsolute (string)
        ///</summary>
        [TestMethod()]
        public void IsAbsoluteTest()
        {
            Assert.IsFalse(Url.IsAbsolute("home/test/"));
            Assert.IsTrue(Url.IsAbsolute("/home/test/"));
        }

        /// <summary>
        ///A test case for IsDirectory (string)
        ///</summary>
        [TestMethod()]
        public void IsDirectoryTest()
        {
            Assert.IsTrue(Url.IsDirectory("home"));
            Assert.IsTrue(Url.IsDirectory("/home/users"));
            Assert.IsTrue(Url.IsDirectory("home/users"));

            Assert.IsFalse(Url.IsDirectory("/home/test/test.gif"));
            Assert.IsFalse(Url.IsDirectory("home/test/test.gif"));
            Assert.IsFalse(Url.IsDirectory("test.gif"));
        }

        /// <summary>
        ///A test case for IsFilename (string)
        ///</summary>
        [TestMethod()]
        public void IsFilenameTest()
        {
            Assert.IsTrue(Url.IsFilename("/home/test/test.gif"));
            Assert.IsTrue(Url.IsFilename("home/test/test.gif"));
            Assert.IsTrue(Url.IsFilename("test.gif"));

            Assert.IsFalse(Url.IsFilename("home"));
            Assert.IsFalse(Url.IsFilename("/home/users"));
            Assert.IsFalse(Url.IsFilename("home/users"));
        }

        /// <summary>
        ///A test case for IsRelative (string)
        ///</summary>
        [TestMethod()]
        public void IsRelativeTest()
        {
            Assert.IsTrue(Url.IsRelative("home/test/"));
            Assert.IsFalse(Url.IsRelative("/home/test/"));
        }

        /// <summary>
        ///A test case for IsValid (string)
        ///</summary>
        [TestMethod()]
        public void IsValidTest()
        {
            Assert.IsTrue(Url.IsValid("/home/users"));
            Assert.IsTrue(Url.IsValid("home"));
            Assert.IsTrue(Url.IsValid("test.gif"));
            Assert.IsTrue(Url.IsValid("home/test/backstab.gif"));
            Assert.IsTrue(Url.IsValid("/home/test/test.gif"));
            Assert.IsFalse(Url.IsValid("!test"));
            Assert.IsFalse(Url.IsValid("/home//test"));
        }
    }
}
