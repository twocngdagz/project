using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace MVVMCodeGenerator
{
    [TestFixture]
    public class CodeGeneratorTest
    {
        HTPropertyDefinition htprop = new HTPropertyDefinition("Id", "uint");
        List<HTPropertyDefinition> htprops = new List<HTPropertyDefinition>();
        public CodeGeneratorTest()
        {
            htprops.Add(new HTPropertyDefinition("Id", "uint"));
            htprops.Add(new HTPropertyDefinition("Name", "string"));
            htprops[0].AlwaysCopy = false;
            htprops[1].AlwaysCopy = true;
        }

        [Test]
        public void MainBody()
        {
            string actual = MVVMBasicGenerator.doConversion("Mashisho", "Tenant", new List<HTPropertyDefinition>());
            string expected =
@"using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class BaseTenant : BaseObject, ITenant
    {
        public override void SetFrom(BaseObject _other, bool full = true)
        {
            Tenant other = (Tenant)_other;
        }

        public override string ToString()
        {
            return ""Tenant[""
                + ""]"";
        }
    }
}
";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SingleProperty()
        {
            string actual = MVVMBasicGenerator.singleProperty(htprop);
            string expected = 
@"        public virtual uint Id
        {
            get
            {
                object value = getPropertyValue(""Id"");
                return value == null ? default(uint) : (uint)value;
            }
            set { setPropertyValue(""Id"", value); }
        }
";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TwoProperties()
        {
            string actual = MVVMBasicGenerator.multipleProperties(htprops);
            string expected = 
@"        public virtual uint Id
        {
            get
            {
                object value = getPropertyValue(""Id"");
                return value == null ? default(uint) : (uint)value;
            }
            set { setPropertyValue(""Id"", value); }
        }

        public virtual string Name
        {
            get
            {
                object value = getPropertyValue(""Name"");
                return value == null ? default(string) : (string)value;
            }
            set { setPropertyValue(""Name"", value); }
        }

";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SinglePropertyCopyFromAlways()
        {
            string actual = MVVMBasicGenerator.singlePropertyCopyFrom(htprops[1]);
            string expected =
@"            this.Name = other.Name;
";

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SinglePropertyCopyFromSometimes()
        {
            string actual = MVVMBasicGenerator.singlePropertyCopyFrom(htprops[0]);
            string expected =
@"            if (full)
                this.Id = other.Id;
";

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void MultiplePropertyCopyFrom()
        {
            string actual = MVVMBasicGenerator.multiplePropertiesCopyFrom(htprops);
            string expected =
@"            if (full)
                this.Id = other.Id;
            this.Name = other.Name;
";

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SinglePropertyToString()
        {
            string actual = MVVMBasicGenerator.singlePropertyToString(htprop);
            string expected =
@"                + (object.Equals(Id, null) ? ""null"" : Id.ToString())";

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SinglePropertyToStringForStringType()
        {
            string actual = MVVMBasicGenerator.singlePropertyToString(new HTPropertyDefinition("Name", "string"));
            string expected =
@"                + (object.Equals(Name, null) ? ""null"" : Name)";

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SinglePropertyToStringForOverride()
        {
            string actual = MVVMBasicGenerator.singlePropertyToString(new HTPropertyDefinition("Tenancies", "ObservableCollection<Tenancy>", "Count.ToString()"));
            string expected =
@"                + (object.Equals(Tenancies, null) ? ""null"" : Tenancies.Count.ToString())";

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void MultiplePropertyToString()
        {
            string actual = MVVMBasicGenerator.multiplePropertiesToString(htprops);
            string expected =
@"                + (object.Equals(Id, null) ? ""null"" : Id.ToString())
                + ""/""
                + (object.Equals(Name, null) ? ""null"" : Name)
";

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Full()
        {
            string actual = MVVMBasicGenerator.doConversion("Mashisho", "Tenant", htprops);
            string expected =
@"using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class BaseTenant : BaseObject, ITenant
    {
        public virtual uint Id
        {
            get
            {
                object value = getPropertyValue(""Id"");
                return value == null ? default(uint) : (uint)value;
            }
            set { setPropertyValue(""Id"", value); }
        }

        public virtual string Name
        {
            get
            {
                object value = getPropertyValue(""Name"");
                return value == null ? default(string) : (string)value;
            }
            set { setPropertyValue(""Name"", value); }
        }

        public override void SetFrom(BaseObject _other, bool full = true)
        {
            Tenant other = (Tenant)_other;
            if (full)
                this.Id = other.Id;
            this.Name = other.Name;
        }

        public override string ToString()
        {
            return ""Tenant[""
                + (object.Equals(Id, null) ? ""null"" : Id.ToString())
                + ""/""
                + (object.Equals(Name, null) ? ""null"" : Name)
                + ""]"";
        }
    }
}
";
            Assert.AreEqual(expected, actual);
        }
    }
}
