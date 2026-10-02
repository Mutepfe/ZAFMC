using System;
//using System.Web.UI;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace ZAFMC.Tests
{
    [TestClass]
    public class Contacts
    {
        [TestMethod]
        public void ContactViewRecordExist()
        {
            

            Assert.Inconclusive();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContactViewRecordDoesNotExist()
        {
           
            Assert.Inconclusive();
        }
        [TestMethod]
        public void ContactViewRecordFound()
        {
            Assert.Inconclusive();
        }
    }
}
