using System;
using Proteus.Kernel.Reflection;
using Microsoft.VisualStudio.QualityTools.UnitTesting.Framework;

namespace Proteus.Kernel.Reflection.Test
{
    /// <summary>
    ///This is a test class for Proteus.Kernel.Reflection.Converter and is intended
    ///to contain all Proteus.Kernel.Reflection.Converter Unit Tests
    ///</summary>
    [TestClass()]
    public class ConverterTest
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
        ///A test case for Convert (object, object)
        ///</summary>
        [TestMethod()]
        public void ConvertObjectTest()
        {
            object val = 10; // TODO: Initialize to an appropriate value

            object def = "Hello"; // TODO: Initialize to an appropriate value

            object expected = "10";
            object actual;

            actual = Converter.ConvertObject(val, def);

            Assert.AreEqual(expected, actual, "Proteus.Kernel.Reflection.Converter.Convert did not return the expected value.");

            val = "Green"; // TODO: Initialize to an appropriate value

            def = System.Drawing.Color.Red; // TODO: Initialize to an appropriate value

            expected = System.Drawing.Color.Green;

            actual = Converter.Convert(val, def);

            Assert.AreEqual(expected, actual, "Proteus.Kernel.Reflection.Converter.Convert did not return the expected value.");
        }

        /// <summary>
        ///A test case for Convert (object, Type)
        ///</summary>
        [TestMethod()]
        public void ConvertObjectTest1()
        {
            object val = "10"; // TODO: Initialize to an appropriate value

            Type targetType = typeof(int); // TODO: Initialize to an appropriate value

            object expected = 10;
            object actual;

            actual = Converter.ConvertObject(val, targetType);

            Assert.AreEqual(expected, actual, "Proteus.Kernel.Reflection.Converter.Convert did not return the expected value.");
        }

        /// <summary>
        ///A test case for Convert&lt;,&gt; (SourceType)
        ///</summary>
        [TestMethod()]
        public void ConvertTest()
        {
            int val = 10; // TODO: Initialize to an appropriate value
            float expected = 10.0f;
            float actual;
            actual = Converter.Convert<int,float>(val);
            Assert.AreEqual(expected, actual, "Proteus.Kernel.Reflection.Converter.Convert[SourceType, TargetType] did not retur" +
                            "n the expected value.");
        }

        /// <summary>
        ///A test case for Convert&lt;,&gt; (SourceType, TargetType)
        ///</summary>
        [TestMethod()]
        public void ConvertTest1()
        {
            Assert.AreEqual(10, Converter.Convert(new System.IO.MemoryStream(), 10));
            Assert.AreEqual(10, Converter.Convert("10", 5));
        }
    }
}
