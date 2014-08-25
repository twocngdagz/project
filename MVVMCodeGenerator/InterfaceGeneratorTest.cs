using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace MVVMCodeGenerator
{
    [TestFixture]
    public class InterfaceGeneratorTest
    {
        HTPropertyDefinition htprop = new HTPropertyDefinition("Id", "uint");
        List<HTPropertyDefinition> htprops = new List<HTPropertyDefinition>();

        public InterfaceGeneratorTest()
        {
            htprops.Add(new HTPropertyDefinition("Id", "uint"));
            htprops.Add(new HTPropertyDefinition("Name", "string"));
        }

        [Test]
        public void MainBody()
        {
            string actual = MVVMBasicGenerator.doInterfaceConversion("Mashisho", "Tenant", new List<HTPropertyDefinition>());
            string expected =
@"using System;
using System.Collections.ObjectModel;

namespace Mashisho
{
    interface ITenant
    {
    }
}
";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SingleInterfaceProperty()
        {
            string actual = MVVMBasicGenerator.singleInterfaceProperty(htprop);
            string expected = @"        uint Id { get; set; }
";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TwoInterfaceProperties()
        {
            string actual = MVVMBasicGenerator.multipleInterfaceProperties(htprops);
            string expected = @"        uint Id { get; set; }
        string Name { get; set; }
";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Full()
        {
            string actual = MVVMBasicGenerator.doInterfaceConversion("Mashisho", "Tenant", htprops);
            string expected =
@"using System;
using System.Collections.ObjectModel;

namespace Mashisho
{
    interface ITenant
    {
        uint Id { get; set; }
        string Name { get; set; }
    }
}
";
            Assert.AreEqual(expected, actual);
        }
    }
}
