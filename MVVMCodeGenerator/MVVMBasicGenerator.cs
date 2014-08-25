using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMCodeGenerator
{
    public static class MVVMBasicGenerator
    {
        public static string doConversion(string namesSpace, string className, List<HTPropertyDefinition> htprops)
        {
            string template =
@"using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace {0}
{{
    public class Base{1} : BaseObject, I{1}
    {{
{2}        public override void SetFrom(BaseObject _other, bool full = true)
        {{
            {1} other = ({1})_other;
{3}        }}

        public override string ToString()
        {{
            return ""{1}[""
{4}                + ""]"";
        }}
    }}
}}
";
            return string.Format(template,
                namesSpace,
                className,
                multipleProperties(htprops),
                multiplePropertiesCopyFrom(htprops),
                multiplePropertiesToString(htprops));
        }
        public static string singleProperty(HTPropertyDefinition htprop)
        {
            string template =
@"        public virtual {1} {0}
        {{
            get
            {{
                object value = getPropertyValue(""{0}"");
                return value == null ? default({1}) : ({1})value;
            }}
            set {{ setPropertyValue(""{0}"", value); }}
        }}
";
            return string.Format(template, htprop.Name, htprop.Type);
        }

        public static string multipleProperties(List<HTPropertyDefinition> htprops)
        {
            List<string> props = new List<string>();
            foreach (HTPropertyDefinition htprop in htprops)
            {
                props.Add(singleProperty(htprop));
            }
            string joiner =
@"
";
            return string.Join(joiner, props.ToArray()) + (htprops.Count > 0 ? Environment.NewLine : "");
        }

        public static string singlePropertyCopyFrom(HTPropertyDefinition htprop)
        {
            string template = @"            this.{0} = other.{0};
";
            if (!htprop.AlwaysCopy)
            {
                template = @"            if (full)
    " + template;
            }
            return string.Format(template, htprop.Name);
        }

        public static string multiplePropertiesCopyFrom(List<HTPropertyDefinition> htprops)
        {
            string result = "";
            List<string> props = new List<string>();
            foreach (HTPropertyDefinition htprop in htprops)
            {
                result += singlePropertyCopyFrom(htprop);
            }
            return result;
        }

        public static string singlePropertyToString(HTPropertyDefinition htprop)
        {
            string tostring = htprop.ToStringOverride;
            if (tostring == null)
            {
                if (htprop.Type == "string")
                    tostring = "";
                else
                    tostring = "ToString()";
            }
            if (tostring != "")
                tostring = "." + tostring;
    
            string template =
@"                + (object.Equals({0}, null) ? ""null"" : {0}{1})";
            return string.Format(template, htprop.Name, tostring);
        }

        public static string multiplePropertiesToString(List<HTPropertyDefinition> htprops)
        {
            List<string> props = new List<string>();
            foreach (HTPropertyDefinition htprop in htprops)
            {
                props.Add(singlePropertyToString(htprop));
            }
            string joiner =
@"
                + ""/""
";
            return string.Join(joiner, props.ToArray()) + (props.Count > 0 ? Environment.NewLine : "");
        }
        public static string doInterfaceConversion(string namesSpace, string className, List<HTPropertyDefinition> htprops)
        {
            string template =
@"using System;
using System.Collections.ObjectModel;

namespace {0}
{{
    interface I{1}
    {{
{2}    }}
}}
";
            return string.Format(template, namesSpace, className, multipleInterfaceProperties(htprops));
        }

        public static string singleInterfaceProperty(HTPropertyDefinition htprop)
        {
            string template = @"        {1} {0} {{ get; set; }}
";
            return string.Format(template, htprop.Name, htprop.Type);
        }
        public static string multipleInterfaceProperties(List<HTPropertyDefinition> htprops)
        {
            string result = "";
            foreach (HTPropertyDefinition htprop in htprops)
            {
                result += singleInterfaceProperty(htprop);
            }
            return result;
        }
        public static string doPartialObjectConversion(string namesSpace, string className, List<HTPropertyDefinition> htprops)
        {
            string template = @"using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace {1}
{{
    public class Partial{0} : {0}, IPartialObject
    {{
        IUpdater<BaseObject> myupdater;
        BaseObject other;
        BaseObject myprivate;

        public Partial{0}(IUpdater<BaseObject> myupdater)
        {{
            this.myupdater = myupdater;

            Clear();
        }}

        public BaseObject SetObject
        {{
            set
            {{
                other = (value == null ? new {0}() : value);
                if (myprivate != null)
                    myprivate.PropertyChanged -= myprivate_PropertyChanged;
                myprivate = null;
                this.OnPropertyChanged("""");
            }}
        }}

        private BaseObject GetObjectForGet()
        {{
            return myprivate == null ? other : myprivate;
        }}
        private BaseObject GetObjectForSet()
        {{
            if (myprivate == null)
            {{
                myprivate = new {0}();
                myprivate.SetFrom(other, true);
                myprivate.PropertyChanged += myprivate_PropertyChanged;
            }}
            return myprivate;
        }}

        void myprivate_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {{
            this.OnPropertyChanged(e.PropertyName);
        }}

        public override object getPropertyValue(string property_name)
        {{
            return GetObjectForGet().getPropertyValue(property_name);
        }}

        public override void setPropertyValue(string property_name, object value)
        {{
            GetObjectForSet().setPropertyValue(property_name, value);
        }}

        public override string ToString()
        {{
            return ""other: ["" + (other == null ? ""null"" : other.ToString()) + ""] myprivate: ["" +
                (myprivate == null ? ""null"" : myprivate.ToString()) + ""]"";
        }}

        public void Add()
        {{
            if (other != null && myprivate != null)
            {{
                (({0})myprivate).Id = 0;
                myupdater.Add(myprivate);

                myprivate.PropertyChanged -= myprivate_PropertyChanged;
                myprivate = null;
            }}
        }}

        public void Update()
        {{
            if (other != null && myprivate != null)
            {{
                myupdater.Update(myprivate, other);

                myprivate.PropertyChanged -= myprivate_PropertyChanged;
                myprivate = null;
            }}
        }}
        public void Delete()
        {{
            if (other != null)
            {{
                myupdater.Delete(other);
                
                if (myprivate != null)
                {{
                    myprivate.PropertyChanged -= myprivate_PropertyChanged;
                    myprivate = null;
                }}
            }}
        }}
        public void Clear()
        {{
            SetObject = null;
        }}
    }}
}}
";
            return string.Format(template, className, namesSpace, className.ToLower()); ;
        }
    }
}
