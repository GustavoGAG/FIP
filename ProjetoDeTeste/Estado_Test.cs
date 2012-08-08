using DataAcess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;

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
        ///A test for Estado Constructor
        ///</summary>
        [TestMethod()]
        public void EstadoConstructor_Test()
        {
            Estado target = new Estado();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

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
            Assert.IsTrue(expected < actual,"Sucesso " + actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

 
        
        ///A test for Id
        ///</summary>
        [TestMethod()]
        public void Id_Test()
        {
            Estado target = new Estado(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected,actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Nome
        ///</summary>
        [TestMethod()]
        public void Nome_Test()
        {
            Estado target = new Estado(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Nome = expected;
            actual = target.Nome;
            Assert.AreEqual(expected,actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Sigla
        ///</summary>
        [TestMethod()]
        public void Sigla_Test()
        {
            Estado target = new Estado(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Sigla = expected;
            actual = target.Sigla;
            Assert.AreEqual(expected,actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for cidade
        ///</summary>
        [TestMethod()]
        public void cidade_Test()
        {
            Estado target = new Estado(); // TODO: Initialize to an appropriate value
            EntityCollection<Cidade> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<Cidade> actual;
            target.cidade = expected;
            actual = target.cidade;
            Assert.AreEqual(expected,actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for usuario
        ///</summary>
        [TestMethod()]
        public void usuario_Test()
        {
            Estado target = new Estado(); // TODO: Initialize to an appropriate value
            EntityCollection<Usuario> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<Usuario> actual;
            target.usuario = expected;
            actual = target.usuario;
            Assert.AreEqual(expected,actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
