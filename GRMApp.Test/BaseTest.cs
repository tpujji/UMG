using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace GRMApp.Test {
    [TestClass]
    public class BaseTest {
       public string BasePath { get; set; }
        public BaseTest() {
            BasePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
        }
    }
}
