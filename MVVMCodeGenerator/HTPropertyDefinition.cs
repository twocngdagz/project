using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MVVMCodeGenerator
{
    public class HTPropertyDefinition
    {
        public HTPropertyDefinition() { }
        public HTPropertyDefinition(string name, string type, string tostring_override=null)
        {
            this.Name = name;
            this.Type = type;
            this.ToStringOverride = tostring_override;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public bool AlwaysCopy { get; set; }
        public string ToStringOverride { get; set; }

        public static List<HTPropertyDefinition> fromFile(string filecontent)
        {
            Regex re = new Regex("\\s*([A|S])\\s+(\\S+)\\s+(\\S+)(\\s+(\\S+))?");
            List<HTPropertyDefinition> result = new List<HTPropertyDefinition>();
            foreach (string line in filecontent.Split('\n'))
            {
                Match m = re.Match(line);
                if (m.Success)
                {
                    HTPropertyDefinition this_result = new HTPropertyDefinition(m.Groups[2].Value, m.Groups[3].Value);
                    this_result.AlwaysCopy = (m.Groups[1].Value == "A");
                    if (m.Groups[5].Success)
                        this_result.ToStringOverride = m.Groups[5].Value;

                    result.Add(this_result);
                }
            }

            return result;
        }
    }
}
