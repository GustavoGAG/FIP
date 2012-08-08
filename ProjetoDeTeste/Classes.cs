using DataAcess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ProjetoDeTeste
{
    
    
    /// <summary>
    ///This is a test class for Estado_Test and is intended
    ///to contain all Estado_Test Unit Tests
    ///</summary>
    [TestClass()]
    public class Estado_Test
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Atualizar
        ///</summary>
        [TestMethod()]
        public void Atualizar_Test()
        {
            Estado target = new Estado(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.Atualizar();
            Assert.IsTrue(actual > expected,"Sucesso " + actual);

            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
