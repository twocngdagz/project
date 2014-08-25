using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace MVVMCodeGenerator
{
    [TestFixture]
    public class HTFileParserTest
    {
        [Test]
        public void Empty()
        {
            string filecontent = "";
            List<HTPropertyDefinition> htprops = HTPropertyDefinition.fromFile(filecontent);
            Assert.AreEqual(0, htprops.Count);
        }
        [Test]
        public void BasicNameType()
        {
            string filecontent = @"
A Id uint
";
            List<HTPropertyDefinition> htprops = HTPropertyDefinition.fromFile(filecontent);
            Assert.AreEqual(1, htprops.Count);
            HTPropertyDefinition htprop = htprops[0];
            Assert.AreEqual("Id", htprop.Name);
            Assert.AreEqual("uint", htprop.Type);
            Assert.AreEqual(null, htprop.ToStringOverride);
        }
        [Test]
        public void TwoNameTypes()
        {
            string filecontent = @"
A Id uint

A Name string
";
            List<HTPropertyDefinition> htprops = HTPropertyDefinition.fromFile(filecontent);
            Assert.AreEqual(2, htprops.Count);
            HTPropertyDefinition htprop = htprops[0];
            Assert.AreEqual("Id", htprop.Name);
            Assert.AreEqual("uint", htprop.Type);
            Assert.AreEqual(null, htprop.ToStringOverride);
            htprop = htprops[1];
            Assert.AreEqual("Name", htprop.Name);
            Assert.AreEqual("string", htprop.Type);
            Assert.AreEqual(null, htprop.ToStringOverride);
        }
        [Test]
        public void WithToStringOverride()
        {
            string filecontent = @"
A Tenancies ObservableCollection<Tenancy> Count.ToString()
";
            List<HTPropertyDefinition> htprops = HTPropertyDefinition.fromFile(filecontent);
            Assert.AreEqual(1, htprops.Count);
            HTPropertyDefinition htprop = htprops[0];
            Assert.AreEqual("Tenancies", htprop.Name);
            Assert.AreEqual("ObservableCollection<Tenancy>", htprop.Type);
            Assert.AreEqual("Count.ToString()", htprop.ToStringOverride);
        }
        [Test]
        public void Always()
        {
            string filecontent = @"
A Id uint
";
            List<HTPropertyDefinition> htprops = HTPropertyDefinition.fromFile(filecontent);
            Assert.AreEqual(1, htprops.Count);
            HTPropertyDefinition htprop = htprops[0];
            Assert.AreEqual(true, htprop.AlwaysCopy);
        }
        [Test]
        public void Sometimes()
        {
            string filecontent = @"
S Id uint
";
            List<HTPropertyDefinition> htprops = HTPropertyDefinition.fromFile(filecontent);
            Assert.AreEqual(1, htprops.Count);
            HTPropertyDefinition htprop = htprops[0];
            Assert.AreEqual(false, htprop.AlwaysCopy);
        }
    }
}